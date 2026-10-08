<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        lblName = New Label()
        lblPhone = New Label()
        lblBirthDate = New Label()
        lblCourse = New Label()
        txtName = New TextBox()
        mtxtPhone = New MaskedTextBox()
        dtpBirthDate = New DateTimePicker()
        cboCourse = New ComboBox()
        rdoNam = New RadioButton()
        rdoNu = New RadioButton()
        chkC = New CheckBox()
        chkJava = New CheckBox()
        chkPython = New CheckBox()
        btnDangKy = New Button()
        SuspendLayout()
        ' 
        ' lblName
        ' 
        lblName.AutoSize = True
        lblName.Location = New Point(173, 83)
        lblName.Name = "lblName"
        lblName.Size = New Size(54, 20)
        lblName.TabIndex = 0
        lblName.Text = "Ho ten"
        ' 
        ' lblPhone
        ' 
        lblPhone.AutoSize = True
        lblPhone.Location = New Point(173, 181)
        lblPhone.Name = "lblPhone"
        lblPhone.Size = New Size(97, 20)
        lblPhone.TabIndex = 1
        lblPhone.Text = "So dien thoai"
        ' 
        ' lblBirthDate
        ' 
        lblBirthDate.AutoSize = True
        lblBirthDate.Location = New Point(173, 130)
        lblBirthDate.Name = "lblBirthDate"
        lblBirthDate.Size = New Size(74, 20)
        lblBirthDate.TabIndex = 2
        lblBirthDate.Text = "Ngay sinh"
        ' 
        ' lblCourse
        ' 
        lblCourse.AutoSize = True
        lblCourse.Location = New Point(173, 224)
        lblCourse.Name = "lblCourse"
        lblCourse.Size = New Size(71, 20)
        lblCourse.TabIndex = 3
        lblCourse.Text = "Khoa hoc"
        ' 
        ' txtName
        ' 
        txtName.Location = New Point(366, 83)
        txtName.Name = "txtName"
        txtName.Size = New Size(125, 27)
        txtName.TabIndex = 4
        ' 
        ' mtxtPhone
        ' 
        mtxtPhone.Location = New Point(366, 181)
        mtxtPhone.Mask = "(000) 000-0000"
        mtxtPhone.Name = "mtxtPhone"
        mtxtPhone.Size = New Size(125, 27)
        mtxtPhone.TabIndex = 5
        ' 
        ' dtpBirthDate
        ' 
        dtpBirthDate.Format = DateTimePickerFormat.Short
        dtpBirthDate.Location = New Point(366, 123)
        dtpBirthDate.Name = "dtpBirthDate"
        dtpBirthDate.Size = New Size(250, 27)
        dtpBirthDate.TabIndex = 6
        ' 
        ' cboCourse
        ' 
        cboCourse.FormattingEnabled = True
        cboCourse.Location = New Point(366, 297)
        cboCourse.Name = "cboCourse"
        cboCourse.Size = New Size(151, 28)
        cboCourse.TabIndex = 7
        ' 
        ' rdoNam
        ' 
        rdoNam.AutoSize = True
        rdoNam.Location = New Point(208, 302)
        rdoNam.Name = "rdoNam"
        rdoNam.Size = New Size(62, 24)
        rdoNam.TabIndex = 8
        rdoNam.TabStop = True
        rdoNam.Text = "Nam"
        rdoNam.UseVisualStyleBackColor = True
        ' 
        ' rdoNu
        ' 
        rdoNu.AutoSize = True
        rdoNu.Location = New Point(141, 297)
        rdoNu.Name = "rdoNu"
        rdoNu.Size = New Size(49, 24)
        rdoNu.TabIndex = 9
        rdoNu.TabStop = True
        rdoNu.Text = "Nu"
        rdoNu.UseVisualStyleBackColor = True
        ' 
        ' chkC
        ' 
        chkC.AutoSize = True
        chkC.Location = New Point(481, 233)
        chkC.Name = "chkC"
        chkC.Size = New Size(49, 24)
        chkC.TabIndex = 10
        chkC.Text = "#C"
        chkC.UseVisualStyleBackColor = True
        ' 
        ' chkJava
        ' 
        chkJava.AutoSize = True
        chkJava.Location = New Point(366, 233)
        chkJava.Name = "chkJava"
        chkJava.Size = New Size(58, 24)
        chkJava.TabIndex = 11
        chkJava.Text = "java"
        chkJava.UseVisualStyleBackColor = True
        ' 
        ' chkPython
        ' 
        chkPython.AutoSize = True
        chkPython.Location = New Point(597, 233)
        chkPython.Name = "chkPython"
        chkPython.Size = New Size(77, 24)
        chkPython.TabIndex = 12
        chkPython.Text = "python"
        chkPython.UseVisualStyleBackColor = True
        ' 
        ' btnDangKy
        ' 
        btnDangKy.Location = New Point(597, 297)
        btnDangKy.Name = "btnDangKy"
        btnDangKy.Size = New Size(94, 29)
        btnDangKy.TabIndex = 14
        btnDangKy.Text = "Dang ki"
        btnDangKy.UseVisualStyleBackColor = True
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(821, 450)
        Controls.Add(btnDangKy)
        Controls.Add(chkPython)
        Controls.Add(chkJava)
        Controls.Add(chkC)
        Controls.Add(rdoNu)
        Controls.Add(rdoNam)
        Controls.Add(cboCourse)
        Controls.Add(dtpBirthDate)
        Controls.Add(mtxtPhone)
        Controls.Add(txtName)
        Controls.Add(lblCourse)
        Controls.Add(lblBirthDate)
        Controls.Add(lblPhone)
        Controls.Add(lblName)
        Name = "Form1"
        Text = "Form1"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblName As Label
    Friend WithEvents lblPhone As Label
    Friend WithEvents lblBirthDate As Label
    Friend WithEvents lblCourse As Label
    Friend WithEvents txtName As TextBox
    Friend WithEvents mtxtPhone As MaskedTextBox
    Friend WithEvents dtpBirthDate As DateTimePicker
    Friend WithEvents cboCourse As ComboBox
    Friend WithEvents rdoNam As RadioButton
    Friend WithEvents rdoNu As RadioButton
    Friend WithEvents chkC As CheckBox
    Friend WithEvents chkJava As CheckBox
    Friend WithEvents chkPython As CheckBox
    Friend WithEvents btnDangKy As Button

End Class
