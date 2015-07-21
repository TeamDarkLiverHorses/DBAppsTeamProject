namespace DatabaseManager.ImportSalesData.ImportToSqlServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DatabaseManager.Data;
    using DatabaseManager.Models;

    public class ImportToSql : IDisposable
    {
        private bool isDisposed = false;
        private SupermarketsContext superContext = null;

        public Dictionary<string, int> ExportDataFromOracle(ImportFromOracleDataHolder dataHolderOracle)
        {
            Dictionary<string, int> exportedData = new Dictionary<string, int>();

            const string ExportedCategories = "Exported categories";
            const string NotExportedCategories = "Categories already added";
            const string ExportedMeasure = "Exported measures";
            const string NotExportedMeasure = "Measures already added";
            const string ExportedVendors = "Exported vendors";
            const string NotExportedVendors = "Vendors already added";
            const string ExportedProducts = "Exported products";
            const string NotExportedProducts = "Products already added";

            exportedData.Add(ExportedCategories, 0);
            exportedData.Add(NotExportedCategories, 0);
            exportedData.Add(ExportedMeasure, 0);
            exportedData.Add(NotExportedMeasure, 0);
            exportedData.Add(ExportedVendors, 0);
            exportedData.Add(NotExportedVendors, 0);
            exportedData.Add(ExportedProducts, 0);
            exportedData.Add(NotExportedProducts, 0);
            
            try
            {
                using (superContext = new SupermarketsContext())
                {
                    var scTransaction = superContext.Database.BeginTransaction();

                    try
                    {
                        using (scTransaction)
                        {
                            foreach (string currentCategory in dataHolderOracle.Categories)
                            {
                                if (!superContext.Categories.Where(c => c.Name == currentCategory).Any())
                                {
                                    superContext.Categories.Add(new Category() { Name = currentCategory });
                                    
                                    exportedData[ExportedCategories] += 1;
                                }
                                else
                                {
                                    exportedData[NotExportedCategories] += 1;
                                }

                            }

                            superContext.SaveChanges();

                            foreach (string currentMeasure in dataHolderOracle.Measures)
                            {
                                if (!superContext.Measures.Where(m => m.Name == currentMeasure).Any())
                                {
                                    superContext.Measures.Add(new Measure() { Name = currentMeasure });
                                    
                                    exportedData[ExportedMeasure] += 1;
                                }
                                else
                                {
                                    exportedData[NotExportedMeasure] += 1;
                                }
                            }

                            superContext.SaveChanges();

                            foreach (string currentVendor in dataHolderOracle.Vendors)
                            {
                                if (!superContext.Vendors.Where(v => v.Name == currentVendor).Any())
                                {
                                    superContext.Vendors.Add(new Vendor() { Name = currentVendor });

                                    exportedData[ExportedVendors] += 1;
                                }
                                else
                                {
                                    exportedData[NotExportedVendors] += 1;
                                }
                            }

                            superContext.SaveChanges();

                            for (int i = 0; i < dataHolderOracle.Products.Length; i++)
                            {
                                Product currentProduct = dataHolderOracle.Products[i];

                                currentProduct.Category = superContext.Categories.Where(c => c.Name == currentProduct.Category.Name).First();
                                currentProduct.Measure = superContext.Measures.Where(m => m.Name == currentProduct.Measure.Name).First();
                                currentProduct.Vendor = superContext.Vendors.Where(v => v.Name == currentProduct.Vendor.Name).First();

                                if (!superContext.Products.Where(p => p.Name == currentProduct.Name).Any())
                                {
                                    superContext.Products.Add(currentProduct);

                                    exportedData[ExportedProducts] += 1;
                                }
                                else
                                {
                                    exportedData[NotExportedProducts] += 1;
                                }
                            }

                            superContext.SaveChanges();

                            scTransaction.Commit();
                        }

                        return exportedData;
                    }
                    catch (Exception innerEx)
                    {
                        try
                        {
                            scTransaction.Rollback();
                        }
                        catch (Exception rollBackEx)
                        {
                            throw new Exception("Error data was not rolled back.", rollBackEx);
                        }

                        throw new Exception("Error updating database. The data was rolled back.", innerEx);
                    }
                }
            }
            catch (Exception ex)
            {
                ClearData();

                throw ex;
            }
        }

        public int ExportDataFromExcel(ImportFromExcelDataHolder dataHolderOExcel)
        {
            int countExportedSales = 0;

            try
            {
                using (superContext = new SupermarketsContext())
                {
                    var scTransaction = superContext.Database.BeginTransaction();

                    try
                    {
                        using (scTransaction)
                        {
                            // this checks if all products exist in the database
                            foreach (string currentProductName in dataHolderOExcel.Products)
                            {
                                if (!superContext.Products.Where(p => p.Name == currentProductName).Any())
                                {
                                    throw new ArgumentNullException(string.Format("Product {0} does not exists in the database."));
                                }

                            }

                            // this checks if all shops exist in the database
                            foreach (string currentShopName in dataHolderOExcel.Shops)
                            {
                                // add shop if it not exists   
                                if (!superContext.Shops.Where(s => s.Name == currentShopName).Any())
                                {
                                    superContext.Shops.Add(new Shop() { Name = currentShopName });
                                }
                            }

                            superContext.SaveChanges();

                            // add all sales
                            for (int i = 0; i < dataHolderOExcel.Sales.Length; i++)
                            {
                                Sale currentSale = dataHolderOExcel.Sales[i];

                                currentSale.Product = superContext.Products.Where(p => p.Name == currentSale.Product.Name).First();
                                currentSale.Shop= superContext.Shops.Where(shop => shop.Name == currentSale.Shop.Name).First();

                                if (!superContext.Sales.Where(s => s.Date == currentSale.Date).Any())
                                {
                                    superContext.Sales.Add(currentSale);
                                }

                                countExportedSales += 1;
                            }

                            superContext.SaveChanges();

                            scTransaction.Commit();
                        }

                        return countExportedSales;
                    }
                    catch (Exception innerEx)
                    {
                        try
                        {
                            scTransaction.Rollback();
                        }
                        catch (Exception rollBackEx)
                        {
                            throw new Exception(innerEx.Message + Environment.NewLine + "Error data was not rolled back.", rollBackEx);
                        }

                        throw new Exception(innerEx.Message + Environment.NewLine + "Error updating database. The data was rolled back.", innerEx);
                    }
                }
            }
            catch (Exception ex)
            {
                ClearData();

                throw ex;
            }
        }

        private void ClearData()
        {
            if (superContext != null)
            {
                superContext.Dispose();
                superContext = null;
            }
        }

        public void Dispose()
        {
            Dispose(isDisposed);
        }

        private void Dispose(bool disposed)
        {
            if (!isDisposed)
            {
                ClearData();

                isDisposed = true;
            }
        }
    }
}