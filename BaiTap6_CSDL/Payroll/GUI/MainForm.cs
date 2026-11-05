using System;
using System.Data;
using System.Windows.Forms;
using BaiTap6.Shared.GUI;
using Payroll.BUS;
using Payroll.DTO;
namespace Payroll.GUI
{
 public sealed class MainForm:Form
 {
  public readonly TabControl Pages;public DataTable Attendance {get;private set;}
  private readonly PayrollBus bus=new PayrollBus();private readonly DataGridView staff,attendance,salaries;private readonly NumericUpDown month,year;private readonly Label state;private readonly CatalogPanel employeeEditor,departments;private PeriodDto loaded;
  private PeriodDto Period(){return new PeriodDto((int)month.Value,(int)year.Value);}
  public MainForm()
  {
   Ui.Style(this,"Bài 6.2 - Nhân sự và tính lương");Pages=Ui.Tabs(this);
   var main=Ui.Tab(Pages,"Tổng quan nhân sự");staff=Ui.Grid(main);var find=Ui.Row(main);var search=Ui.Text(find,"Tên / CCCD / phòng",220);var department=Ui.Number(find,"Mã phòng (0=tất cả)");Ui.Button(find,"Tìm",()=>staff.DataSource=bus.Employees(search.Text,(int)department.Value));
   var edit=Ui.Tab(Pages,"Hồ sơ nhân viên");employeeEditor=new CatalogPanel(bus.EmployeeEditor);edit.Controls.Add(employeeEditor);
   var pb=Ui.Tab(Pages,"Phòng ban");departments=new CatalogPanel(bus.DepartmentEditor);pb.Controls.Add(departments);
   var cong=Ui.Tab(Pages,"Chấm công tháng");attendance=Ui.Grid(cong);attendance.ReadOnly=false;attendance.DataError+=(s,e)=>{e.ThrowException=false;MessageBox.Show("Chỉ nhập số hợp lệ cho ngày công và OT.");};
   var toolbar=Ui.Row(cong);month=Ui.Number(toolbar,"Tháng",1);month.Maximum=12;month.Value=DateTime.Today.Month;year=Ui.Number(toolbar,"Năm",2000);year.Maximum=2100;year.Value=DateTime.Today.Year;
   Ui.Button(toolbar,"Nạp chấm công",()=>LoadPeriod(Period()));Ui.Button(toolbar,"Lưu chấm công",()=>{attendance.EndEdit();((CurrencyManager)BindingContext[Attendance]).EndCurrentEdit();if(loaded==null||loaded.Key!=Period().Key)throw new ArgumentException("Nạp đúng tháng trước khi lưu.");bus.Save(loaded,Attendance);MessageBox.Show("Đã lưu chấm công.");});
   Ui.Button(toolbar,"Chốt lương tháng",()=>{if(Attendance==null||loaded.Key!=Period().Key)throw new ArgumentException("Nạp tháng trước khi chốt.");attendance.EndEdit();if(Attendance.GetChanges()!=null)throw new ArgumentException("Lưu chấm công trước khi chốt.");if(Ui.Confirm("Chốt lương và khóa chấm công tháng "+loaded.Key+"?")){bus.Close(loaded);LoadPeriod(loaded);Pages.SelectedIndex=4;}});
   state=new Label{AutoSize=true,Margin=new Padding(8)};toolbar.Controls.Add(state);
   var report=Ui.Tab(Pages,"Bảng lương đã chốt");salaries=Ui.Grid(report);Ui.Button(Ui.Row(report),"Tải bảng lương tháng đã chọn",()=>salaries.DataSource=bus.Salaries(Period()));
   Shown+=(s,e)=>Ui.Guard(RefreshData);Pages.SelectedIndexChanged+=(s,e)=>Ui.Guard(RefreshData);
  }
  public void LoadPeriod(PeriodDto p){month.Value=p.Month;year.Value=p.Year;loaded=p;Attendance=bus.Load(p);attendance.DataSource=Attendance;foreach(DataGridViewColumn c in attendance.Columns)c.ReadOnly=c.Name!="SoNgayCong"&&c.Name!="SoGioOT";bool closed=bus.Closed(p);attendance.ReadOnly=closed;state.Text=closed?"Đã chốt - khóa sửa":"Chưa chốt";salaries.DataSource=bus.Salaries(p);}
  public void RefreshData(){staff.DataSource=bus.Employees("",0);employeeEditor.Reload();departments.Reload();salaries.DataSource=bus.Salaries(Period());}
 }
}
