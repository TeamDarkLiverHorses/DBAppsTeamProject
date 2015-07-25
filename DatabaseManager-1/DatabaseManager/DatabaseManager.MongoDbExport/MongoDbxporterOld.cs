namespace DatabaseManager.MongoDbExport
{
    using DatabaseManager.SalesReports;
    using MongoDB;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MongoDbExporterOld
    {
        private SalesReportForPeriod reportGenerator;

        public MongoDbExporterOld()
        {
            this.reportGenerator = new SalesReportForPeriod();
        }

        public int ExportProducSalesBetween(DateTime startDate, DateTime endDate)
        {
            var productSales = reportGenerator.GetProductSalesBetween(startDate, endDate);
            ExportToMongo(productSales);
            return productSales.Count();
        }

        public int ExportProducSalesAfter(DateTime startDate)
        {
            var productSales = reportGenerator.GetProductSalesAfter(startDate);
            ExportToMongo(productSales);
            return productSales.Count();
        }

        public int ExportProducSalesBefore(DateTime endDate)
        {
            var productSales = reportGenerator.GetProductSalesBefore(endDate);
            ExportToMongo(productSales);
            return productSales.Count();
        }

        public int ExportProducSalesOn(DateTime date)
        {
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
            jsonCreator.WriteJsonFiles(documentsToExport);

            return documentsToExport.Count();
        }

        private MongoDB.Document CreateDocument(ProductSales productSales)
        {
            var document = new MongoDB.Document();
            document.Add("product-id" , productSales.ProductId);
            document.Add("product-name" , productSales.ProductName );
            document.Add("vendor-name" , productSales.VendorName);
            document.Add("total-quantity-sold" , productSales.TotalQuantitySold);
            document.Add("total-incomes" , productSales.TotalIncome );
            
            return document;
        }
    }
}
