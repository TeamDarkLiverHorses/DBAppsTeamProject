namespace DatabaseManager.ImportSalesData.Utilities
{
    public static class Messages
    {
        public static readonly string ExportedCategories = "Exported categories";
        public static readonly string NotExportedCategories = "Categories already added";
        public static readonly string ExportedMeasure = "Exported measures";
        public static readonly string NotExportedMeasure = "Measures already added";
        public static readonly string ExportedVendors = "Exported vendors";
        public static readonly string NotExportedVendors = "Vendors already added";
        public static readonly string ExportedProducts = "Exported products";
        public static readonly string NotExportedProducts = "Products already added";
        public static readonly string ErrorUpdatingSQLDatabase = "Error updating database. The data was rolled back.";
        public static readonly string ErrorDuringRollback = "Error - data was not rolled back.";
        public static readonly string NonExistingProduct = "Products {0} does not exist in the database.";
    }
}
