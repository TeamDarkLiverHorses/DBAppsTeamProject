namespace DatabaseManager.MongoDbExport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DatabaseManager.SalesReports;
    using MongoDB;

    public class MongoDbExporterOld
    {
        private SalesReportForPeriod reportGenerator;
        private string directoryPath;

        public MongoDbExporterOld()
        {
            this.reportGenerator = new SalesReportForPeriod();
        }

        public int ExportProducSalesBetween(DateTime startDate, DateTime endDate, string directory)
        {
            directoryPath = directory;
            var productSales = reportGenerator.GetProductSalesBetween(startDate, endDate);
            ExportToMongo(productSales);
            return productSales.Count();
        }

        public int ExportProducSalesAfter(DateTime startDate, string directory)
        {
            directoryPath = directory;
            var productSales = reportGenerator.GetProductSalesAfter(startDate);
            ExportToMongo(productSales);
            return productSales.Count();
        }

        public int ExportProducSalesBefore(DateTime endDate, string directory)
        {
            directoryPath = directory;
            var productSales = reportGenerator.GetProductSalesBefore(endDate);
            ExportToMongo(productSales);
            return productSales.Count();
        }

        public int ExportProducSalesOn(DateTime date, string directory)
        {
            directoryPath = directory;
            var productSales = reportGenerator.GetProductSalesOn(date);
            ExportToMongo(productSales);
            return productSales.Count();
        }

        private int ExportToMongo(IEnumerable<ProductSales> sales)
        {
            List<MongoDB.Document> documentsToExport = new List<MongoDB.Document>();

            foreach (var entry in sales)
            {
                var doc = CreateDocument(entry);
                documentsToExport.Add(doc);
            }

            var mongo = new Mongo();
            mongo.Connect();
            var db = mongo.GetDatabase("supermarkets");

            var collection = db.GetCollection("SalesByProductReports");
            collection.Insert(documentsToExport);

            mongo.Disconnect();

            var jsonCreator = new JsonCreator();
            jsonCreator.WriteJsonFiles(documentsToExport, directoryPath);

            return documentsToExport.Count();
        }

        private MongoDB.Document CreateDocument(ProductSales productSales)
        {
            var document = new MongoDB.Document();
            document.Add("product-id", productSales.ProductId);
            document.Add("product-name", productSales.ProductName);
            document.Add("vendor-name", productSales.VendorName);
            document.Add("total-quantity-sold", productSales.TotalQuantitySold);
            document.Add("total-incomes", productSales.TotalIncome);

            return document;
        }
    }
}
