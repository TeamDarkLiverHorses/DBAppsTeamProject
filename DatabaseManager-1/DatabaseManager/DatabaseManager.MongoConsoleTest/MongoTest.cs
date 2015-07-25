namespace DatabaseManager.MongoConsoleTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DatabaseManager.MongoDbExport;
    using DatabaseManager.SalesReports;

    public class MongoTest
    {
        public static void Main()
        {
            MongoDbExporter exporter = new MongoDbExporter();
            int recordsAffected = exporter.ExportProducSalesBefore(DateTime.Now);
            Console.WriteLine("Exported {0} documents...", recordsAffected);
        }
    }
}
