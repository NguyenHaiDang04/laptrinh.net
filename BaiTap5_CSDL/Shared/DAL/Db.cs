using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BaiTap5.Shared.DAL
{
    // Mọi dữ liệu người dùng đều được truyền qua SqlParameter, không ghép vào SQL.
    public sealed class Db
    {
        private readonly string connectionString;
        public Db(string name)
        {
            var item = ConfigurationManager.ConnectionStrings[name];
            if (item == null) throw new InvalidOperationException("Thiếu chuỗi kết nối " + name);
            connectionString = item.ConnectionString;
        }
        public SqlConnection Open()
        {
            var cn = new SqlConnection(connectionString);
            cn.Open();
            return cn;
        }
        public static SqlParameter P(string name, object value)
        {
            SqlParameter p;
            if (value is decimal)
                p = new SqlParameter(name, SqlDbType.Decimal) { Precision = 18, Scale = 2 };
            else if (value is int) p = new SqlParameter(name, SqlDbType.Int);
            else if (value is DateTime) p = new SqlParameter(name, SqlDbType.DateTime2);
            else if (value is bool) p = new SqlParameter(name, SqlDbType.Bit);
            else p = new SqlParameter(name, SqlDbType.NVarChar, 4000);
            p.Value = value ?? DBNull.Value;
            return p;
        }
        public static SqlCommand Command(SqlConnection cn, SqlTransaction tx, string sql, params SqlParameter[] args)
        {
            var cmd = new SqlCommand(sql, cn, tx);
            cmd.Parameters.AddRange(args);
            return cmd;
        }
        public DataTable Table(string sql, params SqlParameter[] args)
        {
            using (var cn = Open())
            using (var cmd = Command(cn, null, sql, args))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                var ds = new DataSet();
                adapter.Fill(ds, "Result");
                return ds.Tables["Result"];
            }
        }
        public object Scalar(string sql, params SqlParameter[] args)
        {
            using (var cn = Open())
            using (var cmd = Command(cn, null, sql, args)) return cmd.ExecuteScalar();
        }
        public int Execute(string sql, params SqlParameter[] args)
        {
            using (var cn = Open())
            using (var cmd = Command(cn, null, sql, args)) return cmd.ExecuteNonQuery();
        }
        public T Transaction<T>(Func<SqlConnection, SqlTransaction, T> work)
        {
            using (var cn = Open())
            using (var tx = cn.BeginTransaction(IsolationLevel.Serializable))
            {
                try { T result = work(cn, tx); tx.Commit(); return result; }
                catch { tx.Rollback(); throw; }
            }
        }
        public static int Exec(SqlConnection cn, SqlTransaction tx, string sql, params SqlParameter[] args)
        {
            using (var cmd = Command(cn, tx, sql, args)) return cmd.ExecuteNonQuery();
        }
        public static object One(SqlConnection cn, SqlTransaction tx, string sql, params SqlParameter[] args)
        {
            using (var cmd = Command(cn, tx, sql, args)) return cmd.ExecuteScalar();
        }
    }
}
