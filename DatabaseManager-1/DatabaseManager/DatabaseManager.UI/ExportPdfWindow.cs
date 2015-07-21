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
    using DatabaseManager.Data;

    public partial class ExportPdfWindow : Form
    {
        const string ONDATE = "On date";
        const string BEFOREDATE = "Before date";
        const string AFTERDATE = "After date";
        const string BEETWENDATES = "Between dates";

        public ExportPdfWindow()
        {
            InitializeComponent();

            this.comboSearchBy.SelectedIndexChanged += SearchOption;

            this.comboSearchBy.DataSource = new string[] { ONDATE, BEFOREDATE, AFTERDATE, BEETWENDATES };
            
            this.btnSearch.Click += Search;
            this.btnExport.Click += ExportToPdf;
        }

        private void Search(object sender, EventArgs e)
        {
            if (this.comboSearchBy.DataSource != null && this.comboSearchBy.Items.Count > 0)
            {
                switch (this.comboSearchBy.SelectedItem.ToString())
                {
                    case ONDATE:
                        DateTime onDate = this.dateMain.Value;
                        // to do
                        break;
                    case BEFOREDATE:
                        DateTime beforeDate = this.dateMain.Value;
                        // to do
                        break;
                    case AFTERDATE:
                        DateTime afterDate = this.dateMain.Value;
                        // to do
                        break;
                    case BEETWENDATES:
                        DateTime startDate = this.dateMain.Value;
                        DateTime endDate = this.dateHelper.Value;
                        // to do
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
            // export;
        }

        private void SearchOption(object sender, EventArgs e)
        {
            if (this.comboSearchBy.DataSource != null && this.comboSearchBy.Items.Count > 0)
            {
                if (this.comboSearchBy.SelectedItem.ToString() != BEETWENDATES)
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
