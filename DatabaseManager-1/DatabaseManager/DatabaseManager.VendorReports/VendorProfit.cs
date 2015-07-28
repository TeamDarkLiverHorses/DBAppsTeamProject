namespace DatabaseManager.VendorReports
{
    public class VendorProfit
    {
        public string VendorName { get; set; }

        public decimal VendorIncome { get; set; }

        public decimal VendorExpenses { get; set; }

        public decimal VendorTaxes { get; set; }

        public decimal Profit
        {
            get
            {
                return this.VendorIncome - this.VendorTaxes - this.VendorExpenses;
            }
        }
    }
}
