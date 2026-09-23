Public Class Form1

    ' SỰ KIỆN FORM LOAD
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 1. Cài đặt Mask cho Số điện thoại
        mtxtPhone.Mask = "(000) 000-0000"

        ' 2. Cài đặt Format cho Ngày sinh
        dtpBirthDate.Format = DateTimePickerFormat.Custom
        dtpBirthDate.CustomFormat = "dd/MM/yyyy"

        ' 3. Nạp danh sách Khóa học cho cboCourse
        Dim courses As New List(Of Course)()
        courses.Add(New Course("K01", "Khóa học Cơ bản"))
        courses.Add(New Course("K02", "Khóa học Nâng cao"))
        courses.Add(New Course("K03", "Khóa học Chuyên sâu"))

        cboCourse.DataSource = courses
        cboCourse.DisplayMember = "Name"
        cboCourse.ValueMember = "Id"
    End Sub

    ' SỰ KIỆN CLICK NÚT ĐĂNG KÝ
    Private Sub btnDangKy_Click(sender As Object, e As EventArgs) Handles btnDangKy.Click
        ' Lấy thông tin
        Dim hoTen As String = txtName.Text
        Dim sdt As String = mtxtPhone.Text
        Dim ngaySinh As String = dtpBirthDate.Value.ToString("dd/MM/yyyy")

        ' Giới tính
        Dim gioiTinh As String = "Chưa xác định"
        If rdoNam.Checked Then
            gioiTinh = "Nam"
        ElseIf rdoNu.Checked Then
            gioiTinh = "Nữ"
        End If

        ' Khóa học
        Dim khoaHoc As String = ""
        Dim maKhoaHoc As String = ""
        If cboCourse.SelectedItem IsNot Nothing Then
            khoaHoc = cboCourse.Text
            maKhoaHoc = cboCourse.SelectedValue.ToString()
        End If

        ' Môn học
        Dim monHoc As String = ""
        If chkC.Checked Then monHoc &= "C#; "
        If chkJava.Checked Then monHoc &= "Java; "
        If chkPython.Checked Then monHoc &= "Python; "

        If monHoc.Length > 0 Then
            monHoc = monHoc.Substring(0, monHoc.Length - 2)
        Else
            monHoc = "Chưa chọn môn nào"
        End If

        ' Tổng hợp và In
        Dim thongTin As String = "THÔNG TIN ĐĂNG KÝ:" & vbCrLf &
                                 "Họ tên: " & hoTen & vbCrLf &
                                 "Số điện thoại: " & sdt & vbCrLf &
                                 "Ngày sinh: " & ngaySinh & vbCrLf &
                                 "Giới tính: " & gioiTinh & vbCrLf &
                                 "Khóa học: " & khoaHoc & " (Mã: " & maKhoaHoc & ")" & vbCrLf &
                                 "Môn học đăng ký: " & monHoc

        MessageBox.Show(thongTin, "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub lblBirthDate_Click(sender As Object, e As EventArgs) Handles lblBirthDate.Click

    End Sub

    Private Sub dtpBirthDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpBirthDate.ValueChanged

    End Sub
End Class

' =========================================================
' LƯU Ý: LỚP COURSE PHẢI ĐẶT BÊN DƯỚI END CLASS CỦA FORM1
' =========================================================
Public Class Course
    Public Property Id As String
    Public Property Name As String

    Public Sub New(id As String, name As String)
        Me.Id = id
        Me.Name = name
    End Sub
End Class