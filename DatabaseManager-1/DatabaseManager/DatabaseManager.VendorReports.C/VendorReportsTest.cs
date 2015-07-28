namespace DatabaseManager.VendorReports.C
{
    using System;
    using DatabaseManager.VendorReports;

    public class VendorReportsTest
    {
        static void Main()
        {
            var reportManager = new VendorProfitReport();

            var profits = reportManager.CalculateVendorsProfits();
            foreach (var profit in profits)
            {
                Console.WriteLine(
                    "{0}: Incomes: {1}, Taxes: {2}, Expenses: {3}, Profit: {4}",
                    profit.VendorName,
                    profit.VendorIncome,
                    profit.VendorTaxes,
                    profit.VendorExpenses,
                    profit.Profit);
            }

            string filePath = @"\\psf\Home\Downloads\VendorProfits.xlsx";
            reportManager.CreateReport(filePath);
        }
    }
}
