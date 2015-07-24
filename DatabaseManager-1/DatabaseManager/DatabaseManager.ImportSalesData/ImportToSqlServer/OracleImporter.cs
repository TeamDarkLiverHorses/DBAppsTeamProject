namespace DatabaseManager.ImportSalesData.ImportToSqlServer
{
    using System;
    using System.Linq;
    using DatabaseManager.Data;
    using DatabaseManager.Models;
    using DatabaseManager.ImportSalesData.OracleConnectionsDB;
    using DatabaseManager.ImportSalesData.NativeSQL;
    using DatabaseManager.ImportSalesData.Utilities;
    using System.Data;
    using System.Data.Entity;

    public class OracleImporter
    {
        private SupermarketsContext context;

        public OracleImporter()
        {
            this.context = new SupermarketsContext();
        }

        public string ImportVendors()
        {
            string vendorName = "";
            int addedVendorsCount = 0;

            try
            {
                OracleSelectConnection oracleConnection = new OracleSelectConnection();
                var oracleTable = oracleConnection.SelectData(NativeOracleSQLCommands.GetVendors, null);

                if (oracleTable == null)
                {
                    return Messages.NoVendorsInOracleDatabase;
                }

                foreach (DataRow row in oracleTable.Rows)
                {
                    vendorName = row["Name"].ToString();
                    if (!this.context.Vendors.Any(v => v.Name == vendorName ))
                    {
                        context.Vendors.Add(new Vendor()
                        {
                            Name = vendorName
                        });
                        addedVendorsCount++;
                    }
                }
                context.SaveChanges();

                return string.Format(Messages.AddedVendors, addedVendorsCount);
            }
            catch (FormatException formatException)
            {
                return formatException.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ImportMeasures()
        {
            string measureName = "";
            int addedMeasuresCount = 0;

            try
            {
                OracleSelectConnection oracleConnection = new OracleSelectConnection();
                var oracleTable = oracleConnection.SelectData(NativeOracleSQLCommands.GetMeasures, null);

                if (oracleTable == null)
                {
                    return Messages.NoMeasuresInOracleDatabase;
                }

                foreach (DataRow row in oracleTable.Rows)
                {
                    measureName = row["Name"].ToString();
                    if (!this.context.Measures.Any(m => m.Name == measureName))
                    {
                        context.Measures.Add(new Measure()
                        {
                            Name = measureName
                        });
                        addedMeasuresCount++;
                    }
                }
                context.SaveChanges();

                return string.Format(Messages.AddedMeasures, addedMeasuresCount);
            }
            catch (FormatException formatException)
            {
                return formatException.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ImportCategories()
        {
            string categoryName = "";
            int addedCategoriesCount = 0;

            try
            {
                OracleSelectConnection oracleConnection = new OracleSelectConnection();
                var oracleTable = oracleConnection.SelectData(NativeOracleSQLCommands.GetCategories, null);

                if (oracleTable == null)
                {
                    return Messages.NoCategoriesInOracleDatabase;
                }

                foreach (DataRow row in oracleTable.Rows)
                {
                    categoryName = row["NAME"].ToString();
                    
                    if (!this.context.Categories.Any(c => c.Name == categoryName))
                    {
                        context.Categories.Add(new Category()
                        {
                            Name = categoryName,
                            ParentCategoryId = null
                        });
                        addedCategoriesCount++;
                    }
                }
                context.SaveChanges();

                return string.Format(Messages.AddedCategories, addedCategoriesCount);
            }
            catch (FormatException formatException)
            {
                return formatException.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ImportParentCategories()
        {
            string categoryName = "";
            string parentCategoryName = "";
            int addedParentCategoriesCount = 0;

            try
            {
                OracleSelectConnection oracleConnection = new OracleSelectConnection();
                var oracleTable = oracleConnection.SelectData(NativeOracleSQLCommands.GetParentCategories, null);

                if (oracleTable == null)
                {
                    return Messages.NoCategoriesInOracleDatabase;
                }

                foreach (DataRow row in oracleTable.Rows)
                {
                    categoryName = row["NAME"].ToString();
                    parentCategoryName = row["parent_category"].ToString();

                    var category = context.Categories.Where(c => c.Name == categoryName).FirstOrDefault();
                    if (category.ParentCategoryId == null)
                    {
                        var parentCategoryId = context.Categories
                        .Where(c => c.Name == parentCategoryName)
                        .Select(pc => pc.Id)
                        .FirstOrDefault();

                        context.Categories.Attach(category);

                        var entry = context.Entry(category);
                        entry.State = EntityState.Modified;
                        entry.Property(e => e.ParentCategoryId).CurrentValue = parentCategoryId;
                        addedParentCategoriesCount++;
                    }
                }
                context.SaveChanges();

                return string.Format(Messages.AddedParentCategories, addedParentCategoriesCount);
            }
            catch (FormatException formatException)
            {
                return formatException.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ImportProducts()
        {
            string productName = "";
            string productPrice, productCategory, productMeasure, productVendor;
            int addedProductsCount = 0;

            try
            {
                OracleSelectConnection oracleConnection = new OracleSelectConnection();
                var oracleTable = oracleConnection.SelectData(NativeOracleSQLCommands.GetProducts, null);

                if (oracleTable == null)
                {
                    return Messages.NoProductsInOracleDatabase;
                }

                foreach (DataRow row in oracleTable.Rows)
                {
                    productName = row["name"].ToString();
                    productPrice = row["price"].ToString();
                    productVendor = row["vendor"].ToString();
                    productMeasure = row["measure"].ToString();
                    productCategory = row["category"].ToString();
                    

                    if (!this.context.Products.Any(c => c.Name == productName))
                    {
                        int vendorId = this.context.Vendors.Where(v => v.Name == productVendor).Select(v => v.Id).FirstOrDefault();
                        int measureId = this.context.Measures.Where(m => m.Name == productMeasure).Select(m => m.Id).FirstOrDefault();
                        int categoryId = this.context.Categories.Where(c => c.Name == productCategory).Select(c => c.Id).FirstOrDefault();

                        context.Products.Add(new Product()
                        {
                            Name = productName,
                            Price = decimal.Parse(productPrice),
                            VendorId = vendorId,
                            MeasureId = measureId,
                            CategoryId = categoryId
                        });
                        addedProductsCount++;
                    }
                }
                context.SaveChanges();

                return string.Format(Messages.AddedProducts, addedProductsCount);
            }
            catch (FormatException formatException)
            {
                return formatException.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}