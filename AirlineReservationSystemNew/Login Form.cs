using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AirlineReservationSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            string query = "SELECT UserID, Username, FullName, Role FROM Users WHERE Username = @Username AND Password = @Password";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", password)
            };

            DataTable result = DatabaseHelper.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                int userId = Convert.ToInt32(result.Rows[0]["UserID"]);
                string fullName = result.Rows[0]["FullName"].ToString();
                string role = result.Rows[0]["Role"].ToString();

                if (role == "Admin")
                {
                    new AdminDashboard(userId, fullName).Show();
                }
                else
                {
                    new UserDashboardForm(userId, fullName).Show();
                }
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }

        private void btnForgotPassword_Click(object sender, EventArgs e)
        {
            ForgotPasswordForm forgotForm = new ForgotPasswordForm();
            forgotForm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void showpassword_chk_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = showpassword_chk.Checked ? '\0' : '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtPassword.Text = "admin";
            txtUsername.Text = "admin";
            btnLogin.PerformClick();
        }
    }
}