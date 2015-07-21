namespace DatabaseManager.ImportSalesData.ImportToSqlServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DatabaseManager.Data;
    using DatabaseManager.Models;

    public static class ImportToSql
    {
        public static Dictionary<string, int> ImportFromOracleToMSSql(ImportFromOracleDataHolder dataHolderOracle)
        {
            const string ExportedCategories = "Exported categories";
            const string NotExportedCategories = "Categories already added";
            const string ExportedMeasure = "Exported measures";
            const string NotExportedMeasure = "Measures already added";
            const string ExportedVendors = "Exported vendors";
            const string NotExportedVendors = "Vendors already added";
            const string ExportedProducts = "Exported products";
            const string NotExportedProducts = "Products already added";

            using (var context = new SupermarketsContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var exportedData = new Dictionary<string, int>();

                    var categoriesToImport = dataHolderOracle.Categories.
                        Except(context.Categories.Select(c => c.Name)).
                        Select(c => new Category() { Name = c });
                    context.Categories.AddRange(categoriesToImport);
                    exportedData.Add(ExportedCategories, categoriesToImport.Count());
                    exportedData.Add(NotExportedCategories, dataHolderOracle.Categories.Count - categoriesToImport.Count());

                    var measuresToImport = dataHolderOracle.Measures.
                        Except(context.Measures.Select(m => m.Name)).
                        Select(m => new Measure() { Name = m });
                    context.Measures.AddRange(measuresToImport);
                    exportedData.Add(ExportedMeasure, measuresToImport.Count());
                    exportedData.Add(NotExportedMeasure, dataHolderOracle.Measures.Count - measuresToImport.Count());

                    var vendorsToImport = dataHolderOracle.Vendors.
                        Except(context.Vendors.Select(v => v.Name)).
                        Select(v => new Vendor() { Name = v });
                    context.Vendors.AddRange(vendorsToImport);
                    exportedData.Add(ExportedVendors, vendorsToImport.Count());
                    exportedData.Add(NotExportedVendors, dataHolderOracle.Vendors.Count - vendorsToImport.Count());

                    var productsToImport = dataHolderOracle.Products.Except(context.Products);
                    context.Products.AddRange(productsToImport);
                    exportedData.Add(ExportedProducts, productsToImport.Count());
                    exportedData.Add(NotExportedProducts, dataHolderOracle.Products.Length - productsToImport.Count());
                    
                    context.SaveChanges();
                    transaction.Commit();
                    return exportedData;
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
                        throw new Exception("Error data was not rolled back.", rollBackEx);
                    }

                    throw new Exception("Error updating database. The data was rolled back.", innerEx);
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
                        throw new Exception(string.Format("Products {0} does not exist in the database.",
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
                        throw new Exception(innerEx.Message + Environment.NewLine + "Error data was not rolled back.", rollBackEx);
                    }

                    throw new Exception(innerEx.Message + Environment.NewLine + "Error updating database. The data was rolled back.", innerEx);
                }
            }
        }
    }
}