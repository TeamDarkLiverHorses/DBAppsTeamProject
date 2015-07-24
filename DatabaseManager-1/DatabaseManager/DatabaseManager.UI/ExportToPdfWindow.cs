namespace DatabaseManager.UI
{
    using System;
    using System.Windows.Forms;
    using DatabaseManager.UI.Utilities;
    using System.Collections.Generic;
    using DatabaseManager.Models;
    using DatabaseManager.SalesReports;
    using System.Linq;

    public partial class ExportToPdfWindow : Form
    {
        private const string ReportFilePath = @"\\psf\Dropbox\Personal\SoftUni\Level 3\DB Apps\team project\DBAppsTeamProject\DatabaseManager-1\DatabaseManager\DatabaseManager.SalesReports\SalesReports\test.pdf";
        //private const string reportFilePath = @"test.pdf";
        private List<Sale> salesData;

        public ExportToPdfWindow()
        {
            InitializeComponent();
            this.comboSearchBy.SelectedIndexChanged += SearchOption;
            this.comboSearchBy.DataSource = new string[] { Constants.OnDate, Constants.BeforeDate, Constants.AfterDate, Constants.BetweenDates };
            this.btnSearch.Click += Search;
            this.btnExport.Click += ExportToPdf;
            this.salesData = new List<Sale>();
        }

        private void Search(object sender, EventArgs e)
        {
            if (this.comboSearchBy.DataSource == null || this.comboSearchBy.Items.Count < 1)
            {
                MessageBox.Show("The are no tables.");
                return;
            }

            using (var salesReportProvider = new SalesReportForPeriod())
            {
                switch (this.comboSearchBy.SelectedItem.ToString())
                {
                    case Constants.OnDate:
                        this.salesData = salesReportProvider.
                            GetSalesOn(this.dateMain.Value).
                            ToList();
                        break;
                    case Constants.BeforeDate:
                        this.salesData = salesReportProvider.
                            GetSalesBefore(this.dateMain.Value).
                            ToList();
                        break;
                    case Constants.AfterDate:
                        this.salesData = salesReportProvider.
                            GetSalesAfter(this.dateMain.Value).
                            ToList();
                        break;
                    case Constants.BetweenDates:
                        this.salesData = salesReportProvider.
                            GetSalesBetween(this.dateMain.Value, this.dateHelper.Value).
                            ToList();
                        break;
                    default:
                        MessageBox.Show("Not valid option.");
                        break;
                }
            }
        }

        private void ExportToPdf(object sender, EventArgs e)
        {
            if (this.salesData.Count == 0)
            {
                MessageBox.Show("The sales table is empty.");
                return;
            }
            PdfReport report = new PdfReport(ReportFilePath);
            report.Create(salesData);
        }

        private void SearchOption(object sender, EventArgs e)
        {
            if (this.comboSearchBy.DataSource != null && this.comboSearchBy.Items.Count > 0)
            {
                this.dateHelper.Enabled = this.comboSearchBy.SelectedItem.ToString() != Constants.BetweenDates;
            }
            else
            {
                MessageBox.Show("The are no tables.");
            }
        }
    }
}