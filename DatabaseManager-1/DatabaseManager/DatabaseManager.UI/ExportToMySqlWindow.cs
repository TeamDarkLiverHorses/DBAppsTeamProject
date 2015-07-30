namespace DatabaseManager.UI
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using DatabaseManager.MySqlImporter;
    using System.Text;

    public partial class ExportToMySqlWindow : Form
    {
        public ExportToMySqlWindow()
        {
            InitializeComponent();
            this.btnClear.Click += ClearData;
            this.btnExport.Click += ExportData;
            this.mExit.Click += (o, s) => this.Close();
        }

        private void ExportData(object sender, EventArgs e)
        {
            ClearData(null, null);

            Task.Run(() =>
            {
                try
                {
                    ImportResult resultVendors = VendorDataImporter.ImportVendors(VendorDataGenerator.GetVendorExpenses());
                    ImportResult resultProducts = VendorDataImporter.ImportProducts(VendorDataGenerator.GetProductIncomes());

                    StringBuilder output = new StringBuilder();
                    output.AppendFormat("{0} records insrted and {1} records updated in Vendors.",
                        resultVendors.Inserted, resultVendors.Updated);
                    output.AppendLine();
                    output.AppendFormat("{0} records insrted and {1} records updated in Products.",
                        resultProducts.Inserted, resultProducts.Updated);

                    this.Invoke(new Action(() => { this.txtInfo.Text = output.ToString(); }));
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() => { MessageBox.Show(ex.Message); }));
                }
            });
        }

        private void ClearData(object sender, EventArgs e)
        {
            txtInfo.Clear();
        }
    }
}
