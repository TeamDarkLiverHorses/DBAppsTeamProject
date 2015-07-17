using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Configuration;

namespace DatabaseApplicationsCourse.OracleConnectionDB.OracleSelectConnection
{
    class OracleSelectConnection : IDisposable
    {
        private bool isDisposed = false;

        OracleConnection connection;
        OracleCommand command;
        OracleDataAdapter dataAdapter;

        string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

        internal DataTable SelectData(string commandString, OracleParameter[] parameters)
        {
            DataTable result = new DataTable();

            try
            {
                if (commandString == string.Empty)
                {
                    throw new ArgumentNullException("SQL command is empty.");
                }
                
                connection = new OracleConnection(connectionString);

                using (connection)
                {
                    command = new OracleCommand(commandString, connection);

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    dataAdapter = new OracleDataAdapter(command);

                    connection.Open();

                    dataAdapter.Fill(result);

                    connection.Close();
                }
   
            }
            catch (ArgumentNullException argumentEx)
            {
                ClearResources();
                throw argumentEx;
            }
            catch (OracleException oracleEx)
            {
                ClearResources();
                throw oracleEx;
            }
            catch (Exception ex)
            {
                ClearResources();
                throw ex;
            }

            ClearResources();
            return result;
        }

        private void ClearResources()
        {
            if (connection != null)
            {
                connection.Dispose();
                connection = null;
            }

            if (command != null)
            {
                command.Dispose();
                command = null;
            }

            if (dataAdapter != null)
            {
                dataAdapter.Dispose();
                dataAdapter = null;
            }
        }

        public void Dispose()
        {
            Dispose(isDisposed);
        }

        private void Dispose(bool dispose)
        {

            if (!dispose)
            {
                ClearResources();

                isDisposed = true;

                GC.SuppressFinalize(this);
            }
        }
    }
}
