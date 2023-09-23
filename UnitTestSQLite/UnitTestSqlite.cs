using System;
using System.Data;
using DatabaseAccess;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestSQLite
{
    [TestClass]
    public class UnitTestSqlite
    {
        private static readonly string fileDbSqlite = AppDomain.CurrentDomain.BaseDirectory + "\\testSqlite.db";
        private static readonly string ConnectionString = "Data Source="+ fileDbSqlite + "; Version = 3; Compress = True;";

        private static readonly string createTable = "CREATE TABLE TEST_TABLE(IDENTIFIER INT PRIMARY KEY, TEXT_VALUE TEXT)";
        private static readonly string insertTable = "INSERT INTO TEST_TABLE (IDENTIFIER, TEXT_VALUE) values ({0}, \'{1}\')";
        private static readonly string updateTable = "UPDATE TEST_TABLE SET TEXT_VALUE=\'{1}\' WHERE IDENTIFIER={0}";
        private static readonly string selectTable = "SELECT * FROM TEST_TABLE WHERE {0}";
        private static readonly string deleteTable = "DELETE FROM TEST_TABLE WHERE {0}";

        [TestMethod]
        public void Test1_Connection()
        {
            try
            {
                System.IO.File.Delete(fileDbSqlite);
                new DbAccess(ConnectionString, BuildDb.RecognizedTypes.Sqlite);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Test2_CreateTable()
        {
            DbAccess dbAccess = new DbAccess(ConnectionString, BuildDb.RecognizedTypes.Sqlite);
            try
            {
                dbAccess.QueryEdit(createTable);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Test3_InsertTable()
        {
            DbAccess dbAccess = new DbAccess(ConnectionString, BuildDb.RecognizedTypes.Sqlite);
            try
            {
                List<string> queries = new List<string>();
                for (int i = 1; i <= 10; i++)
                {
                    queries.Add(string.Format(insertTable, i.ToString(), "Value " + i.ToString()));
                }
                dbAccess.QueryEdit(string.Join(";", queries));
                DataTable dataTable = dbAccess.QuerySelect("SELECT * FROM TEST_TABLE");
                Assert.AreEqual(10, dataTable.Rows.Count);
                for (int i = 1; i <= 10; i++)
                {
                    Assert.AreEqual(dataTable.Rows[i - 1]["IDENTIFIER"].ToString(), i.ToString());
                    Assert.AreEqual(dataTable.Rows[i - 1]["TEXT_VALUE"].ToString(), "Value " + i.ToString());
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Test4_UpdateTable()
        {
            DbAccess dbAccess = new DbAccess(ConnectionString, BuildDb.RecognizedTypes.Sqlite);
            try
            {
                List<string> queries = new List<string>();
                for (int i = 0; i <= 10; i++)
                {
                    string query = string.Format(updateTable, i.ToString(), "Value " + i.ToString("N3"));
                    queries.Add(query);
                }
                dbAccess.QueryEdit(string.Join(";", queries));
                DataTable dataTable = dbAccess.QuerySelect("SELECT * FROM TEST_TABLE");
                Assert.AreEqual(10, dataTable.Rows.Count);
                for (int i = 1; i <= 10; i++)
                {
                    Assert.AreEqual(dataTable.Rows[i - 1]["IDENTIFIER"].ToString(), i.ToString());
                    Assert.AreEqual(dataTable.Rows[i - 1]["TEXT_VALUE"].ToString(), "Value " + i.ToString("N3"));
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Test5_DeleteTable()
        {
            DbAccess dbAccess = new DbAccess(ConnectionString, BuildDb.RecognizedTypes.Sqlite);
            try
            {
                string query = string.Format(deleteTable, "IDENTIFIER=8");
                int changedRows = dbAccess.QueryEdit(query);
                Assert.AreEqual(1, changedRows);
                DataTable dataTable = dbAccess.QuerySelect("SELECT * FROM TEST_TABLE");
                Assert.AreEqual(9, dataTable.Rows.Count);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Test6_SelectTable()
        {
            DbAccess dbAccess = new DbAccess(ConnectionString, BuildDb.RecognizedTypes.Sqlite);
            try
            {
                string query = string.Format(selectTable, "IDENTIFIER=1");
                DataTable dataTable = dbAccess.QuerySelect(query);
                Assert.AreEqual(1, dataTable.Rows.Count);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
