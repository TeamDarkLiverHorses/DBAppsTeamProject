namespace DatabaseManager.UI
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;
    using DatabaseManager.ImportSalesData.OracleConnectionsDB;
    using DatabaseManager.ImportSalesData.ImportToSqlServer;
    using DatabaseManager.Models;
    using DatabaseManager.ImportSalesData.ImportFromExcel;

    public partial class ImportToSQLWindow : Form
    {
        public ImportToSQLWindow()
        {
            InitializeComponent();
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

                    ReadZipFile readExcel = new ReadZipFile();

                    List<Sale> sales = readExcel.GetExcelEntriesFromZip(filePath);

                    this.listInfo.Items.Add("Building sales...");

                    ImportFromExcelDataHolder excelDataHolder = null;

                    excelDataHolder = BuildDataFromExcel.BuildSales(sales.ToArray());

                    this.listInfo.Items.Add(string.Format("There are {0} sale(s) for import.", excelDataHolder.Sales.Length));

                    this.listInfo.Items.Add(string.Format("There are {0} shop(s) for import.", excelDataHolder.Shops.Count));

                    this.listInfo.Items.Add("Importing...");

                    // throws error
                    int importedExcels = ImportToSql.ExportDataFromExcel(excelDataHolder);

                    this.listInfo.Items.Add(string.Format("Number of imported sales - {0}.", importedExcels));

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

        private void ExportFromOracle(object sender, EventArgs e)
        {
            const string CommandString =
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
                var oracleTable = oracleAction.SelectProduct(CommandString);

                listInfo.Items.Add("Building data to export...");
                if (oracleTable == null)
                {
                    this.listInfo.Items.Add("There are no products to export.");
                    return;
                }

                var oracleData = BuildDataFromOracle.BuildProducts(oracleTable, "PRODUCTNAME", "PRODUCTPRICE",
                "CATEGORYNAME", "MEASURENAME", "VENDORNAME");

                this.listInfo.Items.Add(string.Format("Threre are {0} categories to export.", oracleData.Categories.Count));
                this.listInfo.Items.Add(string.Format("Threre are {0} measures to export.", oracleData.Measures.Count));
                this.listInfo.Items.Add(string.Format("Threre are {0} vendors to export.", oracleData.Vendors.Count));
                this.listInfo.Items.Add(string.Format("Threre are {0} products to export.", oracleData.Products.Length));

                listInfo.Items.Add("Exporting ...");
                var finalReport = ImportToSql.ImportFromOracleToMSSql(oracleData);
                foreach (string key in finalReport.Keys)
                {
                    this.listInfo.Items.Add(key + " - " + finalReport[key].ToString());
                }
                this.listInfo.Items.Add("Done!!!");

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
