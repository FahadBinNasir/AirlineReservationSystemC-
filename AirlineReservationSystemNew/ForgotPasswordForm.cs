using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.Configuration;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AirlineReservationSystem
{
    public partial class ForgotPasswordForm : Form
    {
        private string _lastSentPhone;
        private string _resetCode;
        private bool _twilioInitialized = false;
        private string _currentUsername;
        private Dictionary<string, string> _countryCodes = new Dictionary<string, string>()
        {
            {"Pakistan (+92)", "+92"},
            {"USA (+1)", "+1"},
            {"UK (+44)", "+44"},
            {"UAE (+971)", "+971"},
            {"India (+91)", "+91"}
        };

        public ForgotPasswordForm()
        {
            InitializeComponent();
            InitializeTwilioClient();
            InitializeCountryCodeComboBox();
            SetupPhoneTextBoxPlaceholder();
        }

        private void InitializeCountryCodeComboBox()
        {
            comboBoxCountryCode.DataSource = new BindingSource(_countryCodes, null);
            comboBoxCountryCode.DisplayMember = "Key";
            comboBoxCountryCode.ValueMember = "Value";
            comboBoxCountryCode.SelectedIndex = 0;
        }

        private void InitializeTwilioClient()
        {
            try
            {
                string accountSid = ConfigurationManager.AppSettings["TwilioAccountSid"];
                string authToken = ConfigurationManager.AppSettings["TwilioAuthToken"];

                if (!string.IsNullOrEmpty(accountSid) && !string.IsNullOrEmpty(authToken))
                {
                    TwilioClient.Init(accountSid, authToken);
                    _twilioInitialized = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Twilio initialization failed: {ex.Message}");
            }
        }

        private void SetupPhoneTextBoxPlaceholder()
        {
            txtPhoneNumber.Text = "Phone Number";
            txtPhoneNumber.ForeColor = SystemColors.GrayText;
            txtPhoneNumber.GotFocus += RemovePlaceholder;
            txtPhoneNumber.LostFocus += SetPlaceholder;
        }

        private void RemovePlaceholder(object sender, EventArgs e)
        {
            if (txtPhoneNumber.Text == "Phone Number")
            {
                txtPhoneNumber.Text = "";
                txtPhoneNumber.ForeColor = SystemColors.WindowText;
            }
        }

        private void SetPlaceholder(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                txtPhoneNumber.Text = "Phone Number";
                txtPhoneNumber.ForeColor = SystemColors.GrayText;
            }
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter a username");
                return;
            }

            if (IsUserRegistered(username))
            {
                _currentUsername = username;
                txtUser.Enabled = false;
                btnUser.Enabled = false;
                MessageBox.Show("User found. Please enter the phone number for this account.");

                btnResendCode.Visible = true;
                txtUser.Visible = true;
                txtPhoneNumber.Visible = true;
                btnSendCode.Visible = true;
                btnVerify.Visible = true;
                label1.Visible = true;
                comboBoxCountryCode.Visible = true;
                txtPhoneNumber.Visible = true;
                btnResetPassword.Visible = true;
                txtVerificationCode.Visible = true;
            }
            else
            {
                MessageBox.Show("Username not found in our system");
            }
        }

        private bool IsUserRegistered(string username)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Users WHERE LOWER(Username) = LOWER(@Username)";
                SqlParameter[] parameters = { new SqlParameter("@Username", username) };
                object result = DatabaseHelper.ExecuteScalar(query, parameters);
                return Convert.ToInt32(result) > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}");
                return false;
            }
        }

        private void btnSendCode_Click(object sender, EventArgs e)
        {
            try
            {
                string phoneNumber = txtPhoneNumber.Text.Trim();
                string countryCode = ((KeyValuePair<string, string>)comboBoxCountryCode.SelectedItem).Value;

                if (string.IsNullOrEmpty(phoneNumber) || phoneNumber == "Phone Number")
                {
                    MessageBox.Show("Please enter your phone number");
                    return;
                }

                if (!IsValidPhoneNumber(phoneNumber))
                {
                    MessageBox.Show("Please enter a valid phone number (digits only)");
                    return;
                }

                string fullPhoneNumber = countryCode + phoneNumber;

                if (!IsPhoneRegisteredForUser(_currentUsername, fullPhoneNumber))
                {
                    MessageBox.Show("This phone number doesn't match the user account");
                    return;
                }

                SendVerificationCodeAsync(fullPhoneNumber);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^\d+$");
        }

        private bool IsPhoneRegisteredForUser(string username, string phone)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Users WHERE LOWER(Username) = LOWER(@Username) AND Phone = @Phone";
                SqlParameter[] parameters = {
                    new SqlParameter("@Username", username),
                    new SqlParameter("@Phone", phone)
                };

                object result = DatabaseHelper.ExecuteScalar(query, parameters);
                return Convert.ToInt32(result) > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}");
                return false;
            }
        }

        private async void SendVerificationCodeAsync(string phone)
        {
            if (!_twilioInitialized)
            {
                MessageBox.Show("WhatsApp service unavailable. Please try again later.");
                return;
            }

            try
            {
                _resetCode = new Random().Next(100000, 999999).ToString();
                _lastSentPhone = phone;

                var message = await MessageResource.CreateAsync(
                    body: $"Your verification code: {_resetCode}",
                    from: new PhoneNumber("whatsapp:+14155238886"),
                    to: new PhoneNumber($"whatsapp:{phone}")
                );

                MessageBox.Show($"Verification code sent to {phone}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send code: {ex.Message}\n\nPlease check the phone number format and try again.");
            }
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (txtVerificationCode.Text == _resetCode)
            {
                string tempPassword = GenerateTemporaryPassword();
                SendTemporaryPassword(_lastSentPhone, tempPassword);
                UpdateUserPassword(_currentUsername, tempPassword);

                MessageBox.Show($"Password reset successful! Your temporary password is: {tempPassword}\nPlease change it after login.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
                new LoginForm().Show();
            }
            else
            {
                MessageBox.Show("Invalid verification code. Please try again.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerateTemporaryPassword()
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghjkmnpqrstuvwxyz23456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void SendTemporaryPassword(string phone, string tempPassword)
        {
            try
            {
                var message = MessageResource.Create(
                    body: $"Your temporary password is: {tempPassword}\nPlease change it after login.",
                    from: new PhoneNumber("whatsapp:+14155238886"),
                    to: new PhoneNumber($"whatsapp:{phone}")
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Password was reset but couldn't send WhatsApp: {ex.Message}\n\nYour temporary password is: {tempPassword}",
                    "Partial Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateUserPassword(string username, string newPassword)
        {
            try
            {
                string query = "UPDATE Users SET Password = @Password WHERE LOWER(Username) = LOWER(@Username)";
                SqlParameter[] parameters = {
                    new SqlParameter("@Password", newPassword),
                    new SqlParameter("@Username", username)
                };

                int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);

                if (rowsAffected == 0)
                {
                    throw new Exception("Failed to update password");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating password: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            this.Close();
            new LoginForm().Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}