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

    }
}
