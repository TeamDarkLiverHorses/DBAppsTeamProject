namespace DatabaseManager.MongoConsoleTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DatabaseManager.MongoDbExport;
    using DatabaseManager.SalesReports;
    using MongoDB;
    using MongoDB.Linq;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class MongoTest
    {
        public static void Main()
        {
            MongoDbExporterOld exporter = new MongoDbExporterOld();
            int recordsAffected = exporter.ExportProducSalesBefore(DateTime.Now);
            Console.WriteLine("Exported {0} documents...", recordsAffected);
        }
    }
}
