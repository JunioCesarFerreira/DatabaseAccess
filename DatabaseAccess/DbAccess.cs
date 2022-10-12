using System.Data;
using System.Data.Common;

namespace DatabaseAccess
{
    public class DbAccess
    {
        private readonly DbConnection DbConnection;
        public int CommandTimeout = 0;

        public DbAccess(DbConnection databaseConnection)
        {
            DbConnection = databaseConnection;
            if (!BuildDb.IsItARecognizedType(databaseConnection))
            {
                throw new DbConnectionIsNotRecognizedException();
            }
        }

        public DbAccess(string connectionString, BuildDb.RecognizedTypes dbType)
        {
            DbConnection = BuildDb.Connection(connectionString, dbType);
        }

        public bool CheckConnection()
        {
            bool result = true;
            try
            {
                DbConnection.Open();
            }
            catch
            {
                result = false;
            }
            finally
            {
                DbConnection.Close();
            }
            return result;
        }

        public int QueryEdit(string query)
        {
            if (BuildDb.Command(DbConnection, query, out DbCommand dbCommand))
            {
                dbCommand.Connection.Open();
                dbCommand.CommandTimeout = CommandTimeout;
                int rowsChanges = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();
                return rowsChanges;
            }
            else
            {
                throw new DbConnectionIsNotRecognizedException();
            }
        }

        public DataTable QuerySelect(string query)
        {
            if (BuildDb.DataAdapter(DbConnection, query, out DbDataAdapter dbDataAdapter))
            {
                dbDataAdapter.SelectCommand.Connection.Open();
                dbDataAdapter.SelectCommand.CommandTimeout = CommandTimeout;
                DataSet dataSet = new DataSet();
                dbDataAdapter.Fill(dataSet);
                DataTable tableResult = dataSet.Tables[0];
                dbDataAdapter.SelectCommand.Connection.Close();
                return tableResult;
            }
            else
            {
                throw new DbConnectionIsNotRecognizedException();
            }
        }
    }
}
