using System;
using System.Drawing;
using System.Windows.Forms;

namespace nguyenhaidangbai4
{
    public partial class Form1 : Form
    {
        // KHÔNG khai báo lại các biến lstMenu, btnRight... ở đây nữa vì file Designer đã có sẵn

        public Form1()
        {
            InitializeComponent();

            // Gắn sự kiện Load cho Form ngay tại hàm khởi tạo
            this.Load += new EventHandler(Form1_Load);
        }

        // ĐÃ XÓA HÀM InitializeComponent() BỊ TRÙNG Ở ĐÂY

        private object GetLstMenu1()
        {
            return lstMenu;
        }

        // Sự kiện khi Form vừa load lên, thêm các món ăn mặc định vào lstMenu
        private void Form1_Load(object sender, EventArgs e)
        {
            lstMenu.Items.Add(new FoodItem("Hamburger", 50));
            lstMenu.Items.Add(new FoodItem("Pizza", 120));
            lstMenu.Items.Add(new FoodItem("Gà Rán", 35));
            lstMenu.Items.Add(new FoodItem("Pepsi", 15));

            CapNhatTongTien();
        }

        // Sự kiện Nút ">": Thêm món từ Menu sang danh sách Đã chọn
        private void btnRight_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn món nào bên lstMenu chưa
            if (lstMenu.SelectedItem != null)
            {
                // Lấy món ăn đang được chọn
                FoodItem selectedFood = (FoodItem)lstMenu.SelectedItem;

                // Thêm vào lstSelected
                lstSelected.Items.Add(selectedFood);

                // Cập nhật lại tổng tiền
                CapNhatTongTien();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một món ăn từ Menu!");
            }
        }

        // Sự kiện Nút "<": Xóa món khỏi danh sách Đã chọn
        private void btnLeft_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng có đang chọn món nào bên lstSelected không
            if (lstSelected.SelectedItem != null)
            {
                // Xóa món đang được chọn
                lstSelected.Items.Remove(lstSelected.SelectedItem);

                // Cập nhật lại tổng tiền
                CapNhatTongTien();
            }
        }

        // Hàm hỗ trợ tính toán và hiển thị tổng tiền động
        private void CapNhatTongTien()
        {
            int total = 0;

            // Duyệt qua tất cả các item trong lstSelected
            foreach (FoodItem item in lstSelected.Items)
            {
                total += item.Price; // Cộng dồn giá tiền
            }
            NewMethod(total);
        }

        private void NewMethod(int total)
        {
            // Cập nhật lên giao diện
            lblTotal.Text = $"Tổng tiền: {total}k";
        }
    }

    // ==========================================
    // LỚP ĐỐI TƯỢNG HỖ TRỢ (CLASS)
    // Để bên ngoài Form1 nhưng vẫn nằm trong namespace
    // ==========================================
    public class FoodItem
    {
        public string Name { get; set; }
        public int Price { get; set; }

        // Hàm khởi tạo (Constructor)
        public FoodItem(string name, int price)
        {
            Name = name;
            Price = price;
        }

        // Ghi đè phương thức ToString() để ListBox tự động hiển thị đúng định dạng
        public override string ToString()
        {
            return $"{Name}: {Price}k";
        }
    }
}