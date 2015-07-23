namespace DatabaseManager.ImportSalesData.NativeSQL
{
    using System;

    public class NativeOracleSQLCommands
    {
        public static readonly string GetVendors = "SELECT NAME FROM VENDORS";
        public static readonly string GetMeasures = "SELECT NAME FROM MEASURES";
        public static readonly string GetCategories = "SELECT NAME FROM CATEGORIES";
        public static readonly string GetParentCategories = "select c1.Name, c2.Name as parent_category" + Environment.NewLine +
            "from Categories c1" + Environment.NewLine +
            "join Categories c2" + Environment.NewLine +
            "on c1.PARENT_CATEGORY_ID = c2.id";
        public static readonly string GetProducts = "select p.Name, p.Price, v.Name as vendor, m.Name as measure, c.Name as Category" + Environment.NewLine +
            "from Products p join Vendors v on p.vendor_id = v.id" + Environment.NewLine +
            "join Measures m on p.measure_id = m.id" + Environment.NewLine +
            "join Categories c on p.category_id = c.id";
    }
}
