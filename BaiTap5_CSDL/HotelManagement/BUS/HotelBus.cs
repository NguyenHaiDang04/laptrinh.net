using System;
using System.Data;
using System.Text.RegularExpressions;
using BaiTap5.Shared.DTO;
using BaiTap5.Shared.BUS;
using HotelManagement.DAL;
using HotelManagement.DTO;

namespace HotelManagement.BUS
{
    public sealed class HotelBus
    {
        private readonly HotelDal dal=new HotelDal();
        public ICatalogService ServiceEditor { get; private set; }
        public HotelBus()
        { ServiceEditor=new CatalogBus(dal.Database,new CatalogDefinition("Dịch vụ","DichVu","MaDV",true,new Field("TenDV","Tên dịch vụ",typeof(string)),new Field("DonGia","Đơn giá",typeof(decimal)))); }
        public DataTable Rooms(){return dal.Rooms();}
        public DataTable Services(){return dal.Services();}
        public DataTable Customer(string cccd){return dal.Customer((cccd??"").Trim());}
        public DataTable Usage(string room){return dal.Usage(room);}
        public DataTable Report(){return dal.Report();}
        public int CheckIn(CheckInDto dto)
        {
            dto.HoTen=(dto.HoTen??"").Trim();dto.CCCD=(dto.CCCD??"").Trim();dto.SoDienThoai=(dto.SoDienThoai??"").Trim();
            if(string.IsNullOrWhiteSpace(dto.SoPhong))throw new ArgumentException("Chọn phòng.");
            if(dto.HoTen.Length==0||dto.HoTen.Length>100)throw new ArgumentException("Họ tên cần có từ 1 đến 100 ký tự.");
            if(!Regex.IsMatch(dto.CCCD,@"^\d{12}$"))throw new ArgumentException("CCCD phải có đúng 12 chữ số.");
            if(dto.SoDienThoai.Length>0&&!Regex.IsMatch(dto.SoDienThoai,@"^\d{9,15}$"))throw new ArgumentException("Số điện thoại phải có 9–15 chữ số.");
            return dal.CheckIn(dto);
        }
        public void AddService(ServiceDto dto)
        { if(string.IsNullOrWhiteSpace(dto.SoPhong)||dto.MaDV<=0||dto.SoLuong<=0)throw new ArgumentException("Chọn phòng, dịch vụ và số lượng lớn hơn 0.");dal.AddService(dto); }
        public InvoiceDto Preview(string room){return dal.Preview(room);}
        public InvoiceDto CheckOut(string room){return dal.CheckOut(room);}
        public void Clean(string room){dal.Clean(room);}
        private static string Escape(string text){return text.Replace("'","''");}
        public static DataView Filter(DataTable rooms,string type,string state)
        {
            var view=new DataView(rooms);string filter="";
            if(type!="Tất cả")filter="LoaiPhong='"+Escape(type)+"'";
            if(state!="Tất cả")filter+=(filter.Length==0?"":" AND ")+"TrangThai='"+Escape(state)+"'";
            view.RowFilter=filter;return view;
        }
    }
}
