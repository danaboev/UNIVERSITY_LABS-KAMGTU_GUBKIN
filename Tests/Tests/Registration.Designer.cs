namespace Tests
{
    partial class Registration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Registration));
            this.buttonCancelReg = new System.Windows.Forms.Button();
            this.buttonSelectReg = new System.Windows.Forms.Button();
            this.textBoxSurnameReg = new System.Windows.Forms.TextBox();
            this.textBoxNameReg = new System.Windows.Forms.TextBox();
            this.textBoxLoginReg = new System.Windows.Forms.TextBox();
            this.textBoxPasswordReg = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonCancelReg
            // 
            this.buttonCancelReg.Location = new System.Drawing.Point(220, 176);
            this.buttonCancelReg.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancelReg.Name = "buttonCancelReg";
            this.buttonCancelReg.Size = new System.Drawing.Size(100, 28);
            this.buttonCancelReg.TabIndex = 0;
            this.buttonCancelReg.Text = "Отмена";
            this.buttonCancelReg.UseVisualStyleBackColor = true;
            this.buttonCancelReg.Click += new System.EventHandler(this.buttonCancelReg_Click);
            // 
            // buttonSelectReg
            // 
            this.buttonSelectReg.Location = new System.Drawing.Point(111, 176);
            this.buttonSelectReg.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSelectReg.Name = "buttonSelectReg";
            this.buttonSelectReg.Size = new System.Drawing.Size(101, 28);
            this.buttonSelectReg.TabIndex = 1;
            this.buttonSelectReg.Text = "Принять";
            this.buttonSelectReg.UseVisualStyleBackColor = true;
            this.buttonSelectReg.Click += new System.EventHandler(this.buttonSelectReg_Click);
            // 
            // textBoxSurnameReg
            // 
            this.textBoxSurnameReg.Location = new System.Drawing.Point(120, 28);
            this.textBoxSurnameReg.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxSurnameReg.Name = "textBoxSurnameReg";
            this.textBoxSurnameReg.Size = new System.Drawing.Size(200, 22);
            this.textBoxSurnameReg.TabIndex = 2;
            // 
            // textBoxNameReg
            // 
            this.textBoxNameReg.Location = new System.Drawing.Point(120, 60);
            this.textBoxNameReg.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxNameReg.Name = "textBoxNameReg";
            this.textBoxNameReg.Size = new System.Drawing.Size(200, 22);
            this.textBoxNameReg.TabIndex = 3;
            // 
            // textBoxLoginReg
            // 
            this.textBoxLoginReg.Location = new System.Drawing.Point(120, 92);
            this.textBoxLoginReg.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxLoginReg.Name = "textBoxLoginReg";
            this.textBoxLoginReg.Size = new System.Drawing.Size(200, 22);
            this.textBoxLoginReg.TabIndex = 4;
            // 
            // textBoxPasswordReg
            // 
            this.textBoxPasswordReg.Location = new System.Drawing.Point(120, 124);
            this.textBoxPasswordReg.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxPasswordReg.Name = "textBoxPasswordReg";
            this.textBoxPasswordReg.Size = new System.Drawing.Size(200, 22);
            this.textBoxPasswordReg.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Фамилия:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Имя:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 96);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Логин:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 128);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Пароль:";
            // 
            // Registration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 246);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPasswordReg);
            this.Controls.Add(this.textBoxLoginReg);
            this.Controls.Add(this.textBoxNameReg);
            this.Controls.Add(this.textBoxSurnameReg);
            this.Controls.Add(this.buttonSelectReg);
            this.Controls.Add(this.buttonCancelReg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Registration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Регистрация";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancelReg;
        private System.Windows.Forms.Button buttonSelectReg;
        private System.Windows.Forms.TextBox textBoxSurnameReg;
        private System.Windows.Forms.TextBox textBoxNameReg;
        private System.Windows.Forms.TextBox textBoxLoginReg;
        private System.Windows.Forms.TextBox textBoxPasswordReg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}