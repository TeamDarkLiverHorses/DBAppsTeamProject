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
            this.btnCreateXmlReports.Click += ShowXMLExportWindow;
            this.btnJSON.Click += ShowJSONExport;
        }

        public void StartOracleForm(object sender, EventArgs e)
        {
            using (var frmOracle = new DBManagerWindow())
            {
                frmOracle.ShowDialog(this);
            }
        }

        public void ExportOracleDB(object sender, EventArgs e)
        {
            using (var importWindow = new ImportToSQLWindow())
            {
                importWindow.ShowDialog(this);
            }
        }

        public void ExportToPdf(object sender, EventArgs e)
        {
            using (var exportToPdfWindow = new ExportToPdfWindow())
            {
                exportToPdfWindow.ShowDialog(this);
            }
        }

        private void ShowXMLExportWindow(object sender, EventArgs e)
        {
            using (var exportToPdfWindow = new ExportToXMLWindow())
            {
                exportToPdfWindow.ShowDialog(this);
            }
        }

        private void ShowJSONExport(object sender, EventArgs e)
        {
            using (JSONWindow jsonWindow = new JSONWindow())
            {
                jsonWindow.ShowDialog(this);
            }
        }
    }
}
