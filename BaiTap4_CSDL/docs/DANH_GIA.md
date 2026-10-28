# KIỂM TRA BÀI 4.1 VÀ 4.2

Kết quả: **42/42 kiểm tra đạt** trên SQL Server LocalDB. Solution biên dịch thành công.

## Nội dung kiểm tra

| Chức năng | Cách kiểm tra |
|---|---|
| Dữ liệu đăng nhập trống | Kiểm tra ErrorProvider trên cả hai ô |
| Enter / Esc | Gọi phím trên Form và kiểm tra xử lý tương ứng |
| Hiện / ẩn mật khẩu | Kiểm tra khi chọn và bỏ chọn checkbox |
| Xác thực | Thử tài khoản đúng, sai mật khẩu và chuỗi SQL injection |
| Nhật ký đăng nhập | Đọc lại LoginAttempts và LastLoginAt |
| Điện thoại, ngày sinh | Kiểm tra mask và ngày hiển thị dd/MM/yyyy |
| Khóa học | Đọc danh sách từ CSDL và kiểm tra binding |
| Đăng ký | Lưu, đọc lại dữ liệu, kiểm tra trùng và các lựa chọn |
| Lưu bền | Đóng mở form và đọc lại từ tiến trình mới |
| SinhVien | Đối chiếu đủ 6 trường và 3 dòng dữ liệu mẫu |
| Khởi tạo lại | Kiểm tra không nhân đôi hoặc ghi đè dữ liệu đã sửa |
| Dữ liệu NULL | Kiểm tra các cột cho phép để trống trong bảng SinhVien |

Kết quả chạy được lưu tại [verification.txt](verification.txt). Số bản ghi sau kiểm tra nằm trong [database-proof.csv](database-proof.csv); dữ liệu sinh viên đối chiếu nằm trong [sinhvien-proof.csv](sinhvien-proof.csv).

Ảnh minh họa được chụp từ các form chạy kiểm tra. Nội dung gửi đến MessageBox được ghi nhận trong bộ kiểm tra; giao diện hộp thoại và chế độ thiết kế Visual Studio chưa được kiểm tra thủ công trên máy khác.
