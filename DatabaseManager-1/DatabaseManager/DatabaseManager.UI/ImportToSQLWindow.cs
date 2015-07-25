namespace DatabaseManager.UI
{
    using DatabaseManager.ImportSalesData.ImportToSqlServer;
    using DatabaseManager.ImportSalesData.Utilities;
    using System;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class ImportToSQLWindow : Form
    {
        public ImportToSQLWindow()
        {
            InitializeComponent();
            this.btnExport.Click += ImportFromOracle;
            this.btnClear.Click += (s, e) => this.logList.Items.Clear();
            this.mExit.Click += (s, e) => this.Close();
            this.btnExcel.Click += ImportFromExcel;
        }

        private void Log(object log)
        {
            Action AddToLogList = () => this.logList.Items.Add(log);
            if (this.logList.InvokeRequired)
            {
                this.logList.Invoke(AddToLogList);
            }
            else
            {
                AddToLogList();
            }
        }

        private void ImportFromExcel(object sender, EventArgs e)
        {
            string fileName = null;
            using (var fd = new OpenFileDialog())
            {
                fd.Filter = "Zip file (*.zip)|*.zip";
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    fileName = fd.FileName;
                }
                else
                {
                    return; // No file?! Stop executing this method and return void :))
                }
            }

            // We run those procs in a new thread so our window will not/must not/should not/cannot/is not going to freeze!
            Task.Run(() =>
            {
                try
                {
                    Log("Reading file...");
                    var extractor = new ExcelDataExtractor(fileName);
                    extractor.Read();

                    Log("Building sales...");
                    Log(string.Format("There are {0} sale(s) for import.", extractor.Sales.Count));
                    Log(string.Format("There are {0} shop(s) for import.", extractor.Shops.Count));
                    Log("Importing...");
                    
                    ExcelDataImporter importer = new ExcelDataImporter(extractor);
                    int importedSalesCount = importer.Import();

                    Log(string.Format(Messages.ImportedSales, importedSalesCount));
                    Log("Done!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Log(ex.Message);
                }
            });
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

                Log(vendors);
                Log(measures);
                Log(categories);
                Log(parentCategories);
                Log(products);
            });
        }
    }
}
