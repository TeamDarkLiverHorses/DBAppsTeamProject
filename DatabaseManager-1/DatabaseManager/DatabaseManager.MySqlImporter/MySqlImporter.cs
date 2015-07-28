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
        private static string info = "Expenses imported: {0}" + Environment.NewLine + "Incomes imported: {1}";

        private static int expenses = 0;
        private static int incomes = 0;

        internal static string ImportData(List<MySqlVendor> vendors, List<MySqlProduct> products)
        {
            expenses = 0;
            incomes = 0;
            
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

                            expenses += 1;
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

                            expenses += 1;

                            incomes += 1;
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

            return string.Format(info, expenses, incomes);
        }
    }
}
