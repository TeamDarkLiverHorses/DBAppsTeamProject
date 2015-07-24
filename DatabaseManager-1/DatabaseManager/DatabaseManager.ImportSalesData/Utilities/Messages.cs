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
        public static readonly string NonExistingProduct = "Product(s) {0} do not exist in the database.";
        public static readonly string InvalidProductPrice = "Product {0} has invalid price";
        public static readonly string NoVendorsInOracleDatabase = "There are no vendors in the Oracle database.";
        public static readonly string NoMeasuresInOracleDatabase = "There are no measures in the Oracle database.";
        public static readonly string NoCategoriesInOracleDatabase = "There are no categories in the Oracle database.";
        public static readonly string NoProductsInOracleDatabase = "There are no products in the Oracle database.";
        public static readonly string AddedVendors = "Added {0} vendors.";
        public static readonly string AddedMeasures = "Added {0} measures.";
        public static readonly string AddedCategories = "Added {0} categories.";
        public static readonly string AddedParentCategories = "Added {0} parent categories.";
        public static readonly string AddedProducts = "Added {0} products.";
        public static readonly string ImportedSales = "Number of imported sales - {0}.";
    }
}
