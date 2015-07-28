namespace DatabaseManager.VendorReports
{
    using System;
    using System.Collections.Generic;

    public class VendorIncome
    {
        public string VendorName { get; set; }

        public IEnumerable<ProductIncome> ProductIncomes { get; set; }
    }
}
