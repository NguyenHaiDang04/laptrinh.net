# Đánh giá hoàn thành bài 4.1 và 4.2

Ngày kiểm tra: **28/10/2026**, múi giờ Asia/Bangkok.

## Kết quả

- Solution biên dịch thành công bằng MSBuild, .NET Framework 4.8; không báo lỗi hoặc cảnh báo.
- **36/36 kiểm tra tự động đạt**, trên SQL Server LocalDB thật. Chi tiết: [verification.txt](verification.txt).
- CSDL kiểm tra sau chạy có **1 tài khoản, 3 lần đăng nhập, 3 khóa học, 2 đăng ký**; xem [database-proof.csv](database-proof.csv).
- Một tiến trình mới đã đọc lại được các đăng ký, xác nhận dữ liệu không chỉ được lưu trong biến/bộ nhớ.

## Đối chiếu yêu cầu

| Bài | Yêu cầu | Đánh giá và bằng chứng |
|---|---|---|
| 4.1 | Đủ txtUsername, txtPassword, chkShowPassword, btnLogin, btnExit, errorProvider | Đủ trong FrmLogin.Designer.cs |
| 4.1 | Enter đăng nhập, Esc thoát | Kiểm tra bằng ProcessDialogKey của Form, không chỉ kiểm tra thuộc tính |
| 4.1 | Tick hiện / bỏ tick ẩn mật khẩu | Kiểm tra UseSystemPasswordChar ở cả hai trạng thái |
| 4.1 | Không để trống hai ô bằng ErrorProvider | Kiểm tra trực tiếp GetError cho cả hai ô, bao gồm chuỗi khoảng trắng |
| 4.1 | Nâng cấp CSDL | Tài khoản đọc từ AppUsers; hash/salt; đăng nhập đúng/sai ghi LoginAttempts; LastLoginAt được cập nhật |
| 4.2 | Mask (000) 000-0000 | Đúng mask, chặn chưa đủ số, lưu 10 chữ số |
| 4.2 | Short / dd/MM/yyyy | Giữ Format.Short; ép chuỗi hiển thị Win32; kiểm tra bằng GetWindowText và ảnh trên máy vùng en-US |
| 4.2 | ComboBox DisplayMember / ValueMember | Khóa học được đọc từ Courses; hiển thị tên, lưu mã |
| 4.2 | RadioButton và CheckBox | Kiểm tra chọn duy nhất và lưu cả trạng thái chọn/bỏ chọn |
| 4.2 | MessageBox tổng hợp | Kiểm tra đủ trường trong nội dung gửi tới MessageBox, chỉ thực hiện sau INSERT thành công |
| 4.2 | Nâng cấp CSDL | INSERT Registrations, đọc lại sau đóng/mở form và từ tiến trình độc lập |

## Điều đã sửa sau kiểm tra

DateTimePicker ban đầu vẫn hiển thị tháng/ngày/năm theo vùng của Windows. Bản cuối đặt định dạng hiển thị trên control Windows, giữ thuộc tính Short và đã xác nhận hiển thị **21/09/2004** đúng yêu cầu.

## Phạm vi xác minh

Chế độ kiểm tra thay hộp thoại bằng lớp ghi nhận thông báo để tự động hóa. Ở chế độ chạy bài, thông báo dùng MessageBox thật. Ảnh được dựng từ form đang chạy; đã xem ảnh đăng nhập và đăng ký. Chưa thao tác trực tiếp trong Visual Studio Designer và chưa kiểm tra thủ công hình thức hộp thoại trên máy khác.

Đây là đánh giá phần đã triển khai và kiểm tra, không phải điểm chấm chính thức của giảng viên.
