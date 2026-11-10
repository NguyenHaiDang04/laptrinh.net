using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectTask.BUS;
using ProjectTask.GUI;

namespace ProjectTask.Tests
{
    internal sealed class SmokeTest
    {
        private readonly MainForm form;
        private readonly string output;
        private readonly List<string> log = new List<string>();

        private SmokeTest(MainForm form, string output)
        {
            this.form = form;
            this.output = output;
        }

        public static int Run(string output)
        {
            Directory.CreateDirectory(output);
            var settings = new SqlConnectionStringBuilder(
                Environment.GetEnvironmentVariable("LTNET_BAI6_CONNECTION") ?? "");
            if (!settings.InitialCatalog.StartsWith("LTNET_Bai6_Test_", StringComparison.Ordinal)
                || settings.DataSource != @"(localdb)\MSSQLLocalDB")
            {
                File.WriteAllText(Path.Combine(output, "verification.txt"), "FAIL: Cần CSDL LocalDB dùng riêng để kiểm thử.");
                return 1;
            }

            using (var form = new MainForm())
            {
                var test = new SmokeTest(form, output);
                int result = 0;
                try
                {
                    form.StartPosition = FormStartPosition.Manual;
                    form.Location = new Point(-15000, -15000);
                    form.ShowInTaskbar = false;
                    form.Show();
                    Application.DoEvents();
                    test.Scenario();
                    test.log.Add("RESULT: PASS (" + test.log.Count + " kiểm tra)");
                }
                catch (Exception ex)
                {
                    test.log.Add("FAIL: " + ex);
                    test.log.Add("Thông báo form: " + test.Find<Label>("lblMessage").Text);
                    result = 1;
                }
                finally
                {
                    form.Close();
                    File.WriteAllLines(Path.Combine(output, "verification.txt"), test.log, new UTF8Encoding(true));
                }
                return result;
            }
        }

