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
        //private const string ReportFilePath = @"\\psf\Dropbox\Personal\SoftUni\Level 3\DB Apps\team project\DBAppsTeamProject\DatabaseManager-1\DatabaseManager\DatabaseManager.SalesReports\SalesReports\test.pdf";
        //private const string reportFilePath = @"test.pdf";
        private List<Sale> salesData;

        public ExportToPdfWindow()
        {
            InitializeComponent();
            this.comboSearchBy.SelectedIndexChanged += SearchOption;
            this.comboSearchBy.DataSource = new string[] { Constants.OnDate, Constants.BeforeDate, Constants.AfterDate, Constants.BetweenDates };
            this.btnSearch.Click += Search;
            this.btnExport.Click += ExportToPdf;
            this.btnExportSelection.Click += ExportToPdfSelection;
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

                if (this.salesData.Count > 0)
                {
                    try
                    {
                        for (int i = 0; i < this.salesData.Count; i++)
                        {
                            int index = this.dataGridViewSales.Rows.Add();

                            DataGridViewRow row = new DataGridViewRow();
                            this.dataGridViewSales.Rows[index].Cells["columnProduct"].Value = salesData[i].Product.Name;
                            this.dataGridViewSales.Rows[index].Cells["columnQuantity"].Value = salesData[i].Quantity;
                            this.dataGridViewSales.Rows[index].Cells["columnPrice"].Value = salesData[i].UnitPrice;
                            this.dataGridViewSales.Rows[index].Cells["columnLocation"].Value = salesData[i].Shop.Name;
                            this.dataGridViewSales.Rows[index].Cells["columnDate"].Value = salesData[i].Date;
                        }

                        if (this.dataGridViewSales.Rows.Count > 0)
                        {
                            this.dataGridViewSales.Rows[0].Selected = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("No results");
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
            }
            else
            {
                ExportPDF();
            }
        }

        private void ExportToPdfSelection(object sender, EventArgs e)
        {
            if (this.salesData.Count == 0 || this.dataGridViewSales.SelectedRows.Count == 0)
            {
                MessageBox.Show("The sales table is empty.");
            }
            else
            {
                if (this.dataGridViewSales.SelectedRows.Count == 0)
                {
                    MessageBox.Show("No selection.");
                }
                else
                {
                    List<Sale> selectedSales = new List<Sale>(this.dataGridViewSales.SelectedRows.Count);

                    int[] indexes = new int[this.dataGridViewSales.SelectedRows.Count];

                    for (int i = 0; i < indexes.Length; i++)
                    {
                        indexes[i] = dataGridViewSales.SelectedRows[i].Index;
                    }

                    for (int a = 0; a < indexes.Length - 1; a++)
                    {
                        for (int b = a + 1; b < indexes.Length; b++)
                        {
                            if (indexes[b] < indexes[a])
                            {
                                int temp = indexes[a];
                                indexes[a] = indexes[b];
                                indexes[b] = temp;
                            }
                        }
                    }

                    for (int i = 0; i < indexes.Length; i++)
                    {
                        selectedSales.Add(this.salesData[indexes[i]]);
                    }

                    ExportPDF(selectedSales);
                }
            }
        }

        private void ExportPDF(List<Sale> sales = null)
        {
            if (sales == null)
            {
                sales = this.salesData;
            }

            string filePath = string.Empty;

            using (SaveFileDialog saveFile = new SaveFileDialog())
            {
                saveFile.Filter = "PDF (*.pdf)|*.pdf";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFile.FileName;
                }
            }

            if (filePath != string.Empty)
            {
                PdfReport report = new PdfReport(filePath);

                report.Create(sales);

                MessageBox.Show("Done!");
            }
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