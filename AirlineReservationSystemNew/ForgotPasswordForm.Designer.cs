using System;
using System.Windows.Media.Animation;

namespace AirlineReservationSystem
{
    partial class ForgotPasswordForm
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
        {            this.label1 = new System.Windows.Forms.Label();
            this.btnSendCode = new System.Windows.Forms.Button();
            this.btnResetPassword = new System.Windows.Forms.Label();
            this.btnBackToLogin = new System.Windows.Forms.Button();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.lblEnterEmail = new System.Windows.Forms.Label();
            this.btnVerify = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnResendCode = new System.Windows.Forms.Button();
            this.txtVerificationCode = new System.Windows.Forms.TextBox();
            this.comboBoxCountryCode = new System.Windows.Forms.ComboBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.btnUser = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter Your Whatsapp Number:";
            this.label1.Visible = false;
            // 
            // btnSendCode
            // 
            this.btnSendCode.Location = new System.Drawing.Point(30, 173);
            this.btnSendCode.Name = "btnSendCode";
            this.btnSendCode.Size = new System.Drawing.Size(84, 26);
            this.btnSendCode.TabIndex = 2;
            this.btnSendCode.Text = "Send Code";
            this.btnSendCode.UseVisualStyleBackColor = true;
            this.btnSendCode.Visible = false;
            this.btnSendCode.Click += new System.EventHandler(this.btnSendCode_Click);
            // 
            // btnResetPassword
            // 
            this.btnResetPassword.AutoSize = true;
            this.btnResetPassword.Location = new System.Drawing.Point(27, 257);
            this.btnResetPassword.Name = "btnResetPassword";
            this.btnResetPassword.Size = new System.Drawing.Size(138, 16);
            this.btnResetPassword.TabIndex = 4;
            this.btnResetPassword.Text = "Enter Verificaion code";
            this.btnResetPassword.Visible = false;
            // 
            // btnBackToLogin
            // 
            this.btnBackToLogin.Location = new System.Drawing.Point(541, 408);
            this.btnBackToLogin.Name = "btnBackToLogin";
            this.btnBackToLogin.Size = new System.Drawing.Size(110, 30);
            this.btnBackToLogin.TabIndex = 5;
            this.btnBackToLogin.Text = "Back to Login";
            this.btnBackToLogin.UseVisualStyleBackColor = true;
            this.btnBackToLogin.Click += new System.EventHandler(this.btnBackToLogin_Click);
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(171, 136);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(144, 22);
            this.txtPhoneNumber.TabIndex = 6;
            this.txtPhoneNumber.Visible = false;
            // 
            // lblEnterEmail
            // 
            this.lblEnterEmail.AutoSize = true;
            this.lblEnterEmail.Location = new System.Drawing.Point(27, 56);
            this.lblEnterEmail.Name = "lblEnterEmail";
            this.lblEnterEmail.Size = new System.Drawing.Size(70, 16);
            this.lblEnterEmail.TabIndex = 8;
            this.lblEnterEmail.Text = "Username";
            // 
            // btnVerify
            // 
            this.btnVerify.Location = new System.Drawing.Point(30, 315);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(129, 23);
            this.btnVerify.TabIndex = 9;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Visible = false;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(678, 408);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(110, 30);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnResendCode
            // 
            this.btnResendCode.Location = new System.Drawing.Point(30, 206);
            this.btnResendCode.Name = "btnResendCode";
            this.btnResendCode.Size = new System.Drawing.Size(105, 23);
            this.btnResendCode.TabIndex = 12;
            this.btnResendCode.Text = "Resend Code";
            this.btnResendCode.UseVisualStyleBackColor = true;
            this.btnResendCode.Visible = false;
            // 
            // txtVerificationCode
            // 
            this.txtVerificationCode.Location = new System.Drawing.Point(30, 287);
            this.txtVerificationCode.Name = "txtVerificationCode";
            this.txtVerificationCode.Size = new System.Drawing.Size(100, 22);
            this.txtVerificationCode.TabIndex = 13;
            this.txtVerificationCode.Visible = false;
            // 
            // comboBoxCountryCode
            // 
            this.comboBoxCountryCode.FormattingEnabled = true;
            this.comboBoxCountryCode.Items.AddRange(new object[] {
            "Pakistan",
            "USA",
            "UK",
            "UAE",
            "India"});
            this.comboBoxCountryCode.Location = new System.Drawing.Point(30, 136);
            this.comboBoxCountryCode.Name = "comboBoxCountryCode";
            this.comboBoxCountryCode.Size = new System.Drawing.Size(135, 24);
            this.comboBoxCountryCode.TabIndex = 14;
            this.comboBoxCountryCode.Visible = false;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(115, 53);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(100, 22);
            this.txtUser.TabIndex = 15;
            // 
            // btnUser
            // 
            this.btnUser.Location = new System.Drawing.Point(250, 53);
            this.btnUser.Name = "btnUser";
            this.btnUser.Size = new System.Drawing.Size(129, 23);
            this.btnUser.TabIndex = 16;
            this.btnUser.Text = "Search User";
            this.btnUser.UseVisualStyleBackColor = true;
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click);
            // 
            // ForgotPasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnVerify);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSendCode);
            this.Controls.Add(this.btnUser);
            this.Controls.Add(this.comboBoxCountryCode);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.btnResetPassword);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtVerificationCode);
            this.Controls.Add(this.lblEnterEmail);
            this.Controls.Add(this.txtPhoneNumber);
            this.Controls.Add(this.btnBackToLogin);
            this.Controls.Add(this.btnResendCode);
            this.Name = "ForgotPasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ForgotPasswordForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void BtnSendLink_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSendCode;
        private System.Windows.Forms.Label btnResetPassword;
        private System.Windows.Forms.Button btnBackToLogin;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.Label lblEnterEmail;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnResendCode;
        private System.Windows.Forms.TextBox txtVerificationCode;
        private System.Windows.Forms.ComboBox comboBoxCountryCode;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Button btnUser;
    }
}