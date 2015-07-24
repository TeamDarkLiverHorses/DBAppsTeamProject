namespace DatabaseManager.UI
{
    using System;
    using System.Windows.Forms;
    using DatabaseManager.UI.Utilities;
    using System.Collections.Generic;
    using DatabaseManager.Models;
    using DatabaseManager.SalesReports;

    public partial class ExportToPdfWindow : Form
    {
        private const string ReportFilePath = @"\\psf\Dropbox\Personal\SoftUni\Level 3\DB Apps\team project\DBAppsTeamProject\DatabaseManager-1\DatabaseManager\DatabaseManager.SalesReports\test.pdf";
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
            if (this.comboSearchBy.DataSource != null && this.comboSearchBy.Items.Count > 0)
            {
                switch (this.comboSearchBy.SelectedItem.ToString())
                {
                    case Constants.OnDate:
                        {
                            DateTime date = this.dateMain.Value;
                            this.salesData = new SalesReportForPeriod().GetSalesOn(date);
                        }
                        break;
                    case Constants.BeforeDate:
                        {
                            DateTime date = this.dateMain.Value;
                            salesData = new SalesReportForPeriod().GetSalesBefore(date);
                        }
                        break;
                    case Constants.AfterDate:
                        {
                            DateTime startDate = this.dateMain.Value;
                            this.salesData = new SalesReportForPeriod().GetSalesAfter(startDate);
                        }
                        break;
                    case Constants.BetweenDates:
                        {
                            DateTime startDate = this.dateMain.Value;
                            DateTime endDate = this.dateHelper.Value;
                            this.salesData = new SalesReportForPeriod().GetSalesBetween(startDate, endDate);
                        }
                        break;
                    default:
                        MessageBox.Show("Not valid option.");
                        break;
                }
            }
            else
            {
                MessageBox.Show("The are no tables.");
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
                if (this.comboSearchBy.SelectedItem.ToString() != Constants.BetweenDates)
                {
                    this.dateHelper.Enabled = false;
                }
                else
                {
                    this.dateHelper.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("The are no tables.");
            }
        }


    }
}
