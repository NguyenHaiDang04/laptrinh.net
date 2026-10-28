# Bài 4.1 - Login Form và CSDL

Mở [solution chung](../../BaiTap4_CSDL.sln), chọn project **Bai4_1_Login** làm Startup Project rồi nhấn F5.

- Giao diện: [FrmLogin.Designer.cs](FrmLogin.Designer.cs), [FrmLogin.resx](FrmLogin.resx).
- Xử lý: [FrmLogin.cs](FrmLogin.cs), [Program.cs](Program.cs).
- Xác thực CSDL: [AuthService.cs](../Shared/AuthService.cs).
- Hash mật khẩu: [PasswordHasher.cs](../Shared/PasswordHasher.cs).
- Tài khoản mẫu: `admin` / `Admin@123`.
- Đủ ErrorProvider, ẩn/hiện mật khẩu, Enter đăng nhập, Esc thoát.
- CSDL có tài khoản và nhật ký đăng nhập; mật khẩu lưu bằng hash + salt.

Xem [hướng dẫn cài đặt và báo cáo chung](../../README.md). Cần tải cả thư mục `BaiTap4_CSDL` để có project `Shared`.
