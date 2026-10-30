using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BaiTap5.Shared.GUI;
using CafePOS.BUS;
using CafePOS.DTO;

namespace CafePOS.GUI
{
    public sealed class MainForm : Form
    {
        private readonly CafeBus bus = new CafeBus();
        public readonly TabControl Pages;
        private readonly FlowLayoutPanel tables;
        private readonly DataGridView details, report;
        private readonly ComboBox category, menu, target;
        private readonly NumericUpDown quantity;
        private readonly Label summary;
        private readonly CatalogPanel categoriesEditor, menuEditor;
        private int selectedTable;
        public MainForm()
        {
            Ui.Style(this,"Bài 5.3 - Quản lý quán cà phê");
            Pages=Ui.Tabs(this);
            var order=Ui.Tab(Pages,"Sơ đồ bàn / Gọi món");
            details=Ui.Grid(order);
            tables=new FlowLayoutPanel { Dock=DockStyle.Left, Width=300, AutoScroll=true, Padding=new Padding(8) }; order.Controls.Add(tables);
            var row=Ui.Row(order);
            category=Ui.Combo(row,"Danh mục"); menu=Ui.Combo(row,"Món"); quantity=Ui.Number(row,"Số lượng",1);
            Ui.Button(row,"Thêm món",()=> { bus.Order(new OrderDto { MaBan=selectedTable,MaMon=Ui.Id(menu),SoLuong=(int)quantity.Value }); RefreshData(); });
            var row2=Ui.Row(order); target=Ui.Combo(row2,"Bàn đích");
            Ui.Button(row2,"Đổi bàn",()=> { int dest=Ui.Id(target); bus.Move(selectedTable,dest); selectedTable=dest; RefreshData(); });
            Ui.Button(row2,"Thanh toán",()=> { if(Ui.Confirm("Thanh toán hóa đơn của bàn đã chọn?")) { decimal total=bus.Checkout(selectedTable); RefreshData(); MessageBox.Show("Đã thanh toán: "+total.ToString("N0")+" đ"); } });
            Ui.Button(row2,"Tải lại",RefreshData);
            summary=new Label { AutoSize=true,Margin=new Padding(10) }; row2.Controls.Add(summary);
            var cat=Ui.Tab(Pages,"Quản lý danh mục"); categoriesEditor=new CatalogPanel(bus.CategoriesEditor); cat.Controls.Add(categoriesEditor);
            var food=Ui.Tab(Pages,"Quản lý thực đơn"); menuEditor=new CatalogPanel(bus.MenuEditor); food.Controls.Add(menuEditor);
            var rpt=Ui.Tab(Pages,"Báo cáo hóa đơn"); report=Ui.Grid(rpt); Ui.Button(Ui.Row(rpt),"Tải báo cáo",()=>report.DataSource=bus.Report());
            category.SelectedIndexChanged+=(s,e)=> { if(category.SelectedValue is int) Ui.Guard(()=>Ui.Bind(menu,bus.Menu(Ui.Id(category)),"MaMon","TenMon")); };
            Shown+=(s,e)=>Ui.Guard(RefreshData);
            Pages.SelectedIndexChanged+=(s,e)=>Ui.Guard(RefreshData);
        }
        public void SelectTable(int id) { selectedTable=id; RefreshData(); }
        public void RefreshData()
        {
            var dt=bus.Tables();
            while(tables.Controls.Count>0) tables.Controls[0].Dispose();
            foreach(DataRow item in dt.Rows)
            {
                int id=Convert.ToInt32(item["MaBan"]);
                var b=Ui.Button(tables,item["TenBan"]+"\n"+item["TrangThai"],()=>SelectTable(id));
                b.AutoSize=false;b.Size=new Size(125,85);b.BackColor=Convert.ToString(item["TrangThai"])=="Trống"?Color.LightGreen:Color.LightCoral;
                if(id==selectedTable)b.FlatStyle=FlatStyle.Popup;
            }
            if(selectedTable==0&&dt.Rows.Count>0) selectedTable=Convert.ToInt32(dt.Rows[0]["MaBan"]);
            Ui.Bind(target,dt.Copy(),"MaBan","TenBan"); Ui.Bind(category,bus.Categories(),"MaDanhMuc","TenDanhMuc");
            details.DataSource=bus.Details(selectedTable); decimal total=0;
            foreach(DataRow row in ((DataTable)details.DataSource).Rows) total+=Convert.ToDecimal(row["ThanhTien"]);
            summary.Text="Bàn #"+selectedTable+" | Tổng tạm: "+total.ToString("N0")+" đ";
            categoriesEditor.Reload();menuEditor.Reload();report.DataSource=bus.Report();
        }
    }
}
