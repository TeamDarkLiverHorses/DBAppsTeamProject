namespace DatabaseManager.ImportSalesData.ImportToSqlServer
{
    using System;
    using System.Linq;
    using DatabaseManager.Data;
    using DatabaseManager.Models;
    using DatabaseManager.ImportSalesData.Utilities;

    public class ExcelDataImporter
    {
        private ExcellDataExtractor extractor;

        public ExcelDataImporter(ExcellDataExtractor extractor)
        {
            this.extractor = extractor;
        }

        public int Import()
        {
            using (var context = new SupermarketsContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var productsNotInDB = this.extractor.Products.
                        Except(context.Products.Select(p => p.Name));
                    if (productsNotInDB.Count() > 0)
                    {
                        throw new Exception(string.Format(Messages.NonExistingProduct,
                            string.Join(", ", productsNotInDB)));
                    }

                    var shopsToInsert = this.extractor.Shops.
                        Except(context.Shops.Select(s => s.Name)).
                        Select(s => new Shop() { Name = s });
                    context.Shops.AddRange(shopsToInsert);
                    context.SaveChanges();

                    var salesToInsert = this.extractor.Sales
                        .Select(s => new Sale
                        {
                            ProductId = context.Products.Where(p => p.Name == s.Product.Name).Select(p => p.Id).FirstOrDefault(),
                            ShopId = context.Shops.Where(shop => shop.Name == s.Shop.Name).Select(shop => shop.Id).FirstOrDefault(),
                            Quantity = s.Quantity,
                            UnitPrice = s.UnitPrice,
                            Date = s.Date
                        })
                        .Where(s => !context.Sales.Any(es => es.ProductId == s.ProductId && es.Date == s.Date && es.ShopId == s.ShopId))
                        .ToList();

                    context.Sales.AddRange(salesToInsert);

                    context.SaveChanges();
                    transaction.Commit();
                    return salesToInsert.Count();
                }
                // TODO: Change to more concrete exceptions
                catch (Exception innerEx)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception rollBackEx)
                    {
                        throw new Exception(innerEx.Message + Environment.NewLine + Messages.ErrorDuringRollback, rollBackEx);
                    }

                    throw new Exception(innerEx.Message + Environment.NewLine + Messages.ErrorUpdatingSQLDatabase, innerEx);
                }
            }
        }
    }
}
