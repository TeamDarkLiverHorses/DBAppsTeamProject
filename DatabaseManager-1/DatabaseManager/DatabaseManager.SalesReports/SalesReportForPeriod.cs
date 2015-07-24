namespace DatabaseManager.SalesReports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DatabaseManager.Data;
    using DatabaseManager.Models;

    public class SalesReportForPeriod
    {
        private SupermarketsContext context;

        public SalesReportForPeriod()
        {
            this.context = new SupermarketsContext();
        }

        public List<Sale> GetSalesBetween(DateTime startDate, DateTime endDate)
        {
            return this.context.Sales
                .Where(s => s.Date >= startDate && s.Date <= endDate)
                .OrderByDescending(s => s.Date)
                .ThenBy(s => s.Product.Name)
                .ToList();
        }

        public List<Sale> GetSalesAfter(DateTime startDate)
        {
            return this.context.Sales
                .Where(s => s.Date >= startDate)
                .OrderByDescending(s => s.Date)
                .ThenBy(s => s.Product.Name)
                .ToList();
        }

        public List<Sale> GetSalesBefore(DateTime endDate)
        {
            return this.context.Sales
                .Where(s => s.Date <= endDate)
                .OrderByDescending(s => s.Date)
                .ThenBy(s => s.Product.Name)
                .ToList();
        }

        public List<Sale> GetSalesOn(DateTime date)
        {
            return this.context.Sales
                .Where(s => s.Date.Year == date.Year && s.Date.Month == date.Month && s.Date.Day == date.Day)
                .OrderByDescending(s => s.Date)
                .ThenBy(s => s.Product.Name)
                .ToList();
        }
    }
}
