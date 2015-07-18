namespace DatabaseManager.Core
{
    using DatabaseManager.Core.OracleConnectionDB;
    using Oracle.ManagedDataAccess.Client;
    using System;

    public class ProductsRepository
    {
        private void InsertProduct(string name, string priceAsString, string vendorAsString, string categoryAsString, string measureAsString)
        {
            const string CommandString = "INSERT INTO PRODUCTS (NAME, PRICE, VENDOR_ID, MEASURE_ID, CATEGORY_ID)" +
               "VALUES (:name, :price, :vendor, :measure, :category)";

            decimal price;
            int vendor;
            int measure;
            int category;
            var decimalSeparator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            if (name == string.Empty)
            {
                throw new ArgumentNullException("Product name is empty.");
            }

            if (!decimal.TryParse(priceAsString, out price))
            {
                throw new FormatException("Product price is not valid." + Environment.NewLine + "Decimal separator is '" + decimalSeparator + "'.");
            }

            if (!int.TryParse(vendorAsString, out vendor))
            {
                throw new FormatException("Product vendor is not valid");
            }

            if (!int.TryParse(measureAsString, out measure))
            {
                throw new FormatException("Product measure is not valid");
            }

            if (!int.TryParse(categoryAsString, out category))
            {
                throw new FormatException("Product category is not valid");
            }

            int result = -1;

            var parameterName = new OracleParameter(":name", name);
            var parameterPrice = new OracleParameter(":price", price);
            var parameterVendor = new OracleParameter(":vendor", vendor);
            var parameterMeasure = new OracleParameter(":measure", measure);
            var parameterCategory = new OracleParameter(":category", category);

            result = UpdateDataBase(CommandString, new OracleParameter[] { parameterName, parameterPrice, parameterVendor, parameterMeasure, parameterCategory });

            if (result == 0)
            {
                // TODO: Make exception class for this case...
                throw new Exception(string.Format("Product {0} was not added to Products. Check if this measure is already added.", name));
            }
            
        }

        private int UpdateDataBase(string commandString, OracleParameter[] parameters)
        {
            OracleNotSelectConnection notSelectConnection = null;

            int result = -1;

            try
            {
                using (notSelectConnection = new OracleNotSelectConnection())
                {
                    result = notSelectConnection.UpdateDataBase(commandString, parameters);
                }
            }
            catch (Exception ex)
            {
                if (notSelectConnection != null)
                {
                    notSelectConnection.Dispose();
                    notSelectConnection = null;
                }

                throw ex;
            }

            return result;
        }
    }
}
