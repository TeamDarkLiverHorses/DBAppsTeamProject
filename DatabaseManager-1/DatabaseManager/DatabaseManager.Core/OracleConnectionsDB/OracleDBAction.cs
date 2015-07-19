using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace DatabaseManager.Core.OracleConnectionDB
{
    public class OracleDBAction
    {
        public int InsertProductCategory(string commandString, string categoryName, string categoryValue, string parentName, string parentValue)
        {
            int result = -1;

            OracleNotSelectConnection notSelectConnection = null;

            OracleParameter insertParameter = new OracleParameter(categoryName, categoryValue);
            OracleParameter parentParameter = new OracleParameter(parentName,  parentValue);

            if (parentValue == string.Empty)
            {
                parentParameter.Value = System.DBNull.Value;
            }

            try
            {
                using (notSelectConnection = new OracleNotSelectConnection())
                {
                    result = notSelectConnection.UpdateDataBase(commandString, new OracleParameter[] { insertParameter, parentParameter });
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

        public int InsertProductParameter(string commandString, string categoryName, string value)
        {
            int result = -1;

            OracleNotSelectConnection notSelectConnection = null;

            OracleParameter insertParameter = new OracleParameter(categoryName, value);

            try
            {
                using (notSelectConnection = new OracleNotSelectConnection())
                {
                    result = notSelectConnection.UpdateDataBase(commandString, new OracleParameter[] { insertParameter });
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

        public DataTable SelectProduct(string commandString)
        {
            DataTable result = null;

            OracleSelectConnection oracleSelectConnection = null;
            
            try
            {
                using (oracleSelectConnection = new OracleSelectConnection())
                {
                    result = oracleSelectConnection.SelectData(commandString, null);
                }
            }
            catch (Exception ex)
            {
                if (oracleSelectConnection != null)
                {
                    oracleSelectConnection.Dispose();
                    oracleSelectConnection = null;
                }
                
                throw ex;
            }

            return result;
        }

        public int InsertNewProduct(string commandString, string name, decimal price, int vendor, int measure, int category)
        {
            int result = -1;

            OracleParameter parameterName = new OracleParameter(":name", name);
            OracleParameter parameterPrice = new OracleParameter(":price", price);
            OracleParameter parameterVendor = new OracleParameter(":vendor", vendor);
            OracleParameter parameterMeasure = new OracleParameter(":measure", measure);
            OracleParameter parameterCategory = new OracleParameter(":category", category);

            OracleNotSelectConnection notSelectConnection = null;

            try
            {
                using (notSelectConnection = new OracleNotSelectConnection())
                {
                    result = notSelectConnection.UpdateDataBase(commandString, 
                        new OracleParameter[] { parameterName, parameterPrice, parameterVendor, parameterMeasure, parameterCategory });
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
