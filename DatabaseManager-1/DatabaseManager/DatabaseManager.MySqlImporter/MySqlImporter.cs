namespace DatabaseManager.MySqlImporter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DatabaseManager.MySqlData;
    using DatabaseManager.MySqlModels;
    using System.Data;

    internal static class MySqlImporter
    {

        internal static void ImportData(List<MySqlVendor> vendors, List<MySqlProduct> products)
        {
            using (MySqlSupermarketContext context = new MySqlSupermarketContext())
            {
                using (var transaction = context.Database.BeginTransaction(IsolationLevel.RepeatableRead))
                {
                    try
                    {
                        foreach (var currentVendor in vendors)
                        {
                            if (context.vendors.Where(v => v.Name == currentVendor.Name).Any())
                            {
                                context.vendors.Where(v => v.Name == currentVendor.Name).First().Expenses = currentVendor.Expenses;
                            }
                            else
                            {
                                context.vendors.Add(currentVendor);
                            }

                        }

                        context.SaveChanges();

                        foreach (var currentProduct in products)
                        {
                            if (context.vendors.Where(v => v.Name == currentProduct.Vendor.Name).Any())
                            {
                                context.vendors.Where(v => v.Name == currentProduct.Vendor.Name).First().Expenses = currentProduct.Vendor.Expenses;
                            }

                            if (context.products.Where(p => p.Name == currentProduct.Name).Any())
                            {
                                context.products.Where(p => p.Name == currentProduct.Name).First().Incomes = currentProduct.Incomes;
                            }
                            else
                            {
                                context.products.Add(currentProduct);
                            }
                        }

                        context.SaveChanges();

                        transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception innerEx)
                        {
                            throw new Exception("Error updating database. Changes were not rollback.", innerEx);
                        }

                        throw new Exception("Error updating database. Changes were rollback.", ex);
                    }
                }
            }
        }
    }
}
