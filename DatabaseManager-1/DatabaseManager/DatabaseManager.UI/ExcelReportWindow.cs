namespace DatabaseManager.UI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using DatabaseManager.VendorReports;
    
    public partial class ExcelReportWindow : Form
    {
        public ExcelReportWindow()
        {
            InitializeComponent();
            this.btnExportExcel.Click += ExportExcel;
            this.btnClear.Click += ClearInfo;
            this.mExit.Click += (s, e) => this.Close();
        }

        private void ExportExcel(object sender, EventArgs e)
        {
            string filePath = string.Empty;

            try
            {
                using (SaveFileDialog saveFile = new SaveFileDialog())
                {
                    saveFile.Filter = "Excel (*.xlsx)|*.xslx";

                    if (saveFile.ShowDialog() == DialogResult.OK)
                    {
                        filePath = saveFile.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                filePath = string.Empty;
                MessageBox.Show(ex.Message);
            }

            if (filePath != string.Empty)
            {
                ClearInfo(null, null);
                
                Task.Run(() => 
                {
                    try
                    {
                        var reportManager = new VendorProfitReport();

                        var profits = reportManager.CalculateVendorsProfits();

                        this.Invoke(new Action(() => LoadData(profits)));

                        reportManager.CreateReport(filePath);
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() => MessageBox.Show(ex.Message)));
                    }
                });
            }
        }

        private void LoadData(IEnumerable<VendorProfit> profits)
        {
            foreach (var profit in profits)
            {
                int index = dataGridView.Rows.Add();

                var row = this.dataGridView.Rows[index];

                row.Cells["columnName"].Value = profit.VendorName;
                row.Cells["columnTaxes"].Value = profit.VendorTaxes;
                row.Cells["columnIncomes"].Value = profit.VendorIncome;
                row.Cells["columnExpenses"].Value = profit.VendorExpenses;
                row.Cells["columnProfit"].Value = profit.Profit;
            }
        }

        private void ClearInfo(object sender, EventArgs e)
        {
            this.dataGridView.Rows.Clear();
        }
    }
}
