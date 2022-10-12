using System;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.Common;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace DatabaseAccess
{
    public class DbConnectionIsNotRecognizedException : Exception
    {
        public DbConnectionIsNotRecognizedException() : base("Database connection type is not recognized.") { }
    }

    public static class BuildDb
    {
        public enum RecognizedTypes
        {
            SqlClient, OleDb, Odbc, Sqlite
        }

        public static bool IsItARecognizedType(DbConnection dbConnection)
        {
            Type type = dbConnection.GetType();
            return 
                   type == typeof(SqlConnection) 
                || type == typeof(OleDbConnection) 
                || type == typeof(OdbcConnection)
                || type == typeof(SQLiteConnection);
        }

        public static DbConnection Connection(string connectionString, RecognizedTypes dbType)
        {
            switch (dbType)
            {
                case RecognizedTypes.SqlClient:
                    return new SqlConnection(connectionString);

                case RecognizedTypes.OleDb:
                    return new OleDbConnection(connectionString);

                case RecognizedTypes.Odbc:
                    return new OdbcConnection(connectionString);

                case RecognizedTypes.Sqlite:
                    return new SQLiteConnection(connectionString);

                default:
                    throw new DbConnectionIsNotRecognizedException();
            }
        }

        public static bool Command(DbConnection dbConnection, string query, out DbCommand dbCommand)
        {
            Type type = dbConnection.GetType();
            if (type == typeof(SqlConnection))
            {
                dbCommand = new SqlCommand(query, (SqlConnection)dbConnection);
            }
            else if (type == typeof(OleDbConnection))
            {
                dbCommand = new OleDbCommand(query, (OleDbConnection)dbConnection);
            }
            else if (type == typeof(OdbcConnection))
            {
                dbCommand = new OdbcCommand(query, (OdbcConnection)dbConnection);
            }
            else if (type == typeof(SQLiteConnection))
            {
                dbCommand = new SQLiteCommand(query, (SQLiteConnection)dbConnection);
            }
            else
            {
                dbCommand = null;
                return false;
            }
            return true;
        }

        public static bool DataAdapter(DbConnection dbConnection, string query, out DbDataAdapter dbDataAdapter)
        {
            Type type = dbConnection.GetType();
            if (type == typeof(SqlConnection))
            {
                dbDataAdapter = new SqlDataAdapter(query, (SqlConnection)dbConnection);
            }
            else if (type == typeof(OleDbConnection))
            {
                dbDataAdapter = new OleDbDataAdapter(query, (OleDbConnection)dbConnection);
            }
            else if (type == typeof(OdbcConnection))
            {
                dbDataAdapter = new OdbcDataAdapter(query, (OdbcConnection)dbConnection);
            }
            else if (type == typeof(SQLiteConnection))
            {
                dbDataAdapter = new SQLiteDataAdapter(query, (SQLiteConnection)dbConnection);
            }
            else
            {
                dbDataAdapter = null;
                return false;
            }
            return true;
        }

    }
}
