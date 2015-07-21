namespace DatabaseManager.UI
{
    using System;
    using System.Windows.Forms;

    static class DBManager
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
