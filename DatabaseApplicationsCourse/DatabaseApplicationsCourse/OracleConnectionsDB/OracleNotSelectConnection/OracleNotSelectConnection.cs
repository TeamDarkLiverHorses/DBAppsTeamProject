using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace DatabaseApplicationsCourse.OracleConnectionDB.OracleNotSelectConnection
{
    class OracleNotSelectConnection : IDisposable
    {
        bool isDisposed = false;

        OracleConnection connection;
        OracleCommand command;
        OracleTransaction transaction;

        string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

        internal int UpdateDataBase(string commandString, OracleParameter[] parameters)
        {
            int rowsUpdated = -1;

            try
            {
                if (commandString == string.Empty)
                {
                    throw new ArgumentNullException("SQL command is empty.");
                }

                if (parameters == null || parameters.Length == 0)
                {
                    throw new ArgumentNullException("SQL parameters are empty.");
                }

                connection = new OracleConnection(connectionString);

                using (connection)
                {
                    connection.Open();
                    
                    command = new OracleCommand(commandString, connection);
                    command.Parameters.AddRange(parameters);

                    transaction = connection.BeginTransaction();

                    rowsUpdated = command.ExecuteNonQuery();

                    try
                    {
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception innerEx)
                        {
                            throw new Exception("No values was updated. The query was not roll back.", innerEx);
                        }

                        throw new Exception("No values was updated. The query was roll back.", ex);
                    }

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

            return rowsUpdated;
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

            if (transaction != null)
            {
                transaction.Dispose();
                transaction = null;
            }
        }

        public void Dispose()
        {
            Dispose(isDisposed);
        }

        private void Dispose(bool disposed)
        {
            if (!disposed)
            {
                isDisposed = true;

                GC.SuppressFinalize(this);
            }
        }
    }
}
