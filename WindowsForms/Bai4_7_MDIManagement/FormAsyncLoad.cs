using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai4_7_MDIManagement // Chú ý: Giữ nguyên Namespace của Bài 4.7 để liên kết MDI
{
    // Lớp đối tượng sinh viên đơn giản
    public class StudentAsync
    {
        public string StudentId { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
    }

    // ========================================================
    // PHẦN 1: CODE XỬ LÝ LOGIC (Async / Await)
    // ========================================================
    public partial class FormAsyncLoad : Form
    {
        public FormAsyncLoad()
        {
            InitializeComponent();
        }

        // Hàm giao tiếp với FormMain (Form cha) để cập nhật StatusStrip
        private void GuiThongBaoLenCha(string text)
        {
            if (this.MdiParent is FormMain frmChinh)
            {
                frmChinh.CapNhatTrangThai(text);
            }
        }

        // Sự kiện Click TẢI DỮ LIỆU - Phải có từ khóa 'async'
        private async void btnLoad_Click(object sender, EventArgs e)
        {
            // 1. Khóa giao diện tránh click đúp
            btnLoad.Enabled = false;
            progressBar1.Value = 0;
            lblPercent.Text = "0%";
            dgvData.DataSource = null;
            GuiThongBaoLenCha("Đang nạp dữ liệu... Vui lòng đợi.");

            // 2. Khởi tạo đối tượng IProgress để nhận % từ luồng ngầm báo về UI Thread
            var progress = new Progress<int>(percent =>
            {
                progressBar1.Value = percent;
                lblPercent.Text = $"{percent}%";
                GuiThongBaoLenCha($"Tiến trình nạp dữ liệu: {percent}%"); // Gửi thẳng lên FormMain
            });

            try
            {
                // 3. Gọi hàm chạy ngầm (Task.Run) và CHỜ (await) nó làm xong
                List<StudentAsync> data = await LoadDataAsync(progress);

                // 4. Nạp xong thì đưa lên DataGridView
                dgvData.DataSource = data;
                GuiThongBaoLenCha("Nạp dữ liệu hoàn tất!");
                MessageBox.Show("Nạp dữ liệu thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi quá trình: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                GuiThongBaoLenCha("Lỗi nạp dữ liệu.");
            }
            finally
            {
                // 5. Khối Finally luôn chạy dù thành công hay lỗi -> Mở khóa lại nút bấm
                btnLoad.Enabled = true;
            }
        }

        // HÀM XỬ LÝ NGẦM: Giả lập việc tải dữ liệu nặng từ Database
        private Task<List<StudentAsync>> LoadDataAsync(IProgress<int> progress)
        {
            // Đẩy toàn bộ quá trình này sang một Thread khác để không làm đơ UI
            return Task.Run(async () =>
            {
                List<StudentAsync> list = new List<StudentAsync>();
                int totalRecords = 50; // Giả lập nạp 50 bản ghi

                for (int i = 1; i <= totalRecords; i++)
                {
                    // Cố tình tạo độ trễ (100ms) để nhìn rõ ProgressBar chạy
                    await Task.Delay(100);

                    list.Add(new StudentAsync
                    {
                        StudentId = "SV" + i.ToString("D3"),
                        FullName = "Sinh viên tải ngầm số " + i,
                        Age = 18 + (i % 5)
                    });

                    // Tính toán phần trăm và báo cáo lại cho đối tượng Progress ở UI
                    if (progress != null)
                    {
                        int percentComplete = (i * 100) / totalRecords;
                        progress.Report(percentComplete);
                    }
                }
                return list;
            });
        }
    }

    // ========================================================
    // PHẦN 2: CODE TẠO GIAO DIỆN TỰ ĐỘNG
    // ========================================================
    public partial class FormAsyncLoad
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.DataGridView dgvData;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnLoad = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblPercent = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();

            // btnLoad
            this.btnLoad.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnLoad.ForeColor = System.Drawing.Color.White;
            this.btnLoad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLoad.Location = new System.Drawing.Point(20, 20);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(120, 35);
            this.btnLoad.Text = "Tải dữ liệu";
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);

            // progressBar1
            this.progressBar1.Location = new System.Drawing.Point(160, 22);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(340, 30);

            // lblPercent
            this.lblPercent.AutoSize = true;
            this.lblPercent.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPercent.Location = new System.Drawing.Point(510, 26);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Text = "0%";

            // dgvData
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(20, 80);
            this.dgvData.Name = "dgvData";
            this.dgvData.Size = new System.Drawing.Size(530, 250);

            // FormAsyncLoad
            this.ClientSize = new System.Drawing.Size(580, 360);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnLoad);
            this.Name = "FormAsyncLoad";
            this.Text = "Nạp dữ liệu Bất đồng bộ (Async/Await)";
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}