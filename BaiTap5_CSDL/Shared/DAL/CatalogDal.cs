using System;
using System.Data;
using System.Linq;
using BaiTap5.Shared.DTO;

namespace BaiTap5.Shared.DAL
{
    public sealed class CatalogDal
    {
        private readonly Db db;
        private readonly CatalogDefinition def;
        public CatalogDal(Db db, CatalogDefinition def) { this.db = db; this.def = def; }
        public DataTable Search(string keyword)
        {
            string filter = string.Join(" OR ", new[] { def.Key }.Concat(def.Fields.Select(f => f.Name))
                .Select(n => "CONVERT(nvarchar(4000),[" + n + "]) LIKE @q ESCAPE '~'"));
            string literal = (keyword ?? "").Replace("~", "~~").Replace("%", "~%")
                .Replace("_", "~_").Replace("[", "~[");
            using (var cn = db.Open())
            using (var cmd = Db.Command(cn, null, "SELECT * FROM [" + def.Table + "] WHERE " + filter + " ORDER BY [" + def.Key + "]", Db.P("@q", "%" + literal + "%")))
            using (var reader = cmd.ExecuteReader())
            {
                var dt = new DataTable(); dt.Load(reader); return dt; // Connected Model.
            }
        }
        public void Save(object key, object[] values)
        {
            bool insert = key == null;
            var columns = def.Fields.Select(f => "[" + f.Name + "]").ToArray();
            var args = values.Select((v, i) => Db.P("@p" + i, v)).ToList();
            string sql;
            if (insert)
                sql = "INSERT INTO [" + def.Table + "] (" + string.Join(",", columns) + ") VALUES (" + string.Join(",", args.Select(p => p.ParameterName)) + ")";
            else
            {
                sql = "UPDATE [" + def.Table + "] SET " + string.Join(",", columns.Select((c, i) => c + "=@p" + i)) + " WHERE [" + def.Key + "]=@id";
                args.Add(Db.P("@id", key));
            }
            if (db.Execute(sql, args.ToArray()) != 1) throw new InvalidOperationException("Bản ghi đã thay đổi. Hãy tải lại.");
        }
        public void Delete(object key)
        {
            if (db.Execute("DELETE FROM [" + def.Table + "] WHERE [" + def.Key + "]=@id", Db.P("@id", key)) != 1)
                throw new InvalidOperationException("Bản ghi không còn tồn tại.");
        }
    }
}
