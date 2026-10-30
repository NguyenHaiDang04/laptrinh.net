using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using BaiTap5.Shared.GUI;
using HotelManagement.BUS;
using HotelManagement.DTO;

namespace HotelManagement.GUI
{
    public sealed class MainForm:Form
    {
        private readonly HotelBus bus=new HotelBus();
        public readonly TabControl Pages;
        private readonly DataGridView rooms,usage,report;
        private readonly ComboBox type,state,service;
        private readonly TextBox cccd,name,phone;
        private readonly NumericUpDown quantity;
        private readonly CatalogPanel serviceEditor;
        private DataTable roomData;
        private readonly TextBox invoiceText;
        public MainForm()
        {
            Ui.Style(this,"Bài 5.4 - Quản lý khách sạn");Pages=Ui.Tabs(this);
            var main=Ui.Tab(Pages,"Phòng / Nhận - trả phòng");rooms=Ui.Grid(main);
            var actions=Ui.Row(main);
            Ui.Button(actions,"Nhận phòng",()=>{bus.CheckIn(new CheckInDto{SoPhong=SelectedRoom(),CCCD=cccd.Text,HoTen=name.Text,SoDienThoai=phone.Text});RefreshData();});
            Ui.Button(actions,"Xem tiền tạm",()=>ShowInvoice(bus.Preview(SelectedRoom())));
            Ui.Button(actions,"Thanh toán / Trả phòng",()=>{if(Ui.Confirm("Xác nhận thanh toán và trả phòng?")){var invoice=bus.CheckOut(SelectedRoom());RefreshData();ShowInvoice(invoice);}});
            Ui.Button(actions,"Đã dọn xong",()=>{bus.Clean(SelectedRoom());RefreshData();});
            Ui.Button(actions,"Tải lại",RefreshData);
            var customer=Ui.Row(main);cccd=Ui.Text(customer,"CCCD");name=Ui.Text(customer,"Họ tên",200);phone=Ui.Text(customer,"Điện thoại");
            Ui.Button(customer,"Tra cứu khách",()=>{var dt=bus.Customer(cccd.Text);if(dt.Rows.Count==0){name.Clear();phone.Clear();MessageBox.Show("Khách mới: nhập họ tên và điện thoại.");}else{name.Text=Convert.ToString(dt.Rows[0]["HoTen"]);phone.Text=Convert.ToString(dt.Rows[0]["SoDienThoai"]);}});
            var filters=Ui.Row(main);type=Ui.Combo(filters,"Loại phòng");state=Ui.Combo(filters,"Trạng thái");
            type.Items.AddRange(new object[]{"Tất cả","Đơn","Đôi","VIP"});state.Items.AddRange(new object[]{"Tất cả","Trống","Đang ở","Đang dọn dẹp"});type.SelectedIndex=0;state.SelectedIndex=0;
            type.SelectedIndexChanged+=(s,e)=>ApplyFilter();state.SelectedIndexChanged+=(s,e)=>ApplyFilter();
            var sv=Ui.Tab(Pages,"Gọi dịch vụ");usage=Ui.Grid(sv);var sr=Ui.Row(sv);service=Ui.Combo(sr,"Dịch vụ");quantity=Ui.Number(sr,"Số lượng",1);
            Ui.Button(sr,"Thêm cho phòng đã chọn",()=>{bus.AddService(new ServiceDto{SoPhong=SelectedRoom(),MaDV=Ui.Id(service),SoLuong=(int)quantity.Value});RefreshUsage();});
            Ui.Label(sr,"Chọn phòng ở tab đầu trước khi gọi dịch vụ.");
            var crud=Ui.Tab(Pages,"Quản lý dịch vụ");serviceEditor=new CatalogPanel(bus.ServiceEditor);crud.Controls.Add(serviceEditor);
            var invoicePage=Ui.Tab(Pages,"Hóa đơn");invoiceText=new TextBox{Multiline=true,ReadOnly=true,Dock=DockStyle.Fill,ScrollBars=ScrollBars.Both};invoicePage.Controls.Add(invoiceText);
            Ui.Button(Ui.Row(invoicePage),"Xuất hóa đơn TXT",()=>{if(invoiceText.Text.Length==0)throw new InvalidOperationException("Chưa có hóa đơn.");using(var dlg=new SaveFileDialog{Filter="Văn bản|*.txt",FileName="HoaDonKhachSan.txt"})if(dlg.ShowDialog()==DialogResult.OK)File.WriteAllText(dlg.FileName,invoiceText.Text,Encoding.UTF8);});
            var rp=Ui.Tab(Pages,"Báo cáo lưu trú");report=Ui.Grid(rp);Ui.Button(Ui.Row(rp),"Tải báo cáo",()=>report.DataSource=bus.Report());
            rooms.SelectionChanged+=(s,e)=>Ui.Guard(RefreshUsage);
            Shown+=(s,e)=>Ui.Guard(RefreshData);
            Pages.SelectedIndexChanged+=(s,e)=>Ui.Guard(()=>{RefreshUsage();Ui.Bind(service,bus.Services(),"MaDV","TenDV");serviceEditor.Reload();report.DataSource=bus.Report();});
        }
        public string SelectedRoom()
        { var row=rooms.CurrentRow==null?null:rooms.CurrentRow.DataBoundItem as DataRowView;if(row==null)throw new ArgumentException("Chưa chọn phòng.");return Convert.ToString(row["SoPhong"]); }
        public void RefreshData()
        { roomData=bus.Rooms();ApplyFilter();Ui.Bind(service,bus.Services(),"MaDV","TenDV");serviceEditor.Reload();report.DataSource=bus.Report(); }
        private void ApplyFilter(){if(roomData!=null)rooms.DataSource=HotelBus.Filter(roomData,Convert.ToString(type.SelectedItem),Convert.ToString(state.SelectedItem));}
        private void RefreshUsage(){if(usage!=null&&rooms.CurrentRow!=null)usage.DataSource=bus.Usage(SelectedRoom());}
        public void ShowInvoice(InvoiceDto i)
        {
            invoiceText.Text="HÓA ĐƠN KHÁCH SẠN #"+i.MaPhieu+Environment.NewLine+
                "Phòng: "+i.SoPhong+" | Khách: "+i.HoTen+Environment.NewLine+
                "Nhận phòng: "+i.NgayNhan.ToString("dd/MM/yyyy HH:mm:ss")+Environment.NewLine+
                "Trả / tạm tính đến: "+i.NgayTra.ToString("dd/MM/yyyy HH:mm:ss")+Environment.NewLine+
                "Giờ tính tiền (làm tròn lên): "+i.SoGio+Environment.NewLine+
                "Đơn giá / giờ: "+i.GiaPhong.ToString("N0")+" đ"+Environment.NewLine+
                "Tiền phòng: "+i.TienPhong.ToString("N0")+" đ"+Environment.NewLine+
                "Tiền dịch vụ: "+i.TienDichVu.ToString("N0")+" đ"+Environment.NewLine+
                "TỔNG THANH TOÁN: "+i.TongTien.ToString("N0")+" đ";
            Pages.SelectedIndex=3;
        }
    }
}
