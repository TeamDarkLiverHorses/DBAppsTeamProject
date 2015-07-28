namespace DatabaseManager.VendorReports
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using DatabaseManager.Data;
    using DatabaseManager.Models;
    using DatabaseManager.SQLite.Data;
    using OfficeOpenXml;
    using OfficeOpenXml.Style;

    public class VendorProfitReport
    {
        private SupermarketsContext supermarketsContext;
        private TaxesContext taxesContext;

        public VendorProfitReport()
        {
            this.supermarketsContext = new SupermarketsContext();
            this.taxesContext = new TaxesContext();
        }

        public void CreateReport(string fileName)
        {
            var vendorProfits = this.CalculateVendorsProfits();

            FileInfo newFile = new FileInfo(fileName);
            ExcelPackage pck = new ExcelPackage(newFile);

            //Add the Content sheet
            var ws = pck.Workbook.Worksheets.Add("Vendors P&L");
            //Headers
            ws.Cells["A1"].Value = "Vendor";
            ws.Cells["B1"].Value = "Incomes";
            ws.Cells["C1"].Value = "Expenses";
            ws.Cells["D1"].Value = "Total Taxes";
            ws.Cells["E1"].Value = "Profit";
            ws.Cells["A1:E1"].Style.Font.Bold = true;
            ws.Cells["A1:E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["A1:E1"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(170, 170, 170));

            int row = 2;
            foreach (var entry in vendorProfits)
            {
                ws.Cells["A" + row].Value = entry.VendorName;
                ws.Cells["B" + row].Value = string.Format("{0:# ###.00}", entry.VendorIncome);
                ws.Cells["C" + row].Value = string.Format("{0:# ###.00}", entry.VendorExpenses);
                ws.Cells["D" + row].Value = string.Format("{0:# ###.00}", entry.VendorTaxes);
                ws.Cells["E" + row].Value = string.Format("{0:# ###.00}", entry.Profit);
                row++;
            }

            var range = ws.Cells["A1:E" + (row - 1)];
            var border = range.Style.Border.Top.Style 
                = range.Style.Border.Left.Style
                = range.Style.Border.Right.Style 
                = range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            pck.Save();
        }

        private IEnumerable<ProductTax> GetProductTaxes()
        {
            var productTaxes = this.taxesContext.Taxes
                .Select(t => new ProductTax() { ProductName = t.ProductName, TaxSize = t.TaxSize });
            return productTaxes;
        }

        private IEnumerable<VendorIncome> GetVendorsIncomes()
        {
            var vendorsIncomes = this.supermarketsContext.Vendors
                .Select(v => new VendorIncome()
                {
                    VendorName = v.Name,
                    ProductIncomes = v.Products.Select(p => new ProductIncome()
                    {
                        ProductName = p.Name,
                        Income = (p.Sales.Sum(s =>
                            (s.Quantity == null ? 0 : s.Quantity) *
                            (s.UnitPrice == null ? 0 : s.UnitPrice))) == null ? 0 : (p.Sales.Sum(s =>
                            (s.Quantity == null ? 0 : s.Quantity) *
                            (s.UnitPrice == null ? 0 : s.UnitPrice)))
                    })
                });
            return vendorsIncomes;
        }

        public IEnumerable<VendorProfit> CalculateVendorsProfits()
        {
            var vendorExpenses = this.GetVendorExpenses();
            var vendorIncomes = this.GetVendorsIncomes();
            var productTaxes = this.GetProductTaxes();

            List<VendorProfit> vendorProfits = new List<VendorProfit>();

            foreach (var income in vendorIncomes)
            {
                VendorProfit currentVendorProfit = new VendorProfit();

                currentVendorProfit.VendorName = income.VendorName;

                currentVendorProfit.VendorExpenses = vendorExpenses
                    .Where(ve => ve.VendorName == income.VendorName)
                    .Select(ve => ve.Expense).FirstOrDefault();

                currentVendorProfit.VendorIncome = vendorIncomes
                    .Where(vi => vi.VendorName == income.VendorName)
                    .FirstOrDefault()
                    .ProductIncomes
                    .Sum(pi => pi.Income);

                currentVendorProfit.VendorTaxes = vendorIncomes
                    .Where(vi => vi.VendorName == income.VendorName)
                    .FirstOrDefault()
                    .ProductIncomes
                    .Sum(pi => pi.Income * (decimal)productTaxes
                        .Where(pt => pt.ProductName == pi.ProductName)
                        .Select(pt => pt.TaxSize)
                        .FirstOrDefault());
                vendorProfits.Add(currentVendorProfit);
            }

            return vendorProfits.OrderByDescending(vp => vp.Profit).ThenBy(vp => vp.VendorName).ToList();
        }

        private IEnumerable<VendorExpense> GetVendorExpenses()
        {
            var vendorExpenses = this.supermarketsContext.Vendors
                .Where(v => v.Expenses.Count > 0)
                .Select(v => new VendorExpense()
                {
                    VendorName = v.Name,
                    Expense = v.Expenses.Sum(e => e.Ammount)
                });
            return vendorExpenses;
        }
    }
}
