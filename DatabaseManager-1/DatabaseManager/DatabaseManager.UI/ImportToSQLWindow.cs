namespace DatabaseManager.UI
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using DatabaseManager.ImportSalesData.OracleConnectionsDB;
    using DatabaseManager.ImportSalesData.ImportToSqlServer;

    public partial class ImportToSQLWindow : Form
    {
        public ImportToSQLWindow()
        {
            InitializeComponent();

            LoadEvents();
        }

        private void LoadEvents()
        {
            this.btnExport.Click += ExportFromOracle;

            this.btnClear.Click += ClearData;

            this.mExit.Click += ExitForm;

            this.btnExcel.Click += ExportFromExcel;
        }

        public void InsertInfo(string text)
        {
            this.listInfo.Items.Add(text);
        }

        private void ExportFromExcel(object sender, EventArgs e)
        {
            MessageBox.Show("I don't work yet.");

            // to do
            // open file dialog to zip file
            // read file
            
            // get Models.Sale[] class for all sales
            
            // ImportFromExcelDataHolder = BuildDataFromExcel(Models.Sale[])
            // ImportToSql(ExportFromExcelDataHolder)
        }

        private void ExportFromOracle(object sender, EventArgs e)
        {
            DataTable oracleTable = null;

            ImportFromOracleDataHolder oracleData = null;

            ImportToSql export = null;

            string commandString =
                "SELECT PRODUCTS.NAME AS PRODUCTNAME, PRODUCTS.PRICE AS PRODUCTPRICE, CATEGORIES.NAME AS CATEGORYNAME, " +
                "MEASURES.NAME AS MEASURENAME, VENDORS.NAME AS VENDORNAME " +
                "FROM PRODUCTS " +
                "JOIN CATEGORIES ON PRODUCTS.CATEGORY_ID = CATEGORIES.ID " +
                "JOIN VENDORS ON PRODUCTS.VENDOR_ID = VENDORS.ID " +
                "JOIN MEASURES ON PRODUCTS.MEASURE_ID = MEASURES.ID ORDER BY Products.name";

            try
            {
                listInfo.Items.Add("Getting data to export...");

                OracleDBAction oracleAction = new OracleDBAction();

                oracleTable = oracleAction.SelectProduct(commandString);

                listInfo.Items.Add("Building data to export...");
                
                if (oracleTable != null)
                {
                    Dictionary<string, int> finalReport = new Dictionary<string, int>();
                    
                    BuildDataFromOracle buildDataFromOracle = new BuildDataFromOracle();
                    oracleData = buildDataFromOracle.BuildProducts(oracleTable, "PRODUCTNAME", "PRODUCTPRICE",
                    "CATEGORYNAME", "MEASURENAME", "VENDORNAME");

                    oracleTable.Dispose();
                    oracleTable = null;

                    this.listInfo.Items.Add(string.Format("Threre are {0} categories to export.", oracleData.Categories.Count));
                    this.listInfo.Items.Add(string.Format("Threre are {0} measures to export.", oracleData.Measures.Count));
                    this.listInfo.Items.Add(string.Format("Threre are {0} vendors to export.", oracleData.Vendors.Count));
                    this.listInfo.Items.Add(string.Format("Threre are {0} products to export.", oracleData.Products.Length));

                    listInfo.Items.Add("Exporting ...");
                    
                    using (export = new ImportToSql())
                    {
                        finalReport.Clear();
                        
                        finalReport = export.ExportDataFromOracle(oracleData);
                    }

                    foreach (string key in finalReport.Keys)
                    {
                        this.listInfo.Items.Add(key + " - " + finalReport[key].ToString());
                    }

                    this.listInfo.Items.Add("Done!!!");
                }
                else
                {
                    this.listInfo.Items.Add("There are no products to export.");
                }

            }
            catch (FormatException fEx)
            {
                this.listInfo.Items.Add(fEx.Message);
                MessageBox.Show(fEx.Message);
            }
            catch (Exception ex)
            {
                this.listInfo.Items.Add(ex.Message);
                MessageBox.Show(ex.Message);
            }
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
