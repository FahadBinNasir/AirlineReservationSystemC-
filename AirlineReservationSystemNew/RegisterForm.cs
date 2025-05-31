using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AirlineReservationSystem
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            cmbcountry.Items.AddRange(new string[] {
                "Pakistan (+92)",
                "USA (+1)",
                "UK (+44)",
                "UAE (+971)",
                "India (+91)"
            });
            cmbcountry.SelectedIndex = 0;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill all required fields.");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username OR Email = @Email";
            SqlParameter[] checkParams = new SqlParameter[]
            {
                new SqlParameter("@Username", username),
                new SqlParameter("@Email", email)
            };

            int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(checkQuery, checkParams));

            if (count > 0)
            {
                MessageBox.Show("Username or email already exists.");
                return;
            }

            string insertQuery = @"INSERT INTO Users (Username, Password, FullName, Email, Phone, Role)
                                VALUES (@Username, @Password, @FullName, @Email, @Phone, 'Customer')";
            SqlParameter[] insertParams = new SqlParameter[]
            {
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", password),
                new SqlParameter("@FullName", fullName),
                new SqlParameter("@Email", email),
                new SqlParameter("@Phone", string.IsNullOrEmpty(phone) ? DBNull.Value : (object)phone)
            };

            try
            {
                int rowsAffected = DatabaseHelper.ExecuteNonQuery(insertQuery, insertParams);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Registration successful! Please login.");
                    LoginForm loginForm = new LoginForm();
                    loginForm.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during registration: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            new LoginForm().Show();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            // Empty as requested
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Empty as requested
        }
    }
}