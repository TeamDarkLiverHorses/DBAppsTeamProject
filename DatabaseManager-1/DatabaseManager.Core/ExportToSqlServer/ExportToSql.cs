using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DatabaseManager.Core.Models;
using DatabaseManager.Core.OracleConnectionDB;

namespace DatabaseManager.Core.ExportToSqlServer
{
    public class ExportToSql : IDisposable
    {
        private bool isDisposed = false;

        HashSet<string> categories = new HashSet<string>();
        HashSet<string> measures = new HashSet<string>();
        HashSet<string> vendors = new HashSet<string>();
        Product[] products;

        private SupermarketsContext superContext = null;

        public Dictionary<string, int> BuildDataToExport(DataTable tableProducts, string productNameColumn, string productPriceColumn,
            string categoryNameColumn, string measureNameColumn, string vendorNameColumn)
        {
           try
           {
               Dictionary<string, int> countTablesValueToExport = new Dictionary<string, int>();

               BuildData buildTables = new BuildData();

               ExportDataHolder dataHolder = buildTables.BuildProducts(tableProducts, "PRODUCTNAME", "PRODUCTPRICE",
                   "CATEGORYNAME", "MEASURENAME", "VENDORNAME");

               categories = dataHolder.Categories;
               measures = dataHolder.Measures;
               vendors = dataHolder.Vendors;
               products = dataHolder.Products;

               dataHolder = null;

               countTablesValueToExport.Add("Categories to export", categories.Count);
               countTablesValueToExport.Add("Measures to export", measures.Count);
               countTablesValueToExport.Add("Vendors to export", vendors.Count);
               countTablesValueToExport.Add("Products to export", products.Length);

               return countTablesValueToExport;
           }
            catch (FormatException fEx)
           {
               throw fEx;
           }
            catch (Exception ex)
           {
               throw ex;
           }
        }

        public Dictionary<string, int> ExportData()
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
                            foreach (string currentCategory in categories)
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

                            foreach (string currentMeasure in measures)
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

                            foreach (string currentVendor in vendors)
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

                            for (int i = 0; i < products.Length; i++)
                            {
                                Product currentProduct = products[i];

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
