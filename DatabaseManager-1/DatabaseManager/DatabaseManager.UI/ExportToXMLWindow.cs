namespace DatabaseManager.UI
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using DatabaseManager.XML;
    using DatabaseManager.XML.Serialization;

    public partial class ExportToXMLWindow : Form
    {
        public ExportToXMLWindow()
        {
            InitializeComponent();
            this.btnExportSales.Click += ExportSales;
            this.importFromXMLBtn.Click += ImportVendorExpenses;
        }

        private async void ImportVendorExpenses(object sender, System.EventArgs e)
        {
            var xml = await XMLSerializer.ReadXML(SelectFileName(false));
            //MessageBox.Show(xml.Vendors.Length.ToString());

            var context = new DatabaseManager.Data.SupermarketsContext();
            int vendorsCount = 0;
            int expensesCount = 0;
            foreach (var vendor in xml.Vendors)
            {
                var dbVendor = context.Vendors.Where(v => v.Name == vendor.Name).FirstOrDefault();
                if (dbVendor != null)
                {
                    vendorsCount++;
                    foreach (var expense in vendor.Summaries)
                    {
                        var newExpense = new DatabaseManager.Models.Expense()
                        {
                            Date = DateTime.Parse(expense.Month),
                            Ammount = decimal.Parse(expense.Price)
                        };
                        dbVendor.Expenses.Add(newExpense);
                        expensesCount++;
                    }
                }
            }
            context.SaveChanges();
            this.logList.Items.Add(string.Format(
                "Adding {0} expenses for {1} vendors...",
                expensesCount, vendorsCount));
            this.logList.Items.Add("Done!");
        }

        private async void ExportSales(object sender, System.EventArgs e)
        {
            var startDate = this.startDateControl.Value.Date;
            var endDate = this.endDateControl.Value.Date;
            var fileName = SelectFileName(true);
            if (fileName == null) return;

            this.logList.Items.Add("Building reports...");
            var salesReports = await Task.Run(() => BuildSalesReports(startDate, endDate));

            this.logList.Items.Add("Writing file...");
            await Task.Run(() => XMLSerializer.WriteXML(salesReports, fileName));
            this.logList.Items.Add("Done");
        }

        private async Task<SalesReportsWrapper> BuildSalesReports(DateTime startDate, DateTime endDate)
        {
            using (var context = new DatabaseManager.Data.SupermarketsContext())
            {
                var saleReportsQuery = from sale in context.Sales
                                       where sale.Date >= startDate
                                       where sale.Date <= endDate
                                       group new { sale.UnitPrice, sale.Date } by new { sale.Product.Vendor.Name } into salesGroup
                                       select new
                                       {
                                           Vendor = salesGroup.Key,
                                           Sales = from sale in salesGroup
                                                   group sale.UnitPrice by sale.Date into datesGroup
                                                   select new { Date = datesGroup.Key, TotalSum = datesGroup.Sum() }
                                       };

                var vendorGroups = await saleReportsQuery.ToArrayAsync();
                string monthFormat = @"MMM-yyyy";
                string currencyFormat = @"# ###.00";
                var salesReports = vendorGroups.Select(vg =>
                {
                    var summaries = vg.Sales.
                        Select(s => new Summary()
                        {
                            Date = s.Date.ToString(monthFormat),
                            TotalPrice = s.TotalSum.ToString(currencyFormat)
                        });
                    var salesReport = new SalesReport()
                        {
                            Vendor = vg.Vendor.Name,
                            Summaries = summaries.ToArray()
                        };
                    return salesReport;
                });
                var salesReportsWrapper =
                    new SalesReportsWrapper() { SaleReports = salesReports.ToArray() };
                return salesReportsWrapper;
            }
        }

        private string SelectFileName(bool isSave)
        {
            FileDialog dialog = null;
            if (isSave) dialog = new SaveFileDialog();
            else dialog = new OpenFileDialog();

            using (dialog)
            {
                dialog.Filter = "XML (*.xml)|*.xml";
                if (dialog.ShowDialog() == DialogResult.OK) return dialog.FileName;
            }

            return null;
        }
    }
}
