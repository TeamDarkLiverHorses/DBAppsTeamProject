namespace DatabaseManager.SalesReports
{
    public class ProductSales
    {
        public int ProductId { get; set; }

        public string  ProductName { get; set; }

        public string VendorName { get; set; }

        public int TotalQuantitySold { get; set; }

        public decimal TotalIncome { get; set; }
    }
}
