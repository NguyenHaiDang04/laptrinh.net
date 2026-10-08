namespace WinFormsApp03
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn1 = new Button();
            btn2 = new Button();
            btn3 = new Button();
            btn4 = new Button();
            btn5 = new Button();
            btn6 = new Button();
            btn7 = new Button();
            btn8 = new Button();
            btn9 = new Button();
            btn0 = new Button();
            txtDisplay = new TextBox();
            btnAdd = new Button();
            btnSub = new Button();
            btnMul = new Button();
            btnDiv = new Button();
            btnEquals = new Button();
            btnClear = new Button();
            SuspendLayout();
            // 
            // btn1
            // 
            btn1.Location = new Point(180, 54);
            btn1.Name = "btn1";
            btn1.Size = new Size(54, 55);
            btn1.TabIndex = 0;
            btn1.Text = "1";
            btn1.UseVisualStyleBackColor = true;
            btn1.Click += button1_Click;
            // 
            // btn2
            // 
            btn2.Location = new Point(240, 54);
            btn2.Name = "btn2";
            btn2.Size = new Size(53, 58);
            btn2.TabIndex = 1;
            btn2.Text = "2";
            btn2.UseVisualStyleBackColor = true;
            btn2.Click += button2_Click;
            // 
            // btn3
            // 
            btn3.Location = new Point(299, 54);
            btn3.Name = "btn3";
            btn3.Size = new Size(55, 58);
            btn3.TabIndex = 2;
            btn3.Text = "3";
            btn3.UseVisualStyleBackColor = true;
            btn3.Click += button3_Click;
            // 
            // btn4
            // 
            btn4.Location = new Point(180, 115);
            btn4.Name = "btn4";
            btn4.Size = new Size(54, 58);
            btn4.TabIndex = 3;
            btn4.Text = "4";
            btn4.UseVisualStyleBackColor = true;
            btn4.Click += button4_Click;
            // 
            // btn5
            // 
            btn5.Location = new Point(240, 115);
            btn5.Name = "btn5";
            btn5.Size = new Size(53, 58);
            btn5.TabIndex = 4;
            btn5.Text = "5";
            btn5.UseVisualStyleBackColor = true;
            // 
            // btn6
            // 
            btn6.Location = new Point(299, 118);
            btn6.Name = "btn6";
            btn6.Size = new Size(55, 55);
            btn6.TabIndex = 5;
            btn6.Text = "6";
            btn6.UseVisualStyleBackColor = true;
            // 
            // btn7
            // 
            btn7.Location = new Point(180, 179);
            btn7.Name = "btn7";
            btn7.Size = new Size(54, 54);
            btn7.TabIndex = 6;
            btn7.Text = "7";
            btn7.UseVisualStyleBackColor = true;
            // 
            // btn8
            // 
            btn8.Location = new Point(240, 179);
            btn8.Name = "btn8";
            btn8.Size = new Size(53, 54);
            btn8.TabIndex = 7;
            btn8.Text = "8";
            btn8.UseVisualStyleBackColor = true;
            btn8.Click += button8_Click;
            // 
            // btn9
            // 
            btn9.Location = new Point(299, 179);
            btn9.Name = "btn9";
            btn9.Size = new Size(55, 54);
            btn9.TabIndex = 8;
            btn9.Text = "9";
            btn9.UseVisualStyleBackColor = true;
            // 
            // btn0
            // 
            btn0.Location = new Point(240, 239);
            btn0.Name = "btn0";
            btn0.Size = new Size(53, 55);
            btn0.TabIndex = 9;
            btn0.Text = "0";
            btn0.UseVisualStyleBackColor = true;
            // 
            // txtDisplay
            // 
            txtDisplay.Location = new Point(180, 12);
            txtDisplay.Name = "txtDisplay";
            txtDisplay.Size = new Size(242, 27);
            txtDisplay.TabIndex = 10;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(381, 54);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(41, 29);
            btnAdd.TabIndex = 11;
            btnAdd.Text = "+";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnSub
            // 
            btnSub.Location = new Point(381, 103);
            btnSub.Name = "btnSub";
            btnSub.Size = new Size(41, 29);
            btnSub.TabIndex = 12;
            btnSub.Text = "-";
            btnSub.UseVisualStyleBackColor = true;
            // 
            // btnMul
            // 
            btnMul.Location = new Point(381, 154);
            btnMul.Name = "btnMul";
            btnMul.Size = new Size(41, 29);
            btnMul.TabIndex = 13;
            btnMul.Text = "*";
            btnMul.UseVisualStyleBackColor = true;
            btnMul.Click += button3_Click_1;
            // 
            // btnDiv
            // 
            btnDiv.Location = new Point(381, 204);
            btnDiv.Name = "btnDiv";
            btnDiv.Size = new Size(41, 29);
            btnDiv.TabIndex = 14;
            btnDiv.Text = "/";
            btnDiv.UseVisualStyleBackColor = true;
            btnDiv.Click += button4_Click;
            // 
            // btnEquals
            // 
            btnEquals.Location = new Point(180, 307);
            btnEquals.Name = "btnEquals";
            btnEquals.Size = new Size(113, 29);
            btnEquals.TabIndex = 17;
            btnEquals.Text = "=";
            btnEquals.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(299, 307);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(123, 29);
            btnClear.TabIndex = 18;
            btnClear.Text = "C";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnClear);
            Controls.Add(btnEquals);
            Controls.Add(btnDiv);
            Controls.Add(btnMul);
            Controls.Add(btnSub);
            Controls.Add(btnAdd);
            Controls.Add(txtDisplay);
            Controls.Add(btn0);
            Controls.Add(btn9);
            Controls.Add(btn8);
            Controls.Add(btn7);
            Controls.Add(btn6);
            Controls.Add(btn5);
            Controls.Add(btn4);
            Controls.Add(btn3);
            Controls.Add(btn2);
            Controls.Add(btn1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void button0_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        private Button btn1;
        private Button btn2;
        private Button btn3;
        private Button btn4;
        private Button btn5;
        private Button btn6;
        private Button btn7;
        private Button btn8;
        private Button btn9;
        private Button btn0;
        private TextBox txtDisplay;
        private Button btnAdd;
        private Button btnSub;
        private Button btnMul;
        private Button btnDiv;
        private Button btnEquals;
        private Button btnClear;
    }
}
