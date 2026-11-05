using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using BaiTap6.Shared.GUI;
using Payroll.BUS;
using Payroll.DTO;

namespace Payroll.GUI
{
 public partial class MainForm : Form
 {
  public DataTable Attendance { get; private set; }
  private PayrollBus bus;
  private CatalogPanel employeeEditor, departments;
  private PeriodDto loaded;
  private PeriodDto Period() { return new PeriodDto((int)month.Value, (int)year.Value); }

  public MainForm()
  {
   InitializeComponent();
   if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
   bus = new PayrollBus();
   employeeEditor = new CatalogPanel(bus.EmployeeEditor);
   departments = new CatalogPanel(bus.DepartmentEditor);
   employeePage.Controls.Add(employeeEditor);
   departmentPage.Controls.Add(departments);
   month.Value = DateTime.Today.Month;
   year.Value = DateTime.Today.Year;
   attendance.ReadOnly = false;
  }

  private void SearchButton_Click(object sender, EventArgs e)
  { Ui.Guard(() => staff.DataSource = bus.Employees(search.Text, (int)department.Value)); }
  private void LoadButton_Click(object sender, EventArgs e)
  { Ui.Guard(() => LoadPeriod(Period())); }
  private void SaveButton_Click(object sender, EventArgs e)
  {
   Ui.Guard(() =>
   {
    if (Attendance == null || loaded == null || loaded.Key != Period().Key)
     throw new ArgumentException("Nạp đúng tháng trước khi lưu.");
    attendance.EndEdit();
    ((CurrencyManager)BindingContext[Attendance]).EndCurrentEdit();
    bus.Save(loaded, Attendance);
    MessageBox.Show("Đã lưu chấm công.");
   });
  }
  private void CloseButton_Click(object sender, EventArgs e)
  {
   Ui.Guard(() =>
   {
    if (Attendance == null || loaded == null || loaded.Key != Period().Key)
     throw new ArgumentException("Nạp tháng trước khi chốt.");
    attendance.EndEdit();
    ((CurrencyManager)BindingContext[Attendance]).EndCurrentEdit();
    if (Attendance.GetChanges() != null) throw new ArgumentException("Lưu chấm công trước khi chốt.");
    if (!Ui.Confirm("Chốt lương và khóa chấm công tháng " + loaded.Key + "?")) return;
    bus.Close(loaded);
    LoadPeriod(loaded);
    Pages.SelectedIndex = 4;
   });
  }
  private void Attendance_DataError(object sender, DataGridViewDataErrorEventArgs e)
  {
   e.ThrowException = false;
   MessageBox.Show("Chỉ nhập số hợp lệ cho ngày công và OT.");
  }
  private void ReportButton_Click(object sender, EventArgs e)
  { Ui.Guard(() => salaries.DataSource = bus.Salaries(Period())); }
  private void MainForm_Shown(object sender, EventArgs e) { if (bus != null) Ui.Guard(RefreshData); }
  private void Pages_SelectedIndexChanged(object sender, EventArgs e) { if (bus != null) Ui.Guard(RefreshData); }
  public void LoadPeriod(PeriodDto p){month.Value=p.Month;year.Value=p.Year;loaded=p;Attendance=bus.Load(p);attendance.DataSource=Attendance;foreach(DataGridViewColumn c in attendance.Columns)c.ReadOnly=c.Name!="SoNgayCong"&&c.Name!="SoGioOT";bool closed=bus.Closed(p);attendance.ReadOnly=closed;state.Text=closed?"Đã chốt - khóa sửa":"Chưa chốt";salaries.DataSource=bus.Salaries(p);}
  public void RefreshData(){staff.DataSource=bus.Employees("",0);employeeEditor.Reload();departments.Reload();salaries.DataSource=bus.Salaries(Period());}
 }
}
