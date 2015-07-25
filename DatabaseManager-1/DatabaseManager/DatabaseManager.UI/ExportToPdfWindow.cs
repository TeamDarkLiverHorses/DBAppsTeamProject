namespace DatabaseManager.UI
{
    using DatabaseManager.Models;
    using DatabaseManager.SalesReports;
    using System;
    using System.Linq;
    using System.Windows.Forms;

    public partial class ExportToPdfWindow : Form
    {
        private Sale[] sales = null;

        public ExportToPdfWindow()
        {
            InitializeComponent();
            this.comboSearchBy.SelectedIndexChanged += OnComboChanged;
            this.comboSearchBy.DataSource = Enum.GetValues(typeof(SearchOption));
            this.btnSearch.Click += Search;
            this.btnExport.Click += ExportToPDF;
            this.btnExportSelection.Click += ExportSelectionToPDF;
        }

        private void UpdateSalesGrid()
        {
            System.Diagnostics.Debug.Assert(this.sales != null);

            this.dataGridViewSales.Rows.Clear();
            foreach (var sale in this.sales)
            {
                int index = this.dataGridViewSales.Rows.Add();
                var row = this.dataGridViewSales.Rows[index];
                row.Cells["columnProduct"].Value = sale.Product.Name;
                row.Cells["columnQuantity"].Value = sale.Quantity;
                row.Cells["columnPrice"].Value = sale.UnitPrice;
                row.Cells["columnLocation"].Value = sale.Shop.Name;
                row.Cells["columnDate"].Value = sale.Date;
            }

            if (this.sales.Count() == 0)
            {
                MessageBox.Show("No results");
            }
            else
            {
                this.dataGridViewSales.Rows[0].Selected = true;
            }
        }

        private void Search(object sender, EventArgs e)
        {
            var searchOption = (SearchOption)this.comboSearchBy.SelectedItem;
            System.Threading.Tasks.Task.Run(() =>
            {
                using (var salesReportProvider = new SalesReportForPeriod())
                {
                    switch (searchOption)
                    {
                        case SearchOption.ExactDate:
                            this.sales = salesReportProvider.
                                GetSalesOn(this.dateMain.Value).
                                ToArray();
                            break;
                        case SearchOption.BeforeDate:
                            this.sales = salesReportProvider.
                                GetSalesBefore(this.dateMain.Value).
                                ToArray();
                            break;
                        case SearchOption.AfterDate:
                            this.sales = salesReportProvider.
                                GetSalesAfter(this.dateMain.Value).
                                ToArray();
                            break;
                        case SearchOption.BetweenDates:
                            this.sales = salesReportProvider.
                                GetSalesBetween(this.dateMain.Value, this.dateHelper.Value).
                                ToArray();
                            break;
                    }

                    this.dataGridViewSales.Invoke((Action)UpdateSalesGrid);
                }
            });
        }

        private void ExportToPDF(object sender, EventArgs e)
        {
            if (this.sales.Length == 0)
            {
                MessageBox.Show("The sales table is empty.");
            }
            else
            {
                ExportPDF();
            }
        }

        private void ExportSelectionToPDF(object sender, EventArgs e)
        {
            if (this.sales.Length == 0)
            {
                MessageBox.Show("The sales table is empty.");
                return;
            }

            if (this.dataGridViewSales.SelectedRows.Count == 0)
            {
                MessageBox.Show("No selection.");
                return;
            }

            int selectedSalesCount = this.dataGridViewSales.SelectedRows.Count;
            var selectedSales = new Sale[selectedSalesCount];
            for (int i = 0; i < selectedSalesCount; i++)
            {
                int selectedRowIndex = this.dataGridViewSales.SelectedRows[i].Index;
                selectedSales[i] = this.sales[selectedRowIndex];
            }

            ExportPDF(selectedSales);
        }

        private void ExportPDF(Sale[] sales = null)
        {
            string filePath = string.Empty;
            using (SaveFileDialog saveFile = new SaveFileDialog())
            {
                saveFile.Filter = "PDF (*.pdf)|*.pdf";
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFile.FileName;
                }
                else
                {
                    return;
                }
            }

            System.Threading.Tasks.Task.Run(() =>
            {
                var report = new PdfReport(filePath);
                report.Create(sales ?? this.sales);
                MessageBox.Show("Done!");
            });
        }

        private void OnComboChanged(object sender, EventArgs e)
        {
            this.dateHelper.Enabled = (SearchOption)this.comboSearchBy.SelectedItem == SearchOption.BetweenDates;
        }
    }
}