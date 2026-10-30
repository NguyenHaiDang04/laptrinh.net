using System;
using System.Data;
using System.Linq;
using BaiTap5.Shared.DAL;
using InventoryManagement.DTO;

namespace InventoryManagement.DAL
{
    public sealed class InventoryDal
    {
        public readonly Db Database=new Db("InventoryDB");
        public DataTable Materials(string keyword)
        {
            // Search của CatalogDal dùng SqlDataReader (Connected Model), SqlParameter và escape LIKE.
            return new CatalogDal(Database,new BaiTap5.Shared.DTO.CatalogDefinition("Vật tư","VatTu","MaVatTu",false,
                new BaiTap5.Shared.DTO.Field("TenVatTu","Tên",typeof(string)))).Search(keyword);
        }
        public DataTable Suppliers(){return Database.Table("SELECT * FROM NhaCungCap ORDER BY TenNCC");}
        public DataTable Vouchers(){return Database.Table("SELECT * FROM PhieuKho ORDER BY MaPhieu DESC");}
        public DataTable LowStock(decimal threshold){return Database.Table("SELECT * FROM VatTu WHERE SoLuongTon<@nguong ORDER BY SoLuongTon",Db.P("@nguong",threshold));}
        public decimal Value(){return Convert.ToDecimal(Database.Scalar("SELECT COALESCE(SUM(SoLuongTon*GiaTonKho),0) FROM VatTu"));}
        public int Save(StockVoucherDto voucher)
        {
            return Database.Transaction((cn,tx)=>
            {
                decimal total=voucher.Lines.Sum(x=>x.SoLuong*x.DonGia);
                int id=Convert.ToInt32(Db.One(cn,tx,"INSERT PhieuKho(LoaiPhieu,MaNCC,TongTien) OUTPUT INSERTED.MaPhieu VALUES(@loai,@ncc,@tong)",
                    Db.P("@loai",voucher.LoaiPhieu),new System.Data.SqlClient.SqlParameter("@ncc",SqlDbType.Int){Value=(object)voucher.MaNCC??DBNull.Value},Db.P("@tong",total)));
                // Cùng thứ tự khóa giữa các phiếu để hạn chế deadlock.
                foreach(var line in voucher.Lines.OrderBy(x=>x.MaVatTu,StringComparer.Ordinal))
                {
                    string sql=voucher.LoaiPhieu=="NHAP" ?
                        @"UPDATE VatTu WITH(UPDLOCK) SET GiaTonKho=ROUND((SoLuongTon*GiaTonKho+@sl*@gia)/(SoLuongTon+@sl),2),
                          SoLuongTon=SoLuongTon+@sl WHERE MaVatTu=@ma" :
                        "UPDATE VatTu WITH(UPDLOCK) SET SoLuongTon=SoLuongTon-@sl WHERE MaVatTu=@ma AND SoLuongTon>=@sl";
                    if(Db.Exec(cn,tx,sql,Db.P("@sl",line.SoLuong),Db.P("@gia",line.DonGia),Db.P("@ma",line.MaVatTu))!=1)
                        throw new InvalidOperationException("Không đủ hàng trong kho hoặc vật tư không tồn tại: "+line.MaVatTu);
                    Db.Exec(cn,tx,"INSERT ChiTietPhieuKho VALUES(@phieu,@ma,@sl,@gia)",Db.P("@phieu",id),Db.P("@ma",line.MaVatTu),Db.P("@sl",line.SoLuong),Db.P("@gia",line.DonGia));
                }
                return id;
            });
        }
    }
}
