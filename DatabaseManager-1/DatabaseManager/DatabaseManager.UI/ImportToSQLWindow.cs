namespace DatabaseManager.UI
{
    using System;
    using System.Windows.Forms;
    using DatabaseManager.ImportSalesData.ImportToSqlServer;
    using DatabaseManager.ImportSalesData.Utilities;

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
            try
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
                    listInfo.Items.Add("Reading file...");

                    ExcellDataExtractor extractor = new ExcellDataExtractor(filePath);
                    extractor.Read();

                    this.listInfo.Items.Add("Building sales...");
                    this.listInfo.Items.Add(string.Format("There are {0} sale(s) for import.", extractor.Sales.Count));
                    this.listInfo.Items.Add(string.Format("There are {0} shop(s) for import.", extractor.Shops.Count));
                    this.listInfo.Items.Add("Importing...");

                    // Throws error
                    ExcelDataImporter importer = new ExcelDataImporter(extractor);
                    int importedSalesCount = importer.Import();

                    this.listInfo.Items.Add(string.Format(Messages.ImportedSales, importedSalesCount));
                    this.listInfo.Items.Add("Done!!!");
                }

            }
            catch (FormatException formatEx)
            {
                MessageBox.Show(formatEx.Message);
                this.listInfo.Items.Add(formatEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.listInfo.Items.Add(ex.Message);
            }
        }

        private void ImportFromOracle(object sender, EventArgs e)
        {
            var oracleImporter = new OracleImporter();
            listInfo.Items.Add(oracleImporter.ImportVendors());
            listInfo.Items.Add(oracleImporter.ImportMeasures());
            listInfo.Items.Add(oracleImporter.ImportCategories());
            listInfo.Items.Add(oracleImporter.ImportParentCategories());
            listInfo.Items.Add(oracleImporter.ImportProducts());
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
