using System.Drawing;
using System.Windows.Forms;

namespace ProjectTask.GUI
{
    partial class MainForm
    {
        private TabControl tabs;
        private Label lblMessage;
        private TabPage tabTasks;
        private ComboBox cboProject;
        private ComboBox cboFilter;
        private Button btnReload;
        private DataGridView gridTasks;
        private TextBox txtTask;
        private TextBox txtDescription;
        private DateTimePicker dtDue;
        private ComboBox cboPriority;
        private ComboBox cboTaskState;
        private CheckedListBox lstMembers;
        private Button btnNewTask;
        private Button btnSaveTask;
        private Button btnMove;
        private Button btnDeleteTask;
        private TabPage tabProjects;
        private DataGridView gridProjects;
        private TextBox txtProject;
        private ComboBox cboProjectState;
        private DateTimePicker dtStart;
        private DateTimePicker dtEnd;
        private Button btnNewProject;
        private Button btnSaveProject;
        private Button btnDeleteProject;
        private TabPage tabKanban;
        private Label lblKanban;
        private Button btnKanban;
        private ListBox listTodo;
        private ListBox listDoing;
        private ListBox listReview;
        private ListBox listDone;
        private TabPage tabReports;
        private ComboBox cboReport;
        private Button btnReport;
        private DataGridView gridOverdue;
        private DataGridView gridReport;

