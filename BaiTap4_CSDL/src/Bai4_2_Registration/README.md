# Bài 4.2 - Đăng ký học viên và CSDL

Mở [solution chung](../../BaiTap4_CSDL.sln), chọn project **Bai4_2_Registration** làm Startup Project rồi nhấn F5.

- Giao diện: [FrmRegistration.Designer.cs](FrmRegistration.Designer.cs), [FrmRegistration.resx](FrmRegistration.resx).
- Xử lý: [FrmRegistration.cs](FrmRegistration.cs), [Program.cs](Program.cs).
- Đọc khóa học/lưu đăng ký: [RegistrationService.cs](../Shared/RegistrationService.cs).
- Mask điện thoại `(000) 000-0000`, DateTimePicker, ComboBox binding, RadioButton, CheckBox.
- Lưu thành công mới hiện MessageBox tổng hợp; đóng mở lại vẫn còn dữ liệu.
- Chặn trùng số điện thoại trong cùng một khóa học.

Xem [hướng dẫn cài đặt và báo cáo chung](../../README.md). Cần tải cả thư mục `BaiTap4_CSDL` để có project `Shared`.
