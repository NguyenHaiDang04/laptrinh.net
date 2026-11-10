using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ProjectTask.BUS;
using ProjectTask.DTO;
using static ProjectTask.GUI.FormSupport;

namespace ProjectTask.GUI
{
    internal partial class MainForm : Form
    {
        private readonly Service service = new Service();
        private bool loading = true;
        private int projectId;
        private int taskId;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainFormLoad(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            cboProjectState.Items.AddRange(Service.TrangThaiDuAn);
            cboTaskState.Items.AddRange(Service.TrangThaiTask);
            cboPriority.Items.AddRange(Service.MucUuTien);
            cboFilter.Items.Add("Tất cả");
            cboFilter.Items.AddRange(Service.TrangThaiTask);
            cboFilter.SelectedIndex = 0;

            lstMembers.DisplayMember = "HoTen";
            foreach (DataRowView member in service.ThanhVien().DefaultView)
                lstMembers.Items.Add(member);
            loading = false;
            LoadProjects();
            Notice(lblMessage, "Chọn dự án để xem công việc và phân công thành viên.");
        });

        private void LoadProjects()
        {
            int project = Id(cboProject), report = Id(cboReport);
            loading = true;
            try
            {
                DataTable projects = service.DuAn();
                Grid(gridProjects, projects, "Mã", "Tên dự án", "Bắt đầu", "Kết thúc", "Trạng thái");
                Bind(cboProject, projects.Copy(), "MaDuAn", "TenDuAn");
                Bind(cboReport, projects.Copy(), "MaDuAn", "TenDuAn");
                if (projects.Rows.Cast<DataRow>().Any(r => (int)r["MaDuAn"] == project))
                    cboProject.SelectedValue = project;
                if (projects.Rows.Cast<DataRow>().Any(r => (int)r["MaDuAn"] == report))
                    cboReport.SelectedValue = report;
                ClearProject();
            }
            finally { loading = false; }
            LoadTasks();
            LoadReport();
        }

        private void LoadTasks()
        {
            loading = true;
            try
            {
                string filter = cboFilter.SelectedIndex <= 0 ? "" : cboFilter.Text;
                Grid(gridTasks, service.CongViec(Id(cboProject), filter),
                    "Mã", "Công việc", "Mô tả", "Hạn nộp", "Ưu tiên", "Trạng thái");
                gridTasks.Columns["MaTask"].FillWeight = 38;
                gridTasks.Columns["TenTask"].FillWeight = 155;
                gridTasks.Columns["MoTa"].FillWeight = 175;
                ClearTask();
            }
            finally { loading = false; }
            LoadKanban();
        }

        private void LoadKanban()
        {
            var columns = new[] { listTodo, listDoing, listReview, listDone };
            foreach (ListBox list in columns) list.Items.Clear();
            lblKanban.Text = "Dự án đang xem: " + cboProject.Text;
            foreach (DataRow row in service.CongViec(Id(cboProject)).Rows)
            {
                int index = Array.IndexOf(Service.TrangThaiTask, (string)row["TrangThai"]);
                columns[index].Items.Add("#" + row["MaTask"] + " " + row["TenTask"]);
            }
        }

        private void LoadReport()
        {
            int id = Id(cboReport);
            Grid(gridOverdue, service.TreHan(id), "Mã", "Công việc", "Hạn nộp", "Ưu tiên", "Trạng thái");
            Grid(gridReport, service.BaoCao(id), "Thành viên", "Được giao", "Đã xong", "Tỷ lệ (%)");
        }

        private void ProjectSelected(object sender, EventArgs e)
        {
            if (loading) return;
            DataRow row = Row(gridProjects);
            if (row == null) return;
            projectId = (int)row["MaDuAn"];
            txtProject.Text = (string)row["TenDuAn"];
            dtStart.Value = (DateTime)row["NgayBatDau"];
            dtEnd.Value = (DateTime)row["NgayKetThuc"];
            cboProjectState.SelectedItem = row["TrangThai"];
        }

