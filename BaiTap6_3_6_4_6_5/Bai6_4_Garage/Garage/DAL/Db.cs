using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Garage.DAL
{
    internal static class Db
    {
        private static string ConnectionString
        {
            get
            {
                return Environment.GetEnvironmentVariable("LTNET_BAI6_CONNECTION")
                    ?? ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            }
        }

        public static SqlCommand Command(SqlConnection connection, SqlTransaction transaction,
            string sql, params SqlParameter[] parameters)
        {
            var command = new SqlCommand(sql, connection, transaction);
            command.Parameters.AddRange(parameters);
            return command;
        }

        public static DataTable Read(string sql, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = Command(connection, null, sql, parameters))
            using (var adapter = new SqlDataAdapter(command))
            {
                var table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public static DataTable ReadWithReader(string sql, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = Command(connection, null, sql, parameters))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    var table = new DataTable();
                    table.Load(reader);
                    return table;
                }
            }
        }

        public static int Run(string sql, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = Command(connection, null, sql, parameters))
            {
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        public static T Transaction<T>(Func<SqlConnection, SqlTransaction, T> action)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        T result = action(connection, transaction);
                        transaction.Commit();
                        return result;
                    }
                    catch
                    {
                        if (transaction.Connection != null)
                            transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static SqlParameter P(string name, object value)
        {
            return new SqlParameter(name, value ?? DBNull.Value);
        }

        public static SqlParameter Varchar(string name, int size, string value)
        {
            return new SqlParameter(name, SqlDbType.VarChar, size) { Value = value };
        }

        public static SqlParameter Money(string name, decimal value)
        {
            return new SqlParameter(name, SqlDbType.Decimal)
            {
                Precision = 18, Scale = 2, Value = value
            };
        }
    }
}
