using System;
using System.Data;
using System.Data.SqlClient;
using BaiTap5.Shared.DTO;
using BaiTap5.Shared.DAL;

namespace BaiTap5.Shared.BUS
{
    public sealed class CatalogBus : ICatalogService
    {
        private readonly CatalogDal dal;
        public CatalogDefinition Definition { get; private set; }
        public CatalogBus(Db db, CatalogDefinition def) { Definition = def; dal = new CatalogDal(db, def); }
        public DataTable Search(string keyword) { return dal.Search(keyword); }
        public void Save(object key, object[] values)
        {
            if (values.Length != Definition.Fields.Length) throw new ArgumentException("Thiếu thông tin.");
            for (int i = 0; i < values.Length; i++)
            {
                var f = Definition.Fields[i];
                if (f.Type == typeof(string))
                {
                    var value = Convert.ToString(values[i]).Trim();
                    if (f.Required && value.Length == 0) throw new ArgumentException(f.Label + " không được để trống.");
                    if (value.Length > f.MaxLength) throw new ArgumentException(f.Label + " tối đa " + f.MaxLength + " ký tự.");
                    values[i] = !f.Required && value.Length == 0 ? null : value;
                }
                else if (f.Type == typeof(decimal) && Convert.ToDecimal(values[i]) < 0)
                    throw new ArgumentException(f.Label + " không được âm.");
                else if (f.Type == typeof(int) && Convert.ToInt32(values[i]) <= 0)
                    throw new ArgumentException(f.Label + " phải lớn hơn 0.");
            }
            try { dal.Save(key, values); }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            { throw new ArgumentException("Mã hoặc số điện thoại đã tồn tại.", ex); }
            catch (SqlException ex) when (ex.Number == 547)
            { throw new ArgumentException("Mã tham chiếu không hợp lệ hoặc dữ liệu vi phạm ràng buộc.", ex); }
        }
        public void Delete(object key)
        {
            if (key == null) throw new ArgumentException("Chọn bản ghi cần xóa.");
            try { dal.Delete(key); }
            catch (SqlException ex) when (ex.Number == 547)
            { throw new InvalidOperationException("Không thể xóa: dữ liệu đang được sử dụng trong chứng từ.", ex); }
        }
    }
}