        private void TaskSelected(object sender, EventArgs e)
        {
            if (loading) return;
            DataRow row = Row(gridTasks);
            if (row == null) return;
            Run(lblMessage, () =>
            {
                taskId = (int)row["MaTask"];
                txtTask.Text = (string)row["TenTask"];
                txtDescription.Text = Convert.ToString(row["MoTa"]);
                dtDue.Value = (DateTime)row["HanNop"];
                cboPriority.SelectedItem = row["DoUuTien"];
                cboTaskState.SelectedItem = row["TrangThai"];
                var members = service.PhanCong(taskId).Rows.Cast<DataRow>()
                    .Select(r => (int)r["MaThanhVien"]).ToArray();
                for (int i = 0; i < lstMembers.Items.Count; i++)
                    lstMembers.SetItemChecked(i, members.Contains((int)((DataRowView)lstMembers.Items[i])["MaThanhVien"]));
            });
        }

        private void ClearProject()
        {
            gridProjects.CurrentCell = null;
            projectId = 0;
            txtProject.Clear();
            dtStart.Value = DateTime.Today;
            dtEnd.Value = DateTime.Today.AddDays(14);
            cboProjectState.SelectedIndex = 0;
        }

        private void ClearTask()
        {
            gridTasks.CurrentCell = null;
            taskId = 0;
            txtTask.Clear();
            txtDescription.Clear();
            dtDue.Value = DateTime.Today.AddDays(1);
            cboPriority.SelectedIndex = 1;
            cboTaskState.SelectedIndex = 0;
            for (int i = 0; i < lstMembers.Items.Count; i++)
                lstMembers.SetItemChecked(i, false);
        }

        private void TaskFilterChanged(object sender, EventArgs e)
        {
            if (!loading) Run(lblMessage, LoadTasks);
        }

        private void ReportChanged(object sender, EventArgs e)
        {
            if (!loading) Run(lblMessage, LoadReport);
        }

        private void ReloadClick(object sender, EventArgs e) => Run(lblMessage, LoadProjects);
        private void ReportClick(object sender, EventArgs e) => Run(lblMessage, LoadReport);
        private void ReloadKanbanClick(object sender, EventArgs e) => Run(lblMessage, LoadKanban);
        private void NewProjectClick(object sender, EventArgs e) => Run(lblMessage, ClearProject);
        private void NewTaskClick(object sender, EventArgs e) => Run(lblMessage, ClearTask);

        private void SaveProjectClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            service.LuuDuAn(new DuAn
            {
                MaDuAn = projectId, TenDuAn = txtProject.Text, NgayBatDau = dtStart.Value.Date,
                NgayKetThuc = dtEnd.Value.Date, TrangThai = cboProjectState.Text
            });
            LoadProjects();
            Notice(lblMessage, "Đã lưu dự án.");
        });

        private void DeleteProjectClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            service.XoaDuAn(projectId);
            LoadProjects();
            Notice(lblMessage, "Đã xóa dự án.");
        });

        private void SaveTaskClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            int id = service.LuuCongViec(new CongViec
            {
                MaTask = taskId, MaDuAn = Id(cboProject), TenTask = txtTask.Text, MoTa = txtDescription.Text,
                HanNop = dtDue.Value, DoUuTien = cboPriority.Text, TrangThai = cboTaskState.Text,
                ThanhVien = lstMembers.CheckedItems.Cast<DataRowView>().Select(v => (int)v["MaThanhVien"]).ToArray()
            });
            LoadTasks();
            LoadReport();
            Notice(lblMessage, "Đã lưu công việc #" + id + " và danh sách phân công.");
        });

        private void MoveClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            string state = cboTaskState.Text;
            service.ChuyenTrangThai(taskId, state);
            LoadTasks();
            LoadReport();
            Notice(lblMessage, "Đã chuyển trạng thái sang " + state + ".");
        });

        private void DeleteTaskClick(object sender, EventArgs e) => Run(lblMessage, () =>
        {
            service.XoaCongViec(taskId);
            LoadTasks();
            LoadReport();
            Notice(lblMessage, "Đã xóa công việc.");
        });
    }
}
