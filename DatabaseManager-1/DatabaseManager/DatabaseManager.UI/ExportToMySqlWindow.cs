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
    using DatabaseManager.MySqlImporter;

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
                    ExpenseIncomeExporter mySqlExporter = new ExpenseIncomeExporter();

                    string result = mySqlExporter.ImportDataToMySql();

                    this.Invoke(new Action(() => { this.txtInfo.Text = result; }));
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
