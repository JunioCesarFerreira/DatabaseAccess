using System.Data;
using System.Data.Common;

namespace DatabaseAccess
{
    public abstract class DbAccessFactory
    {

    }

    public class DbAccess
    {
        private readonly DbConnection _dbConnection;
        public int CommandTimeout = 0;

        public DbAccess(string connectionString, RecognizedTypes dbType)
        {
            _dbConnection = BuildDb.Connection(connectionString, dbType);
        }

        public bool CheckConnection()
        {
            bool result = true;
            try
            {
                _dbConnection.Open();
            }
            catch
            {
                result = false;
            }
            finally
            {
                _dbConnection.Close();
            }
            return result;
        }

        public int QueryEdit(string query)
        {
            if (BuildDb.Command(_dbConnection, query, out DbCommand dbCommand))
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
            if (BuildDb.DataAdapter(_dbConnection, query, out DbDataAdapter dbDataAdapter))
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
