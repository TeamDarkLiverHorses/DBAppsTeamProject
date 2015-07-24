﻿namespace DatabaseManager.SalesReports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DatabaseManager.Data;
    using DatabaseManager.Models;
    using System.Data.Entity;

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
                .Where(s => DbFunctions.TruncateTime(s.Date) >= DbFunctions.TruncateTime(startDate) &&
                    DbFunctions.TruncateTime(s.Date) <= DbFunctions.TruncateTime(endDate))
                .OrderByDescending(s => s.Date)
                .ThenBy(s => s.Product.Name)
                .ToList();
        }

        public List<Sale> GetSalesAfter(DateTime startDate)
        {
            return this.context.Sales
                .Where(s => DbFunctions.TruncateTime(s.Date) >= DbFunctions.TruncateTime(startDate))
                .OrderByDescending(s => s.Date)
                .ThenBy(s => s.Product.Name)
                .ToList();
        }

        public List<Sale> GetSalesBefore(DateTime endDate)
        {
            return this.context.Sales
                .Where(s => DbFunctions.TruncateTime(s.Date) <= DbFunctions.TruncateTime(endDate))
                .OrderByDescending(s => s.Date)
                .ThenBy(s => s.Product.Name)
                .ToList();
        }

        public List<Sale> GetSalesOn(DateTime date)
        {
            return this.context.Sales
                .Where(s => DbFunctions.TruncateTime(s.Date) == DbFunctions.TruncateTime(date))
                .OrderByDescending(s => s.Date)
                .ThenBy(s => s.Product.Name)
                .ToList();
        }
    }
}