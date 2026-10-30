using System;
using System.Data;
using System.Linq;
using BaiTap5.Shared.BUS;
using BaiTap5.Shared.DTO;
using InventoryManagement.DAL;
using InventoryManagement.DTO;

namespace InventoryManagement.BUS
{
    public sealed class InventoryBus
    {
        private readonly InventoryDal dal=new InventoryDal();
        public ICatalogService MaterialEditor { get; private set; }
        public ICatalogService SupplierEditor { get; private set; }
        public InventoryBus()
        {
            // Tồn kho, giá vốn chỉ thay đổi qua phiếu nhập/xuất; không sửa tay trong CRUD.
            MaterialEditor=new CatalogBus(dal.Database,new CatalogDefinition("Vật tư","VatTu","MaVatTu",false,
                new Field("MaVatTu","Mã vật tư",typeof(string),true,20),new Field("TenVatTu","Tên vật tư",typeof(string),true,150),new Field("DVT","Đơn vị tính",typeof(string),true,20)));
            SupplierEditor=new CatalogBus(dal.Database,new CatalogDefinition("Nhà cung cấp","NhaCungCap","MaNCC",true,
                new Field("TenNCC","Tên nhà cung cấp",typeof(string),true,150),new Field("DienThoai","Điện thoại",typeof(string),false,15),new Field("DiaChi","Địa chỉ",typeof(string),false,255)));
        }
        public DataTable Materials(string keyword){return dal.Materials(keyword);}
        public DataTable Suppliers(){return dal.Suppliers();}
        public DataTable Vouchers(){return dal.Vouchers();}
        public DataTable LowStock(decimal threshold){if(threshold<0)throw new ArgumentException("Ngưỡng không được âm.");return dal.LowStock(threshold);}
        public decimal Value(){return dal.Value();}
        public int Save(StockVoucherDto voucher)
        {
            if(voucher==null||(voucher.LoaiPhieu!="NHAP"&&voucher.LoaiPhieu!="XUAT"))throw new ArgumentException("Loại phiếu phải là NHAP hoặc XUAT.");
            if(voucher.LoaiPhieu=="NHAP"&&(!voucher.MaNCC.HasValue||voucher.MaNCC<=0))throw new ArgumentException("Phiếu nhập cần nhà cung cấp.");
            if(voucher.Lines==null||voucher.Lines.Count==0)throw new ArgumentException("Phiếu cần ít nhất một vật tư.");
            foreach(var line in voucher.Lines)
            {
                if(line==null||string.IsNullOrWhiteSpace(line.MaVatTu)||line.MaVatTu.Length>20)throw new ArgumentException("Mã vật tư không hợp lệ.");
                line.MaVatTu=line.MaVatTu.Trim();
                if(line.SoLuong<=0||line.DonGia<0)throw new ArgumentException("Số lượng phải dương, đơn giá không được âm.");
                if(decimal.Round(line.SoLuong,2)!=line.SoLuong||decimal.Round(line.DonGia,2)!=line.DonGia)throw new ArgumentException("Số lượng, đơn giá tối đa 2 chữ số thập phân.");
            }
            if(voucher.Lines.Select(l=>l.MaVatTu).Distinct(StringComparer.OrdinalIgnoreCase).Count()!=voucher.Lines.Count)
                throw new ArgumentException("Mỗi vật tư chỉ được có một dòng trên phiếu. Hãy gộp số lượng.");
            if(voucher.LoaiPhieu=="XUAT")voucher.MaNCC=null;
            return dal.Save(voucher);
        }
    }
}
