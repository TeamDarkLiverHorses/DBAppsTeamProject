namespace DatabaseManager.UI
{

    using DatabaseManager.MongoDbExport;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class JSONWindow : Form
    {
        public JSONWindow()
        {
            InitializeComponent();
            this.comboExportOptions.SelectedIndexChanged += SelectedIndexChanged;
            this.btnExport.Click += ExportJson;
            this.btnClear.Click += ClearInfo;
            this.comboExportOptions.DataSource = Enum.GetValues(typeof(SearchOption));
            this.mExit.Click += (o, s) => this.Close();
        }

        private void ExportJson(object sender, EventArgs e)
        {
            string path = string.Empty;

            try
            {
                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        path = folderDialog.SelectedPath;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (path != string.Empty)
            {
                this.ClearInfo(null, null);

                switch ((SearchOption)comboExportOptions.SelectedItem)
                {
                    case SearchOption.ExactDate:
                        Task.Run(() =>
                        {
                            try
                            {
                                MongoDbExporter mongoExporter = new MongoDbExporter();
                                int recordsAffected = mongoExporter.ExportProducSalesOn(dateSearch.Value, path);

                                this.listInfo.Invoke(new Action(() => { ResultInformation(recordsAffected); }));
                            }
                            catch (Exception ex)
                            {
                                this.Invoke(new Action(() => { MessageBox.Show(ex.Message); }));
                            }
                        });
                        break;
                    case SearchOption.BeforeDate:
                        Task.Run(() =>
                        {
                            try
                            {
                                MongoDbExporter exportToDb = new MongoDbExporter();
                                int recordsAffected = exportToDb.ExportProducSalesBefore(dateSearch.Value, path);

                                this.listInfo.Invoke(new Action(() => { ResultInformation(recordsAffected); }));
                            }
                            catch (Exception ex)
                            {
                                this.Invoke(new Action(() => { MessageBox.Show(ex.Message); }));
                            }
                        });
                        break;
                    case SearchOption.AfterDate:
                        Task.Run(() =>
                        {
                            try
                            {
                                MongoDbExporter exportToDb = new MongoDbExporter();
                                int recordsAffected = exportToDb.ExportProducSalesAfter(dateSearch.Value, path);

                                this.listInfo.Invoke(new Action(() => { ResultInformation(recordsAffected); }));
                            }
                            catch (Exception ex)
                            {
                                this.Invoke(new Action(() => { MessageBox.Show(ex.Message); }));
                            }
                        });
                        break;
                    case SearchOption.BetweenDates:
                        Task.Run(() =>
                        {
                            try
                            {
                                MongoDbExporter exportToDb = new MongoDbExporter();
                                int recordsAffected = exportToDb.ExportProducSalesBetween(dateSearch.Value, dateHelper.Value, path);

                                this.listInfo.Invoke(new Action(() => { ResultInformation(recordsAffected); }));
                            }
                            catch (Exception ex)
                            {
                                this.Invoke(new Action(() => { MessageBox.Show(ex.Message); }));
                            }
                        });
                        break;
                    default:
                        MessageBox.Show("No such option.");
                        break;
                }
            }
        }

        private void ResultInformation(int count)
        {
            this.listInfo.Items.Add(string.Format("Exported {0} documents...", count));
        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dateHelper.Enabled = (SearchOption)comboExportOptions.SelectedItem == SearchOption.BetweenDates ? true : false;
        }

        private void ClearInfo(object sender, EventArgs e)
        {
            this.listInfo.Items.Clear();
        }
    }
}
