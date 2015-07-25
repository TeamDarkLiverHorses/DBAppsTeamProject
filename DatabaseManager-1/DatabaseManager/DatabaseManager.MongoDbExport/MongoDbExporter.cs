namespace DatabaseManager.MongoDbExport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DatabaseManager.SalesReports;
    using MongoDB.Bson;
    using MongoDB.Driver;
    
    public class MongoDbExporter
    {
        private string connectionUri = @"mongodb://admin:123456@ds036648.mongolab.com:36648/supermarkets";
        private MongoClient mnogoClient;
        private IMongoDatabase supermarketsDb;
        private SalesReportForPeriod reportGenerator;

        public MongoDbExporter()
        {
            this.mnogoClient = new MongoClient(connectionUri);
            this.supermarketsDb = this.mnogoClient.GetDatabase("supermarkets");
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

        async private Task ExportToMongo(IEnumerable<ProductSales> sales)
        {
            List<BsonDocument> documentsToExport = new List<BsonDocument>();
            
            foreach (var entry in sales)
            {
                documentsToExport.Add(CreateBson(entry));
            }

            var collection = supermarketsDb.GetCollection<BsonDocument>("SalesByProductReports");
            await collection.InsertManyAsync(documentsToExport);   
        }

        private BsonDocument CreateBson(ProductSales productSales)
        {
            var document = new BsonDocument
            {
                { "product-id" , productSales.ProductId },
                { "product-name" , productSales.ProductName },
                { "vendor-name" , productSales.VendorName },
                { "total-quantity-sold" , productSales.TotalQuantitySold },
                { "total-incomes" , productSales.TotalIncome.ToBson() }
            };
            return document;
        }
    }
}
