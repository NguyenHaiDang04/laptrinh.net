using System;
using System.Drawing;
using System.Windows.Forms;

namespace nguyenhaidangbai5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            // Hàm này gọi giao diện tự động của hệ thống
            InitializeComponent();

            // Gọi hàm thiết kế giao diện bằng code của chúng ta
            SetupResponsiveLayout();
        }

        // Đặt toàn bộ code cấu hình giao diện vào một hàm riêng biệt
        private void SetupResponsiveLayout()
        {
            // 1. Cấu hình TableLayoutPanel
            TableLayoutPanel tlpMain = new TableLayoutPanel();
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.ColumnCount = 2;
            tlpMain.RowCount = 1;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            this.Controls.Add(tlpMain); // Từ khóa 'this' hợp lệ ở đây vì nó nằm trong instance của Form1

            // 2. Cấu hình Cột 1 (GroupBox)
            GroupBox grpInput = new GroupBox { Text = "Thông tin nhập liệu", Dock = DockStyle.Fill };
            TextBox txtName = new TextBox { Location = new Point(20, 40), Width = 150 };
            txtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right; // Tự động co giãn bề ngang
            grpInput.Controls.Add(txtName);
            tlpMain.Controls.Add(grpInput, 0, 0); // Thêm vào Cột 0, Dòng 0

            // 3. Cấu hình Cột 2 (Panel)
            Panel pnlRight = new Panel { Dock = DockStyle.Fill };
            DataGridView dgvData = new DataGridView { Location = new Point(10, 10), Width = 400, Height = 300 };
            dgvData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right; // Tự động co giãn 4 chiều

            Button btnSave = new Button { Text = "Lưu", Location = new Point(335, 330), Width = 75 };
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right; // Neo góc dưới phải

            pnlRight.Controls.Add(dgvData);
            pnlRight.Controls.Add(btnSave);
            tlpMain.Controls.Add(pnlRight, 1, 0); // Thêm vào Cột 1, Dòng 0
        }
    }
}