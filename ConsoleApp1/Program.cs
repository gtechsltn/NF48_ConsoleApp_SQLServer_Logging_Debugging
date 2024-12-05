using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace ConsoleApp1
{
    internal class Program
    {
        private static string baseFolder = string.Empty;

        private static void Main(string[] args)
        {
            AppInitializer();
            UpdateLogFile($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: Started.");
            ExecuteSqlTextAllLines();
            UpdateLogFile($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: Finished.");
        }

        private static void AppInitializer()
        {
            baseFolder = GetBaseFolder();
            string filePath = Path.Combine(baseFolder, "TsWebsite.log");
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, string.Empty, Encoding.UTF8);
            }
        }

        private static string GetBaseFolder()
        {
            string baseFolder = Environment.CurrentDirectory;
            return baseFolder;
        }

        private static void UpdateLogFile(string newLine)
        {
            string filePath = Path.Combine(baseFolder, "TsWebsite.log");
            if (File.Exists(filePath) && !string.IsNullOrWhiteSpace(newLine))
            {
                string fileContent = File.ReadAllText(filePath);
                StringBuilder sb = new StringBuilder(fileContent);
                sb.AppendLine(newLine);
                fileContent = sb.ToString();
                File.WriteAllText(filePath, fileContent);
            }
        }

        /// <summary>
        /// Execute TSQL Without using SMO?
        /// https://stackoverflow.com/questions/8073170/execute-tsql-without-using-smo
        /// </summary>
        private static void ExecuteSqlTextAllLines()
        {
            string connectionString = GetSqlServerConnectionString();
            string sqlFilePath = Path.Combine(baseFolder, "SQLQuery.sql");
            string sqlStatementBatch = File.ReadAllText(sqlFilePath, Encoding.UTF8);

            //All SQL Statements
            string[] allSqlStatements = ParseSqlStatementBatch(sqlStatementBatch);

            //SQL Statements does not include "--"
            string[] sqlStatements = new string[allSqlStatements.Length];
            for (int i = allSqlStatements.Length - 1; i >= 0; i--)
            {
                var sqlStatement = allSqlStatements[i].TrimStart();
                if (!sqlStatement.StartsWith("--"))
                {
                    Debug.WriteLine(sqlStatement);
                    UpdateLogFile(sqlStatement);
                    sqlStatements[i] = sqlStatement;
                }
            }
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand command = sqlConnection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandTimeout = GetSqlServerCommandTimeout();

                    foreach (string sqlStatement in sqlStatements)
                    {
                        int intRtn = 0;
                        object objRtn = null;
                        DbDataReader dataReader = null;
                        XmlReader xmlReader = null;
                        if (!string.IsNullOrWhiteSpace(sqlStatement))
                        {
                            command.CommandText = sqlStatement;
#if DEBUG
                            Debug.WriteLine(sqlStatement);
#else
                            intRtn = command.ExecuteNonQuery();
                            objRtn = command.ExecuteScalar();
                            dataReader = command.ExecuteReader();
                            xmlReader = command.ExecuteXmlReader();
#endif
                        }
                        Debug.WriteLine(intRtn);
                        Debug.WriteLine(objRtn);
                        Debug.WriteLine(dataReader);
                        Debug.WriteLine(xmlReader);
                    }
                }
            }
        }

        private static int GetSqlServerCommandTimeout()
        {
            int commandTimeout = 30;
            return commandTimeout;
        }

        private static string GetSqlServerConnectionString()
        {
            string connectionString = "Data Source=MANH;Integrated Security=True;Initial Catalog=mssql;Connect Timeout=30;Pooling=True;Max Pool Size=1000;";
            return connectionString;
        }

        /// <summary>
        /// Execute TSQL Without using SMO?
        /// https://stackoverflow.com/questions/8073170/execute-tsql-without-using-smo
        /// </summary>
        /// <param name="sqlStatementBatch"></param>
        /// <returns></returns>
        public static string[] ParseSqlStatementBatch(string sqlStatementBatch)
        {
            // Split the sql into seperate batches by dividing on the GO statement
            Regex sqlStatementBatchSplitter = new Regex(@"^\s*GO\s*\r?$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return sqlStatementBatchSplitter.Split(sqlStatementBatch);
        }
    }
}