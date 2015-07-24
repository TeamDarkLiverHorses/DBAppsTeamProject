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

                    var salesToInsert = this.extractor.Sales.
                        Except(context.Sales);
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
