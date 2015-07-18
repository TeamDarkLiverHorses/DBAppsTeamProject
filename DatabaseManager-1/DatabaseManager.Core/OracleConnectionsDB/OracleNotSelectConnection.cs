namespace DatabaseManager.Core.OracleConnectionDB
{
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Configuration;

    // TODO: OracleSelectConnection and OracleNotSelectConnection have similar logic and we must use OOP Inheritance :))

    public class OracleNotSelectConnection : OracleSelectConnection
    {
        private OracleTransaction transaction = null;

        public int UpdateDataBase(string commandString, OracleParameter[] parameters)
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
                    base.connection.Open();

                    base.command = new OracleCommand(commandString, connection);
                    base.command.Parameters.AddRange(parameters);

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

                    base.connection.Close();
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

        protected override void ClearResources()
        {
            if (transaction != null)
            {
                transaction.Dispose();
                transaction = null;
            }
            
            base.ClearResources();
        }
    }
}
