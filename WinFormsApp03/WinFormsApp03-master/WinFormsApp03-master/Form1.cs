namespace WinFormsApp03
{
    public partial class Form1 : Form
    {
        private double resultValue = 0;
        private string operationPerformed = "";
        private bool isOperationPerformed = false;

        public Form1()
        {
            InitializeComponent();

            txtDisplay.Text = "0";
        }
        private void NumberButton_Click(object sender, EventArgs e)
        {
            // Xóa số "0" ở đầu hoặc xóa màn hình nếu vừa bấm một phép toán
            if (txtDisplay.Text == "0" || isOperationPerformed)
            {
                txtDisplay.Clear();
            }

            isOperationPerformed = false;

            // Ép kiểu sender về Button để biết chính xác nút nào vừa được bấm
            Button btn = (Button)sender;

            // Nối chuỗi (Text của nút) vào màn hình hiển thị
            txtDisplay.Text += btn.Text;
        }

        // ==========================================
        // SỰ KIỆN CHUNG CHO CÁC NÚT PHÉP TOÁN (+, -, *, /)
        // ==========================================
        private void OperatorButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            operationPerformed = btn.Text; // Lưu lại dấu phép toán
            resultValue = double.Parse(txtDisplay.Text); // Lưu lại số hạng đầu tiên
            isOperationPerformed = true; // Đánh dấu đã bấm phép toán để nhập số mới
        }

        // ==========================================
        // SỰ KIỆN CHO NÚT BẰNG (=)
        // ==========================================
        private void btnEquals_Click(object sender, EventArgs e)
        {
            double secondValue = double.Parse(txtDisplay.Text);

            switch (operationPerformed)
            {
                case "+":
                    txtDisplay.Text = (resultValue + secondValue).ToString();
                    break;
                case "-":
                    txtDisplay.Text = (resultValue - secondValue).ToString();
                    break;
                case "*":
                    txtDisplay.Text = (resultValue * secondValue).ToString();
                    break;
                case "/":
                    if (secondValue != 0)
                        txtDisplay.Text = (resultValue / secondValue).ToString();
                    else
                        MessageBox.Show("Không thể chia cho 0!");
                    break;
                default:
                    break;
            }

            // Cập nhật lại kết quả và reset trạng thái
            resultValue = double.Parse(txtDisplay.Text);
            operationPerformed = "";
        }

        // ==========================================
        // SỰ KIỆN CHO NÚT CLEAR (C)
        // ==========================================
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            resultValue = 0;
            operationPerformed = "";
        }
    }
}

