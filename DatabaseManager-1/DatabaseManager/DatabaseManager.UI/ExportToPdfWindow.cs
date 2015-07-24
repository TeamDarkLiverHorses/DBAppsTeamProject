namespace DatabaseManager.UI
{
    using System;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using DatabaseManager.Models;
    using DatabaseManager.SalesReports;
    using System.Linq;

    public partial class ExportToPdfWindow : Form
    {
        private List<Sale> salesData;

        public ExportToPdfWindow()
        {
            InitializeComponent();
            this.comboSearchBy.SelectedIndexChanged += OnComboChanged;
            this.comboSearchBy.DataSource = Enum.GetValues(typeof(SearchOption));
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
                switch ((SearchOption)this.comboSearchBy.SelectedItem)
                {
                    case SearchOption.ExactDate:
                        this.salesData = salesReportProvider.
                            GetSalesOn(this.dateMain.Value).
                            ToList();
                        break;
                    case SearchOption.BeforeDate:
                        this.salesData = salesReportProvider.
                            GetSalesBefore(this.dateMain.Value).
                            ToList();
                        break;
                    case SearchOption.AfterDate:
                        this.salesData = salesReportProvider.
                            GetSalesAfter(this.dateMain.Value).
                            ToList();
                        break;
                    case SearchOption.BetweenDates:
                        this.salesData = salesReportProvider.
                            GetSalesBetween(this.dateMain.Value, this.dateHelper.Value).
                            ToList();
                        break;
                }

                if (this.salesData.Count < 1)
                {
                    MessageBox.Show("No results");
                    return;
                }

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

        private void OnComboChanged(object sender, EventArgs e)
        {
            this.dateHelper.Enabled = (SearchOption)this.comboSearchBy.SelectedItem == SearchOption.BetweenDates;
        }
    }
}