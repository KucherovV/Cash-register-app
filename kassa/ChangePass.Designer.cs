namespace kassa
{
    partial class ChangePass
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
            this.textBoxCurrentPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNewPasword = new System.Windows.Forms.TextBox();
            this.radioButtonNewCashierPassword = new System.Windows.Forms.RadioButton();
            this.textBoxConfirm = new System.Windows.Forms.TextBox();
            this.radioButtonNewAdminPassword = new System.Windows.Forms.RadioButton();
            this.buttonConfirmChangePassword = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxCurrentPassword
            // 
            this.textBoxCurrentPassword.Location = new System.Drawing.Point(180, 17);
            this.textBoxCurrentPassword.Name = "textBoxCurrentPassword";
            this.textBoxCurrentPassword.PasswordChar = '*';
            this.textBoxCurrentPassword.Size = new System.Drawing.Size(123, 20);
            this.textBoxCurrentPassword.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Подтвердите новый пароль";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Новый пароль";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Текущий пароль";
            // 
            // textBoxNewPasword
            // 
            this.textBoxNewPasword.Location = new System.Drawing.Point(180, 47);
            this.textBoxNewPasword.Name = "textBoxNewPasword";
            this.textBoxNewPasword.PasswordChar = '*';
            this.textBoxNewPasword.Size = new System.Drawing.Size(123, 20);
            this.textBoxNewPasword.TabIndex = 7;
            // 
            // radioButtonNewCashierPassword
            // 
            this.radioButtonNewCashierPassword.AutoSize = true;
            this.radioButtonNewCashierPassword.Location = new System.Drawing.Point(180, 111);
            this.radioButtonNewCashierPassword.Name = "radioButtonNewCashierPassword";
            this.radioButtonNewCashierPassword.Size = new System.Drawing.Size(158, 17);
            this.radioButtonNewCashierPassword.TabIndex = 1;
            this.radioButtonNewCashierPassword.TabStop = true;
            this.radioButtonNewCashierPassword.Text = "Новый пароль для касира";
            this.radioButtonNewCashierPassword.UseVisualStyleBackColor = true;
            // 
            // textBoxConfirm
            // 
            this.textBoxConfirm.Location = new System.Drawing.Point(180, 77);
            this.textBoxConfirm.Name = "textBoxConfirm";
            this.textBoxConfirm.PasswordChar = '*';
            this.textBoxConfirm.Size = new System.Drawing.Size(123, 20);
            this.textBoxConfirm.TabIndex = 8;
            // 
            // radioButtonNewAdminPassword
            // 
            this.radioButtonNewAdminPassword.AutoSize = true;
            this.radioButtonNewAdminPassword.Location = new System.Drawing.Point(180, 131);
            this.radioButtonNewAdminPassword.Name = "radioButtonNewAdminPassword";
            this.radioButtonNewAdminPassword.Size = new System.Drawing.Size(206, 17);
            this.radioButtonNewAdminPassword.TabIndex = 2;
            this.radioButtonNewAdminPassword.TabStop = true;
            this.radioButtonNewAdminPassword.Text = "Новый пароль для администратора";
            this.radioButtonNewAdminPassword.UseVisualStyleBackColor = true;
            // 
            // buttonConfirmChangePassword
            // 
            this.buttonConfirmChangePassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonConfirmChangePassword.Location = new System.Drawing.Point(15, 111);
            this.buttonConfirmChangePassword.Name = "buttonConfirmChangePassword";
            this.buttonConfirmChangePassword.Size = new System.Drawing.Size(129, 37);
            this.buttonConfirmChangePassword.TabIndex = 9;
            this.buttonConfirmChangePassword.Text = "Сменить пароль";
            this.buttonConfirmChangePassword.UseVisualStyleBackColor = true;
            this.buttonConfirmChangePassword.Click += new System.EventHandler(this.buttonConfirmChangePassword_Click);
            // 
            // ChangePass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(401, 176);
            this.Controls.Add(this.radioButtonNewCashierPassword);
            this.Controls.Add(this.radioButtonNewAdminPassword);
            this.Controls.Add(this.buttonConfirmChangePassword);
            this.Controls.Add(this.textBoxConfirm);
            this.Controls.Add(this.textBoxCurrentPassword);
            this.Controls.Add(this.textBoxNewPasword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Name = "ChangePass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Смена пароля";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChangePass_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxCurrentPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNewPasword;
        private System.Windows.Forms.RadioButton radioButtonNewCashierPassword;
        private System.Windows.Forms.TextBox textBoxConfirm;
        private System.Windows.Forms.RadioButton radioButtonNewAdminPassword;
        private System.Windows.Forms.Button buttonConfirmChangePassword;
    }
}