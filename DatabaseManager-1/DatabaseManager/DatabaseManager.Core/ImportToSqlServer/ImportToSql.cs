using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DatabaseManager.Core.Models;
using DatabaseManager.Core.OracleConnectionDB;

namespace DatabaseManager.Core.ImportToSqlServer
{
    public class ImportToSql : IDisposable
    {
        private bool isDisposed = false;

        private SupermarketsContext superContext = null;

        public Dictionary<string, int> ExportDataFromOracle(ImportFromOracleDataHolder dataHolderOracle)
        {
            Dictionary<string, int> exportedData = new Dictionary<string, int>();

            string exportedCategories = "Exported categories";
            string notExportedCategories = "Categories already added";
            string exportedMeasure = "Exported measures";
            string notExportedMeasure = "Measures already added";
            string exportedVendors = "Exported vendors";
            string notExportedVendors = "Vendors already added";
            string exportedProducts = "Exported products";
            string notExportedProducts = "Products already added";

            exportedData.Add(exportedCategories, 0);
            exportedData.Add(notExportedCategories, 0);
            exportedData.Add(exportedMeasure, 0);
            exportedData.Add(notExportedMeasure, 0);
            exportedData.Add(exportedVendors, 0);
            exportedData.Add(notExportedVendors, 0);
            exportedData.Add(exportedProducts, 0);
            exportedData.Add(notExportedProducts, 0);
            
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
                                    
                                    exportedData[exportedCategories] += 1;
                                }
                                else
                                {
                                    exportedData[notExportedCategories] += 1;
                                }

                            }

                            superContext.SaveChanges();

                            foreach (string currentMeasure in dataHolderOracle.Measures)
                            {
                                if (!superContext.Measures.Where(m => m.Name == currentMeasure).Any())
                                {
                                    superContext.Measures.Add(new Measure() { Name = currentMeasure });
                                    
                                    exportedData[exportedMeasure] += 1;
                                }
                                else
                                {
                                    exportedData[notExportedMeasure] += 1;
                                }
                            }

                            superContext.SaveChanges();

                            foreach (string currentVendor in dataHolderOracle.Vendors)
                            {
                                if (!superContext.Vendors.Where(v => v.Name == currentVendor).Any())
                                {
                                    superContext.Vendors.Add(new Vendor() { Name = currentVendor });

                                    exportedData[exportedVendors] += 1;
                                }
                                else
                                {
                                    exportedData[notExportedVendors] += 1;
                                }
                            }

                            superContext.SaveChanges();

                            for (int i = 0; i < dataHolderOracle.Products.Length; i++)
                            {
                                Product currentProduct = dataHolderOracle.Products[i];

                                currentProduct.Category = superContext.Categories.Where(c => c.Name == currentProduct.Category.Name).First();
                                currentProduct.Measure = superContext.Measures.Where(m => m.Name == currentProduct.Measure.Name).First();
                                currentProduct.Vendor = superContext.Vendors.Where(v => v.Name == currentProduct.Vendor.Name).First();

                                if (!superContext.Products.Where(p => p.Name == currentProduct.Name &&
                                    p.Price == currentProduct.Price && p.CategoryId == currentProduct.Category.Id &&
                                    p.MeasureId == currentProduct.Measure.Id && p.VendorId == currentProduct.Vendor.Id).Any())
                                {
                                    superContext.Products.Add(currentProduct);

                                    exportedData[exportedProducts] += 1;
                                }
                                else
                                {
                                    exportedData[notExportedProducts] += 1;
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

                                superContext.Sales.Add(currentSale);

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