        private void Scenario()
        {
            var service = new Service();
            DataTable projects = service.DuAn();
            Check(projects.Rows.Count == 2 && service.ThanhVien().Rows.Count == 3, "Script tạo hai dự án và ba thành viên mẫu.");

            var project = Find<ComboBox>("cboProject");
            var filter = Find<ComboBox>("cboFilter");
            var tasks = Find<DataGridView>("gridTasks");
            Tab("tabTasks");
            filter.SelectedItem = "In Progress";
            Check(tasks.Rows.Count == 1 && (string)tasks.Rows[0].Cells["TrangThai"].Value == "In Progress",
                "Lọc theo dự án và trạng thái In Progress.");
            project.SelectedValue = 2;
            Check(tasks.Rows.Count == 0, "Đổi dự án áp dụng đồng thời bộ lọc.");
            project.SelectedValue = 1;
            filter.SelectedIndex = 0;
            Tab("tabKanban");
            Check(Find<ListBox>("listReview").Items.Count == 1 && Find<ListBox>("listDone").Items.Count == 1,
                "Kanban hiển thị Review và Done đúng nhóm.");
            Capture("01_main_screen.png");

            Tab("tabProjects");
            Click("btnNewProject");
            Set("txtProject", "Dự án kiểm thử");
            Click("btnSaveProject");
            int newProject = (int)service.DuAn().Rows.Cast<DataRow>()
                .Single(r => (string)r["TenDuAn"] == "Dự án kiểm thử")["MaDuAn"];
            SelectRow("gridProjects", "MaDuAn", newProject);
            Set("txtProject", "Dự án kiểm thử (đã sửa)");
            Find<ComboBox>("cboProjectState").SelectedItem = "Tạm dừng";
            Click("btnSaveProject");
            Check(service.DuAn().Rows.Cast<DataRow>().Any(r => (int)r["MaDuAn"] == newProject
                && (string)r["TenDuAn"] == "Dự án kiểm thử (đã sửa)" && (string)r["TrangThai"] == "Tạm dừng"),
                "Thêm và sửa dự án từ form.");
            Capture("02_crud_screen.png");
            SelectRow("gridProjects", "MaDuAn", newProject);
            Click("btnDeleteProject");
            Check(service.DuAn().Rows.Count == 2, "Xóa được dự án rỗng.");

            Tab("tabTasks");
            project.SelectedValue = 1;
            Click("btnNewTask");
            const string title = "Kiểm thử phân công";
            Set("txtTask", title);
            Set("txtDescription", "Kiểm tra tạo task và giao cho hai thành viên.");
            Find<ComboBox>("cboTaskState").SelectedItem = "In Progress";
            var members = Find<CheckedListBox>("lstMembers");
            members.SetItemChecked(0, true);
            members.SetItemChecked(1, true);
            Click("btnSaveTask");
            int taskId = (int)service.CongViec(1).Rows.Cast<DataRow>()
                .Single(r => (string)r["TenTask"] == title)["MaTask"];
            Check(service.PhanCong(taskId).Rows.Count == 2, "Tạo task và lưu hai phân công vào SQL Server.");
            SelectRow("gridTasks", "MaTask", taskId);
            Check(members.CheckedItems.Count == 2, "Chọn lại task hiện đúng thành viên đã phân công.");
            Capture("03_transaction_screen.png");
            members.SetItemChecked(1, false);
            members.SetItemChecked(2, true);
            Click("btnSaveTask");
            Check(service.PhanCong(taskId).Rows.Cast<DataRow>().Select(r => (int)r[0]).OrderBy(id => id)
                .SequenceEqual(new[] { 1, 3 }), "Sửa phân công giữ thành viên cũ và đổi thành viên thứ hai.");
            SelectRow("gridTasks", "MaTask", taskId);
            Find<ComboBox>("cboTaskState").SelectedItem = "Done";
            Click("btnMove");
            Check(service.CongViec(1,"Done").Rows.Cast<DataRow>().Any(r => (int)r["MaTask"] == taskId),
                "Nút chuyển trạng thái cập nhật task thành Done.");
            Tab("tabReports");
            Check(service.TreHan(1).Rows.Count == 1 && (int)service.TreHan(1).Rows[0]["MaTask"] == 3,
                "SqlDataReader trả đúng task quá hạn và loại task Done.");
            Check(service.BaoCao(1).Rows.Cast<DataRow>().Any(r => (string)r["HoTen"] == "Nguyễn Văn An"
                && (int)r["DaXong"] == 2 && (decimal)r["TyLe"] == 66.67m),
                "Báo cáo GROUP BY tính đúng 2/3 = 66,67%.");
            Capture("04_report_screen.png");

            Tab("tabTasks");
            Click("btnNewTask");
            const string payload = "A'; DROP TABLE DuAn;--";
            Set("txtTask", payload);
            Click("btnSaveTask");
            Check(service.CongViec(1).Rows.Cast<DataRow>().Any(r => (string)r["TenTask"] == payload)
                && service.DuAn().Rows.Count == 2, "Chuỗi giống SQL injection được lưu nguyên văn và bảng DuAn vẫn còn.");
            Capture("05_error_handling.png");
            SelectRow("gridTasks", "MaTask", taskId);
            Click("btnDeleteTask");
            Check(service.PhanCong(taskId).Rows.Count == 0, "Xóa task và các phân công liên quan.");
        }

        private T Find<T>(string name) where T : Control => (T)form.Controls.Find(name, true).Single();

        private void Tab(string name)
        {
            Find<TabControl>("tabs").SelectedTab = Find<TabPage>(name);
            Application.DoEvents();
        }

        private void Click(string name)
        {
            Find<Button>(name).PerformClick();
            Application.DoEvents();
        }

        private void Set(string name, string value) => Find<TextBox>(name).Text = value;

        private void SelectRow(string grid, string column, object value)
        {
            var view = Find<DataGridView>(grid);
            var row = view.Rows.Cast<DataGridViewRow>().Single(r => Equals(r.Cells[column].Value, value));
            view.CurrentCell = row.Cells.Cast<DataGridViewCell>().First(c => c.Visible);
            Application.DoEvents();
        }

        private void Check(bool condition, string message)
        {
            if (!condition) throw new InvalidOperationException(message);
            log.Add("PASS: " + message);
        }

        private void Capture(string name)
        {
            form.Refresh();
            Application.DoEvents();
            using (var image = new Bitmap(form.Width, form.Height))
            {
                form.DrawToBitmap(image, new Rectangle(Point.Empty, form.Size));
                image.Save(Path.Combine(output, name), ImageFormat.Png);
            }
        }
    }
}
