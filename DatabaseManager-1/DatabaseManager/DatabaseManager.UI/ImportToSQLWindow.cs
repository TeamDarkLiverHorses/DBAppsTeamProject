namespace DatabaseManager.UI
{
    using System;
    using System.Windows.Forms;
    using DatabaseManager.ImportSalesData.ImportToSqlServer;
    using DatabaseManager.ImportSalesData.Utilities;
    using System.Threading.Tasks;

    public partial class ImportToSQLWindow : Form
    {
        public ImportToSQLWindow()
        {
            InitializeComponent();
            this.btnExport.Click += ImportFromOracle;
            this.btnClear.Click += ClearData;
            this.mExit.Click += ExitForm;
            this.btnExcel.Click += ImportFromExcel;
        }

        public void InsertInfo(string text)
        {
            this.listInfo.Items.Add(text);
        }

        private void ImportFromExcel(object sender, EventArgs e)
        {
            
            string filePath = string.Empty;
            using (OpenFileDialog openZip = new OpenFileDialog())
            {
                openZip.Filter = "Zip file (*.zip)|*.zip";

                if (openZip.ShowDialog() == DialogResult.OK)
                {
                    filePath = openZip.FileName;
                }
            }

            if (filePath != string.Empty)
            {
                // We run those procs in a new thread so our window will not/must not/should not/cannot/is not going to freeze!
                Task.Run(() =>
                {
                    try
                    {
                        listInfo.Invoke((MethodInvoker)(() => listInfo.Items.Add("Reading file...")));

                        ExcellDataExtractor extractor = new ExcellDataExtractor(filePath);
                        extractor.Read();

                        listInfo.Invoke((MethodInvoker)(() =>
                        {
                            this.listInfo.Items.Add("Building sales...");
                            this.listInfo.Items.Add(string.Format("There are {0} sale(s) for import.", extractor.Sales.Count));
                            this.listInfo.Items.Add(string.Format("There are {0} shop(s) for import.", extractor.Shops.Count));
                            this.listInfo.Items.Add("Importing...");
                        }));

                        // Throws error
                        ExcelDataImporter importer = new ExcelDataImporter(extractor);
                        int importedSalesCount = importer.Import();

                        listInfo.Invoke((MethodInvoker)(() =>
                        {
                            this.listInfo.Items.Add(string.Format(Messages.ImportedSales, importedSalesCount));
                            this.listInfo.Items.Add("Done!!!");
                        }));
                    }
                    catch (FormatException formatEx)
                    {
                        MessageBox.Show(formatEx.Message);
                        listInfo.Invoke((MethodInvoker)(() =>
                        {
                            this.listInfo.Items.Add(formatEx.Message);
                        }));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        listInfo.Invoke((MethodInvoker)(() =>
                        {
                            this.listInfo.Items.Add(ex.Message);
                        }));
                    }
                });
            }
        }

        private void ImportFromOracle(object sender, EventArgs e)
        {
            // We run those procs in a new thread so our window will not/must not/should not/cannot/is not going to freeze!
            Task.Run(() =>
            {
                var oracleImporter = new OracleImporter();
                string vendors = oracleImporter.ImportVendors();
                string measures = oracleImporter.ImportMeasures();
                string categories = oracleImporter.ImportCategories();
                string parentCategories = oracleImporter.ImportParentCategories();
                string products = oracleImporter.ImportProducts();

                listInfo.Invoke((MethodInvoker)(() =>
                {
                    listInfo.Items.AddRange(new object[]
                    {
                        vendors,
                        measures,
                        categories,
                        parentCategories,
                        products
                    });
                }));
            });
        }

        private void ClearData(object sender, EventArgs e)
        {
            this.listInfo.Items.Clear();
        }

        private void ExitForm(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
