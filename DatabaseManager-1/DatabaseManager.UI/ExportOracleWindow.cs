using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseManager.Core.OracleConnectionDB;
using DatabaseManager.Core.ExportToSqlServer;

namespace DatabaseManager.UI
{
    public partial class ExportOracleWindow : Form
    {
        public ExportOracleWindow()
        {
            InitializeComponent();

            LoadEvents();
        }

        private void LoadEvents()
        {
            this.btnExport.Click += Export;

            this.btnClear.Click += ClearData;

            this.mExit.Click += ExitForm;
        }

        public void InsertInfo(string text)
        {
            this.listInfo.Items.Add(text);
        }

        private void Export(object sender, EventArgs e)
        {
            DataTable oracleTable = null;

            ExportToSql export;

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

                    using (export = new ExportToSql())
                    {
                        finalReport = export.BuildDataToExport(oracleTable, "PRODUCTNAME", "PRODUCTPRICE",
                        "CATEGORYNAME", "MEASURENAME", "VENDORNAME");

                        foreach (string key in finalReport.Keys)
                        {
                            listInfo.Items.Add(key + " - " + finalReport[key].ToString());
                        }

                        listInfo.Items.Add("Exporting ...");

                        finalReport.Clear();
                        
                        finalReport = export.ExportData();
                    }

                    foreach (string key in finalReport.Keys)
                    {
                        listInfo.Items.Add(key + " - " + finalReport[key].ToString());
                    }

                    listInfo.Items.Add("Done!!!");
                }
                else
                {
                    listInfo.Items.Add("There are no products to export.");
                }

            }
            catch (FormatException fEx)
            {
                listInfo.Items.Add(fEx.Message);
                MessageBox.Show(fEx.Message);
            }
            catch (Exception ex)
            {
                listInfo.Items.Add(ex.Message);
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
