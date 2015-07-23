namespace DatabaseManager.ImportSalesData.ImportToSqlServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DatabaseManager.Data;
    using DatabaseManager.Models;
    using DatabaseManager.ImportSalesData.Utilities;

    public static class ImportToSql
    {
        public static Dictionary<string, int> ImportFromOracleToMSSql(ImportFromOracleDataHolder dataHolderOracle)
        {
            using (var context = new SupermarketsContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var exportOperationInfo = new Dictionary<string, int>();

                    var categoriesToImport = dataHolderOracle.Categories
                        //.Where(categoryInOracle => categoryInOracle.)
                        .Except(context.Categories.Select(c => c.Name))
                        .Select(c => new Category() { Name = c });

                    context.Categories.AddRange(categoriesToImport);

                    exportOperationInfo.Add(Messages.ExportedCategories, categoriesToImport.Count());
                    exportOperationInfo.Add(Messages.NotExportedCategories, dataHolderOracle.Categories.Count - categoriesToImport.Count());

                    var measuresToImport = dataHolderOracle.Measures.
                        Except(context.Measures.Select(m => m.Name)).
                        Select(m => new Measure() { Name = m });
                    context.Measures.AddRange(measuresToImport);
                    exportOperationInfo.Add(Messages.ExportedMeasure, measuresToImport.Count());
                    exportOperationInfo.Add(Messages.NotExportedMeasure, dataHolderOracle.Measures.Count - measuresToImport.Count());

                    var vendorsToImport = dataHolderOracle.Vendors.
                        Except(context.Vendors.Select(v => v.Name)).
                        Select(v => new Vendor() { Name = v });
                    context.Vendors.AddRange(vendorsToImport);
                    exportOperationInfo.Add(Messages.ExportedVendors, vendorsToImport.Count());
                    exportOperationInfo.Add(Messages.NotExportedVendors, dataHolderOracle.Vendors.Count - vendorsToImport.Count());

                    var productsToImport = dataHolderOracle.Products.Except(context.Products);
                    context.Products.AddRange(productsToImport);
                    exportOperationInfo.Add(Messages.ExportedProducts, productsToImport.Count());
                    exportOperationInfo.Add(Messages.NotExportedProducts, dataHolderOracle.Products.Length - productsToImport.Count());
                    
                    context.SaveChanges();
                    transaction.Commit();
                    return exportOperationInfo;
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
                        throw new Exception(Messages.ErrorDuringRollback, rollBackEx);
                    }

                    throw new Exception(Messages.ErrorUpdatingSQLDatabase, innerEx);
                }
            }
        }

        public static int ExportDataFromExcel(ImportFromExcelDataHolder dataHolderOExcel)
        {
            using (var context = new SupermarketsContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var productsNotInDB = dataHolderOExcel.Products.
                        Except(context.Products.Select(p => p.Name));
                    if (productsNotInDB.Count() > 0)
                    {
                        throw new Exception(string.Format(Messages.NonExistingProduct,
                            string.Join(", ", productsNotInDB)));
                    }

                    var shopsToInsert = dataHolderOExcel.Shops.
                        Except(context.Shops.Select(s => s.Name)).
                        Select(s => new Shop() { Name = s });
                    context.Shops.AddRange(shopsToInsert);

                    var salesToInsert = dataHolderOExcel.Sales.
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