        private void InitializeComponent()
        {
            this.tabs = new TabControl();
            this.lblMessage = new Label();
            this.tabTasks = new TabPage();
            var label1 = new Label();
            this.cboProject = new ComboBox();
            var label2 = new Label();
            this.cboFilter = new ComboBox();
            this.btnReload = new Button();
            this.gridTasks = new DataGridView();
            var label3 = new Label();
            this.txtTask = new TextBox();
            var label4 = new Label();
            this.txtDescription = new TextBox();
            var label5 = new Label();
            this.dtDue = new DateTimePicker();
            var label6 = new Label();
            this.cboPriority = new ComboBox();
            var label7 = new Label();
            this.cboTaskState = new ComboBox();
            var label8 = new Label();
            this.lstMembers = new CheckedListBox();
            this.btnNewTask = new Button();
            this.btnSaveTask = new Button();
            this.btnMove = new Button();
            this.btnDeleteTask = new Button();
            this.tabProjects = new TabPage();
            this.gridProjects = new DataGridView();
            var label9 = new Label();
            this.txtProject = new TextBox();
            var label10 = new Label();
            this.cboProjectState = new ComboBox();
            var label11 = new Label();
            this.dtStart = new DateTimePicker();
            var label12 = new Label();
            this.dtEnd = new DateTimePicker();
            this.btnNewProject = new Button();
            this.btnSaveProject = new Button();
            this.btnDeleteProject = new Button();
            this.tabKanban = new TabPage();
            this.lblKanban = new Label();
            this.btnKanban = new Button();
            var label13 = new Label();
            this.listTodo = new ListBox();
            var label14 = new Label();
            this.listDoing = new ListBox();
            var label15 = new Label();
            this.listReview = new ListBox();
            var label16 = new Label();
            this.listDone = new ListBox();
            this.tabReports = new TabPage();
            var label17 = new Label();
            this.cboReport = new ComboBox();
            this.btnReport = new Button();
            var label18 = new Label();
            this.gridOverdue = new DataGridView();
            var label19 = new Label();
            this.gridReport = new DataGridView();
            this.SuspendLayout();

            this.tabs.Name = "tabs";
            this.tabs.Location = new Point(12, 12);
            this.tabs.Size = new Size(1032, 578);
            this.tabTasks.Name = "tabTasks";
            this.tabTasks.Text = "Công việc";
            this.tabTasks.UseVisualStyleBackColor = true;
            this.tabs.Controls.Add(this.tabTasks);
            label1.Name = "label1";
            label1.Location = new Point(12, 15);
            label1.Size = new Size(55, 23);
            label1.Text = "Dự án";
            this.tabTasks.Controls.Add(label1);

            this.cboProject.Name = "cboProject";
            this.cboProject.Location = new Point(68, 12);
            this.cboProject.Size = new Size(322, 26);
            this.cboProject.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboProject.SelectedIndexChanged += this.TaskFilterChanged;
            this.tabTasks.Controls.Add(this.cboProject);

            label2.Name = "label2";
            label2.Location = new Point(409, 15);
            label2.Size = new Size(70, 23);
            label2.Text = "Trạng thái";
            this.tabTasks.Controls.Add(label2);

            this.cboFilter.Name = "cboFilter";
            this.cboFilter.Location = new Point(480, 12);
            this.cboFilter.Size = new Size(158, 26);
            this.cboFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboFilter.SelectedIndexChanged += this.TaskFilterChanged;
            this.tabTasks.Controls.Add(this.cboFilter);

            this.btnReload.Name = "btnReload";
            this.btnReload.Location = new Point(660, 10);
            this.btnReload.Size = new Size(95, 30);
            this.btnReload.Text = "Tải lại";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += this.ReloadClick;
            this.tabTasks.Controls.Add(this.btnReload);

            this.gridTasks.Name = "gridTasks";
            this.gridTasks.Location = new Point(12, 52);
            this.gridTasks.Size = new Size(998, 228);
            this.gridTasks.AllowUserToAddRows = false;
            this.gridTasks.AllowUserToDeleteRows = false;
            this.gridTasks.ReadOnly = true;
            this.gridTasks.MultiSelect = false;
            this.gridTasks.RowHeadersVisible = false;
            this.gridTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridTasks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridTasks.BackgroundColor = SystemColors.Window;
            this.gridTasks.CurrentCellChanged += this.TaskSelected;
            this.tabTasks.Controls.Add(this.gridTasks);

            label3.Name = "label3";
            label3.Location = new Point(12, 300);
            label3.Size = new Size(90, 23);
            label3.Text = "Công việc";
            this.tabTasks.Controls.Add(label3);

            this.txtTask.Name = "txtTask";
            this.txtTask.Location = new Point(108, 297);
            this.txtTask.Size = new Size(587, 26);
            this.txtTask.MaxLength = 200;
            this.tabTasks.Controls.Add(this.txtTask);

            label4.Name = "label4";
            label4.Location = new Point(12, 337);
            label4.Size = new Size(90, 23);
            label4.Text = "Mô tả";
            this.tabTasks.Controls.Add(label4);

            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Location = new Point(108, 334);
            this.txtDescription.Size = new Size(587, 61);
            this.txtDescription.MaxLength = 4000;
            this.txtDescription.Multiline = true;
            this.txtDescription.ScrollBars = ScrollBars.Vertical;
            this.tabTasks.Controls.Add(this.txtDescription);

            label5.Name = "label5";
            label5.Location = new Point(12, 414);
            label5.Size = new Size(90, 23);
            label5.Text = "Hạn nộp";
            this.tabTasks.Controls.Add(label5);

            this.dtDue.Name = "dtDue";
            this.dtDue.Location = new Point(108, 411);
            this.dtDue.Size = new Size(154, 26);
            this.dtDue.Format = DateTimePickerFormat.Custom;
            this.dtDue.CustomFormat = "dd/MM/yyyy HH:mm";
            this.tabTasks.Controls.Add(this.dtDue);

            label6.Name = "label6";
            label6.Location = new Point(282, 414);
            label6.Size = new Size(59, 23);
            label6.Text = "Ưu tiên";
            this.tabTasks.Controls.Add(label6);

            this.cboPriority.Name = "cboPriority";
            this.cboPriority.Location = new Point(341, 411);
            this.cboPriority.Size = new Size(128, 26);
            this.cboPriority.DropDownStyle = ComboBoxStyle.DropDownList;
            this.tabTasks.Controls.Add(this.cboPriority);

            label7.Name = "label7";
            label7.Location = new Point(485, 414);
            label7.Size = new Size(78, 23);
            label7.Text = "Trạng thái";
            this.tabTasks.Controls.Add(label7);

            this.cboTaskState.Name = "cboTaskState";
            this.cboTaskState.Location = new Point(565, 411);
            this.cboTaskState.Size = new Size(130, 26);
            this.cboTaskState.DropDownStyle = ComboBoxStyle.DropDownList;
            this.tabTasks.Controls.Add(this.cboTaskState);

            label8.Name = "label8";
            label8.Location = new Point(718, 299);
            label8.Size = new Size(280, 23);
            label8.Text = "Phân công thành viên";
            this.tabTasks.Controls.Add(label8);

            this.lstMembers.Name = "lstMembers";
            this.lstMembers.Location = new Point(718, 324);
            this.lstMembers.Size = new Size(292, 152);
            this.lstMembers.CheckOnClick = true;
            this.tabTasks.Controls.Add(this.lstMembers);

            this.btnNewTask.Name = "btnNewTask";
            this.btnNewTask.Location = new Point(12, 472);
            this.btnNewTask.Size = new Size(130, 30);
            this.btnNewTask.Text = "Công việc mới";
            this.btnNewTask.UseVisualStyleBackColor = true;
            this.btnNewTask.Click += this.NewTaskClick;
            this.tabTasks.Controls.Add(this.btnNewTask);

            this.btnSaveTask.Name = "btnSaveTask";
            this.btnSaveTask.Location = new Point(151, 472);
            this.btnSaveTask.Size = new Size(126, 30);
            this.btnSaveTask.Text = "Lưu công việc";
            this.btnSaveTask.UseVisualStyleBackColor = true;
            this.btnSaveTask.Click += this.SaveTaskClick;
            this.tabTasks.Controls.Add(this.btnSaveTask);

            this.btnMove.Name = "btnMove";
            this.btnMove.Location = new Point(286, 472);
            this.btnMove.Size = new Size(149, 30);
            this.btnMove.Text = "Chuyển trạng thái";
            this.btnMove.UseVisualStyleBackColor = true;
            this.btnMove.Click += this.MoveClick;
            this.tabTasks.Controls.Add(this.btnMove);

            this.btnDeleteTask.Name = "btnDeleteTask";
            this.btnDeleteTask.Location = new Point(444, 472);
            this.btnDeleteTask.Size = new Size(125, 30);
            this.btnDeleteTask.Text = "Xóa công việc";
            this.btnDeleteTask.UseVisualStyleBackColor = true;
            this.btnDeleteTask.Click += this.DeleteTaskClick;
            this.tabTasks.Controls.Add(this.btnDeleteTask);

            this.tabProjects.Name = "tabProjects";
            this.tabProjects.Text = "Dự án";
            this.tabProjects.UseVisualStyleBackColor = true;
            this.tabs.Controls.Add(this.tabProjects);
            this.gridProjects.Name = "gridProjects";
            this.gridProjects.Location = new Point(12, 13);
            this.gridProjects.Size = new Size(998, 270);
            this.gridProjects.AllowUserToAddRows = false;
            this.gridProjects.AllowUserToDeleteRows = false;
            this.gridProjects.ReadOnly = true;
            this.gridProjects.MultiSelect = false;
            this.gridProjects.RowHeadersVisible = false;
            this.gridProjects.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridProjects.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridProjects.BackgroundColor = SystemColors.Window;
            this.gridProjects.CurrentCellChanged += this.ProjectSelected;
            this.tabProjects.Controls.Add(this.gridProjects);

            label9.Name = "label9";
            label9.Location = new Point(12, 307);
            label9.Size = new Size(86, 23);
            label9.Text = "Tên dự án";
            this.tabProjects.Controls.Add(label9);

            this.txtProject.Name = "txtProject";
            this.txtProject.Location = new Point(103, 304);
            this.txtProject.Size = new Size(482, 26);
            this.txtProject.MaxLength = 150;
            this.tabProjects.Controls.Add(this.txtProject);

            label10.Name = "label10";
            label10.Location = new Point(614, 307);
            label10.Size = new Size(86, 23);
            label10.Text = "Trạng thái";
            this.tabProjects.Controls.Add(label10);

            this.cboProjectState.Name = "cboProjectState";
            this.cboProjectState.Location = new Point(705, 304);
            this.cboProjectState.Size = new Size(250, 26);
            this.cboProjectState.DropDownStyle = ComboBoxStyle.DropDownList;
            this.tabProjects.Controls.Add(this.cboProjectState);

            label11.Name = "label11";
            label11.Location = new Point(12, 351);
            label11.Size = new Size(88, 23);
            label11.Text = "Bắt đầu";
            this.tabProjects.Controls.Add(label11);

            this.dtStart.Name = "dtStart";
            this.dtStart.Location = new Point(103, 348);
            this.dtStart.Size = new Size(154, 26);
            this.dtStart.Format = DateTimePickerFormat.Custom;
            this.dtStart.CustomFormat = "dd/MM/yyyy";
            this.tabProjects.Controls.Add(this.dtStart);

            label12.Name = "label12";
            label12.Location = new Point(290, 351);
            label12.Size = new Size(75, 23);
            label12.Text = "Kết thúc";
            this.tabProjects.Controls.Add(label12);

            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Location = new Point(369, 348);
            this.dtEnd.Size = new Size(154, 26);
            this.dtEnd.Format = DateTimePickerFormat.Custom;
            this.dtEnd.CustomFormat = "dd/MM/yyyy";
            this.tabProjects.Controls.Add(this.dtEnd);

            this.btnNewProject.Name = "btnNewProject";
            this.btnNewProject.Location = new Point(12, 414);
            this.btnNewProject.Size = new Size(120, 30);
            this.btnNewProject.Text = "Dự án mới";
            this.btnNewProject.UseVisualStyleBackColor = true;
            this.btnNewProject.Click += this.NewProjectClick;
            this.tabProjects.Controls.Add(this.btnNewProject);

            this.btnSaveProject.Name = "btnSaveProject";
            this.btnSaveProject.Location = new Point(141, 414);
            this.btnSaveProject.Size = new Size(120, 30);
            this.btnSaveProject.Text = "Lưu dự án";
            this.btnSaveProject.UseVisualStyleBackColor = true;
            this.btnSaveProject.Click += this.SaveProjectClick;
            this.tabProjects.Controls.Add(this.btnSaveProject);

            this.btnDeleteProject.Name = "btnDeleteProject";
            this.btnDeleteProject.Location = new Point(270, 414);
            this.btnDeleteProject.Size = new Size(120, 30);
            this.btnDeleteProject.Text = "Xóa dự án";
            this.btnDeleteProject.UseVisualStyleBackColor = true;
            this.btnDeleteProject.Click += this.DeleteProjectClick;
            this.tabProjects.Controls.Add(this.btnDeleteProject);

            this.tabKanban.Name = "tabKanban";
            this.tabKanban.Text = "Bảng Kanban";
            this.tabKanban.UseVisualStyleBackColor = true;
            this.tabs.Controls.Add(this.tabKanban);
            this.lblKanban.Name = "lblKanban";
            this.lblKanban.Location = new Point(12, 15);
            this.lblKanban.Size = new Size(780, 25);
            this.tabKanban.Controls.Add(this.lblKanban);

            this.btnKanban.Name = "btnKanban";
            this.btnKanban.Location = new Point(906, 11);
            this.btnKanban.Size = new Size(104, 30);
            this.btnKanban.Text = "Tải lại";
            this.btnKanban.UseVisualStyleBackColor = true;
            this.btnKanban.Click += this.ReloadKanbanClick;
            this.tabKanban.Controls.Add(this.btnKanban);

            label13.Name = "label13";
            label13.Location = new Point(12, 60);
            label13.Size = new Size(238, 23);
            label13.Text = "To Do";
            this.tabKanban.Controls.Add(label13);

            this.listTodo.Name = "listTodo";
            this.listTodo.Location = new Point(12, 87);
            this.listTodo.Size = new Size(242, 417);
            this.listTodo.HorizontalScrollbar = true;
            this.listTodo.IntegralHeight = false;
            this.tabKanban.Controls.Add(this.listTodo);

            label14.Name = "label14";
            label14.Location = new Point(264, 60);
            label14.Size = new Size(238, 23);
            label14.Text = "In Progress";
            this.tabKanban.Controls.Add(label14);

            this.listDoing.Name = "listDoing";
            this.listDoing.Location = new Point(264, 87);
            this.listDoing.Size = new Size(242, 417);
            this.listDoing.HorizontalScrollbar = true;
            this.listDoing.IntegralHeight = false;
            this.tabKanban.Controls.Add(this.listDoing);

            label15.Name = "label15";
            label15.Location = new Point(516, 60);
            label15.Size = new Size(238, 23);
            label15.Text = "Review";
            this.tabKanban.Controls.Add(label15);

            this.listReview.Name = "listReview";
            this.listReview.Location = new Point(516, 87);
            this.listReview.Size = new Size(242, 417);
            this.listReview.HorizontalScrollbar = true;
            this.listReview.IntegralHeight = false;
            this.tabKanban.Controls.Add(this.listReview);

            label16.Name = "label16";
            label16.Location = new Point(768, 60);
            label16.Size = new Size(238, 23);
            label16.Text = "Done";
            this.tabKanban.Controls.Add(label16);

            this.listDone.Name = "listDone";
            this.listDone.Location = new Point(768, 87);
            this.listDone.Size = new Size(242, 417);
            this.listDone.HorizontalScrollbar = true;
            this.listDone.IntegralHeight = false;
            this.tabKanban.Controls.Add(this.listDone);

            this.tabReports.Name = "tabReports";
            this.tabReports.Text = "Báo cáo";
            this.tabReports.UseVisualStyleBackColor = true;
            this.tabs.Controls.Add(this.tabReports);
            label17.Name = "label17";
            label17.Location = new Point(12, 15);
            label17.Size = new Size(52, 23);
            label17.Text = "Dự án";
            this.tabReports.Controls.Add(label17);

            this.cboReport.Name = "cboReport";
            this.cboReport.Location = new Point(66, 12);
            this.cboReport.Size = new Size(424, 26);
            this.cboReport.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboReport.SelectedIndexChanged += this.ReportChanged;
            this.tabReports.Controls.Add(this.cboReport);

            this.btnReport.Name = "btnReport";
            this.btnReport.Location = new Point(508, 10);
            this.btnReport.Size = new Size(105, 30);
            this.btnReport.Text = "Cập nhật";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += this.ReportClick;
            this.tabReports.Controls.Add(this.btnReport);

            label18.Name = "label18";
            label18.Location = new Point(12, 59);
            label18.Size = new Size(500, 23);
            label18.Text = "Công việc quá hạn chưa hoàn thành";
            this.tabReports.Controls.Add(label18);

            this.gridOverdue.Name = "gridOverdue";
            this.gridOverdue.Location = new Point(12, 85);
            this.gridOverdue.Size = new Size(998, 176);
            this.gridOverdue.AllowUserToAddRows = false;
            this.gridOverdue.AllowUserToDeleteRows = false;
            this.gridOverdue.ReadOnly = true;
            this.gridOverdue.MultiSelect = false;
            this.gridOverdue.RowHeadersVisible = false;
            this.gridOverdue.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridOverdue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridOverdue.BackgroundColor = SystemColors.Window;
            this.tabReports.Controls.Add(this.gridOverdue);

            label19.Name = "label19";
            label19.Location = new Point(12, 283);
            label19.Size = new Size(550, 23);
            label19.Text = "Tỷ lệ hoàn thành theo thành viên";
            this.tabReports.Controls.Add(label19);

            this.gridReport.Name = "gridReport";
            this.gridReport.Location = new Point(12, 309);
            this.gridReport.Size = new Size(998, 212);
            this.gridReport.AllowUserToAddRows = false;
            this.gridReport.AllowUserToDeleteRows = false;
            this.gridReport.ReadOnly = true;
            this.gridReport.MultiSelect = false;
            this.gridReport.RowHeadersVisible = false;
            this.gridReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridReport.BackgroundColor = SystemColors.Window;
            this.tabReports.Controls.Add(this.gridReport);

            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Location = new Point(15, 598);
            this.lblMessage.Size = new Size(1024, 38);
            this.lblMessage.BorderStyle = BorderStyle.FixedSingle;
            this.lblMessage.TextAlign = ContentAlignment.MiddleLeft;
            this.lblMessage.Padding = new Padding(8, 0, 8, 0);

            this.AutoScaleMode = AutoScaleMode.Font;
            this.Font = new Font("Segoe UI", 9F);
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.ClientSize = new Size(1056, 646);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.lblMessage);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Name = "MainForm";
            this.Text = "Bài 6.3 - Quản lý công việc và tiến độ dự án";
            this.Load += this.MainFormLoad;
            this.ResumeLayout(false);
        }
    }
}
