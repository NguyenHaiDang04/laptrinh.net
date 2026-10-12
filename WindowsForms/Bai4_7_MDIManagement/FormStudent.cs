using System;
using System.Windows.Forms;

namespace Bai4_7_MDIManagement // Sửa tên này nếu Project của bạn tên khác nhé
{
    // ========================================================
    // PHẦN 1: CODE XỬ LÝ LOGIC (Các nút bấm, sự kiện)
    // ========================================================
    public partial class FormStudent : Form
    {
        public FormStudent()
        {
            InitializeComponent();
        }

        private void FormStudent_Load(object sender, EventArgs e)
        {
            // Thêm dữ liệu mẫu vào DataGridView
            dgvSinhVien.Columns.Add("MaSV", "Mã SV");
            dgvSinhVien.Columns.Add("Ten", "Họ tên");
            dgvSinhVien.Rows.Add("SV01", "Nguyễn Hải Đăng");
            dgvSinhVien.Rows.Add("SV02", "Trần Văn Dũng");
            dgvSinhVien.Rows.Add("SV03", "Trịnh Ngọc Hương");
            dgvSinhVien.Rows.Add("SV04", "Trịnh Hải Yến");
        }

        // HÀM GIAO TIẾP VỚI FORM CHA (FormMain)
        private void GuiThongBaoLenCha(string text)
        {
            if (this.MdiParent is FormMain frmChinh)
            {
                frmChinh.CapNhatTrangThai(text);
            }
        }

        private void ctxLamMoi_Click(object sender, EventArgs e)
        {
            GuiThongBaoLenCha("Đã làm mới danh sách Sinh viên lúc " + DateTime.Now.ToShortTimeString());
        }

        private void ctxXoa_Click(object sender, EventArgs e)
        {
            if (dgvSinhVien.SelectedRows.Count > 0)
            {
                dgvSinhVien.Rows.Remove(dgvSinhVien.SelectedRows[0]);
                GuiThongBaoLenCha("Thao tác: Xóa thành công 1 dòng dữ liệu sinh viên.");
            }
            else
            {
                MessageBox.Show("Vui lòng bôi đen 1 dòng để xóa!");
            }
        }

        private void ctxXemChiTiet_Click(object sender, EventArgs e)
        {
            GuiThongBaoLenCha("Đang mở hộp thoại Xem chi tiết sinh viên...");
            MessageBox.Show("Chức năng xem chi tiết đang phát triển!", "Chi tiết");
        }
    }

    // ========================================================
    // PHẦN 2: CODE TẠO GIAO DIỆN (Nằm chung file luôn cho tiện)
    // ========================================================
    public partial class FormStudent
    {
        private System.ComponentModel.IContainer? components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            dgvSinhVien = new DataGridView();
            contextMenuStrip1 = new ContextMenuStrip(components);
            ctxLamMoi = new ToolStripMenuItem();
            ctxXoa = new ToolStripMenuItem();
            ctxXemChiTiet = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dgvSinhVien).BeginInit();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // dgvSinhVien
            dgvSinhVien.AllowUserToAddRows = false;
            dgvSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSinhVien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSinhVien.ContextMenuStrip = contextMenuStrip1;
            dgvSinhVien.Dock = DockStyle.Fill;
            dgvSinhVien.Location = new Point(0, 0);
            dgvSinhVien.Name = "dgvSinhVien";
            dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSinhVien.Size = new Size(600, 350);
            dgvSinhVien.TabIndex = 0;
            // contextMenuStrip1
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { ctxLamMoi, ctxXoa, ctxXemChiTiet });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(164, 70);
            // ctxLamMoi
            ctxLamMoi.Text = "Làm mới dữ liệu";
            ctxLamMoi.Click += ctxLamMoi_Click;
            // ctxXoa
            ctxXoa.Text = "Xóa dòng chọn";
            ctxXoa.Click += ctxXoa_Click;
            // ctxXemChiTiet
            ctxXemChiTiet.Text = "Xem chi tiết";
            ctxXemChiTiet.Click += ctxXemChiTiet_Click;
            // FormStudent
            ClientSize = new Size(600, 350);
            Controls.Add(dgvSinhVien);
            Name = "FormStudent";
            Text = "Quản lý Sinh viên (MDI Child)";
            Load += FormStudent_Load;
            ((System.ComponentModel.ISupportInitialize)dgvSinhVien).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView dgvSinhVien;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ctxLamMoi;
        private System.Windows.Forms.ToolStripMenuItem ctxXoa;
        private System.Windows.Forms.ToolStripMenuItem ctxXemChiTiet;
    }
}