namespace DatabaseManager.UI
{
    using DatabaseManager.XML;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class ExportToXMLWindow : Form
    {
        public ExportToXMLWindow()
        {
            InitializeComponent();
            this.btnExportSales.Click += ExportSales;
        }

        private async void ExportSales(object sender, System.EventArgs e)
        {
            var startDate = this.startDateControl.Value.Date;
            var endDate = this.endDateControl.Value.Date;
            var fileName = SelectFileName();
            if (fileName == null) return;

            this.logList.Items.Add("Building reports...");
            var salesReports = await Task.Run(() => BuildSalesReports(startDate, endDate));

            this.logList.Items.Add("Writing file...");
            await Task.Run(() => Serializer.WriteXML(salesReports, fileName));
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
                var salesReports = vendorGroups.Select(vg =>
                {
                    var summaries = vg.Sales.
                        Select(s => new Summary()
                        {
                            Date = s.Date.ToString(),
                            TotalPrice = s.TotalSum.ToString()
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

        private string SelectFileName()
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "XML (*.xml)|*.xml";
                if (dialog.ShowDialog() == DialogResult.OK) return dialog.FileName;
            }

            return null;
        }
    }
}
