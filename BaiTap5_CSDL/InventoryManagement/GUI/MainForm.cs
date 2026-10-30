using System;
using System.ComponentModel;
using System.Windows.Forms;
using BaiTap5.Shared.GUI;
using InventoryManagement.BUS;
using InventoryManagement.DTO;

namespace InventoryManagement.GUI
{
    public sealed class MainForm:Form
    {
        private readonly InventoryBus bus=new InventoryBus();
        public readonly TabControl Pages;
        private readonly DataGridView stock,linesGrid,vouchers,low;
        private readonly ComboBox kind,supplier,material;
        private readonly TextBox search;
        private readonly NumericUpDown quantity,price,threshold;
        private readonly Label value,amount;
        private readonly CatalogPanel materialEditor,supplierEditor;
        private readonly BindingList<StockLineDto> lines=new BindingList<StockLineDto>();
        public MainForm()
        {
            Ui.Style(this,"Bài 5.5 - Quản lý xuất nhập kho");Pages=Ui.Tabs(this);
            var main=Ui.Tab(Pages,"Tổng quan tồn kho");stock=Ui.Grid(main);var row=Ui.Row(main);search=Ui.Text(row,"Mã / tên vật tư",220);
            Ui.Button(row,"Tìm kiếm",()=>stock.DataSource=bus.Materials(search.Text));Ui.Button(row,"Tải lại",RefreshData);
            var vt=Ui.Tab(Pages,"Quản lý vật tư");materialEditor=new CatalogPanel(bus.MaterialEditor);vt.Controls.Add(materialEditor);
            var nc=Ui.Tab(Pages,"Nhà cung cấp");supplierEditor=new CatalogPanel(bus.SupplierEditor);nc.Controls.Add(supplierEditor);
            var voucher=Ui.Tab(Pages,"Lập phiếu nhập / xuất");linesGrid=Ui.Grid(voucher);linesGrid.DataSource=lines;
            var save=Ui.Row(voucher);
            Ui.Button(save,"Lưu phiếu kho",()=>
            {
                var dto=new StockVoucherDto{LoaiPhieu=Convert.ToString(kind.SelectedItem),MaNCC=kind.SelectedIndex==0?(int?)Ui.Id(supplier):null};
                foreach(var line in lines)dto.Lines.Add(new StockLineDto{MaVatTu=line.MaVatTu,SoLuong=line.SoLuong,DonGia=line.DonGia});
                int id=bus.Save(dto);lines.Clear();RefreshData();MessageBox.Show("Đã lưu phiếu #"+id);
            });
            Ui.Button(save,"Xóa dòng",()=>{if(linesGrid.CurrentRow!=null)lines.Remove((StockLineDto)linesGrid.CurrentRow.DataBoundItem);UpdateTotal();});
            Ui.Button(save,"Hủy phiếu tạm",()=>{lines.Clear();UpdateTotal();});amount=new Label{AutoSize=true,Margin=new Padding(10)};save.Controls.Add(amount);
            var add=Ui.Row(voucher);material=Ui.Combo(add,"Vật tư");quantity=Ui.Number(add,"Số lượng",0.01m,2);quantity.Value=1;price=Ui.Number(add,"Đơn giá",0,2);
            Ui.Button(add,"Thêm dòng",()=>{if(material.SelectedValue==null)throw new ArgumentException("Chọn vật tư.");string code=Convert.ToString(material.SelectedValue);
                foreach(var line in lines)if(line.MaVatTu==code)throw new ArgumentException("Vật tư đã có trên phiếu, hãy xóa dòng cũ để nhập lại.");
                lines.Add(new StockLineDto{MaVatTu=code,SoLuong=quantity.Value,DonGia=price.Value});UpdateTotal();});
            var header=Ui.Row(voucher);kind=Ui.Combo(header,"Loại phiếu");kind.Items.AddRange(new object[]{"NHAP","XUAT"});kind.SelectedIndex=0;
            supplier=Ui.Combo(header,"Nhà cung cấp");kind.SelectedIndexChanged+=(s,e)=>supplier.Enabled=kind.SelectedIndex==0;
            var rp=Ui.Tab(Pages,"Báo cáo / Cảnh báo");low=Ui.Grid(rp);var reportRow=Ui.Row(rp);threshold=Ui.Number(reportRow,"Ngưỡng an toàn",0,2);threshold.Value=10;
            Ui.Button(reportRow,"Lọc tồn thấp",RefreshReport);value=new Label{AutoSize=true,Margin=new Padding(10)};reportRow.Controls.Add(value);
            var history=Ui.Tab(Pages,"Lịch sử phiếu kho");vouchers=Ui.Grid(history);
            Shown+=(s,e)=>Ui.Guard(RefreshData);
            Pages.SelectedIndexChanged+=(s,e)=>Ui.Guard(RefreshData);
        }
        private void UpdateTotal(){decimal total=0;foreach(var line in lines)total+=line.SoLuong*line.DonGia;amount.Text="Tổng phiếu: "+total.ToString("N0")+" đ";}
        private void RefreshReport(){low.DataSource=bus.LowStock(threshold.Value);value.Text="Tổng giá trị tồn (giá vốn bình quân): "+bus.Value().ToString("N0")+" đ";}
        public void RefreshData()
        {
            stock.DataSource=bus.Materials(search.Text);Ui.Bind(material,bus.Materials(""),"MaVatTu","TenVatTu");Ui.Bind(supplier,bus.Suppliers(),"MaNCC","TenNCC");
            materialEditor.Reload();supplierEditor.Reload();vouchers.DataSource=bus.Vouchers();RefreshReport();UpdateTotal();
        }
    }
}
