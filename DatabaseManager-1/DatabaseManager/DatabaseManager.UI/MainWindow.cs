namespace DatabaseManager.UI
{
    using System;
    using System.Windows.Forms;

    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            this.mExit.Click += (s, e) => this.Close();
            this.btnOracle.Click += StartOracleForm;
            this.btnExportOracle.Click += ExportOracleDB;
            this.btnExportPdf.Click += ExportToPdf;
        }

        public void StartOracleForm(object sender, EventArgs e)
        {
            DBManagerWindow frmOracle = new DBManagerWindow();

            using (frmOracle)
            {
                frmOracle.ShowDialog(this);
            }
        }

        public void ExportOracleDB(object sender, EventArgs e)
        {
            ImportToSQLWindow importWindow = new ImportToSQLWindow();

            using (importWindow)
            {
                importWindow.ShowDialog(this);
            }
        }

        public void ExportToPdf(object sender, EventArgs e)
        {
            ExportToPdfWindow exportToPdfWindow = new ExportToPdfWindow();

            using (exportToPdfWindow)
            {
                exportToPdfWindow.ShowDialog(this);
            }
        }
    }
}
