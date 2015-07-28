namespace DatabaseManager.MongoConsoleTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DatabaseManager.MongoDbExport;
    using DatabaseManager.SalesReports;
    using MongoDB;
    using MongoDB.Driver.Linq;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class MongoTest
    {
        public static void Main()
        {
            var exporter = new MongoDbExporter();
            int recordsAffected = exporter.ExportProducSalesBefore(DateTime.Now, "");
            Console.WriteLine("Exported {0} documents...", recordsAffected);
            //var task = exporter.GetSales();
            //System.Threading.Tasks.Task.WaitAll(task);
            //foreach (var item in task.Result)
            //{
            //    Console.WriteLine(item);
            //}
        }
    }
}
