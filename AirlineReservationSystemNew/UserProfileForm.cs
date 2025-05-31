using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AirlineReservationSystem
{
    public partial class UserProfileForm : Form
    {
        private int userId;

        public UserProfileForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadUserProfile();
            SetupPasswordFields();
        }

        private void SetupPasswordFields()
        {
            txtCurrent.PasswordChar = '*';
            txtNew.PasswordChar = '*';
            txtConfirm.PasswordChar = '*';
        }

        private void LoadUserProfile()
        {
            string query = "SELECT FullName, Email, Phone FROM Users WHERE UserID = @UserID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId)
            };

            DataTable userData = DatabaseHelper.ExecuteQuery(query, parameters);

            if (userData.Rows.Count > 0)
            {
                lblName.Text = userData.Rows[0]["FullName"].ToString();
                lblEmail.Text = userData.Rows[0]["Email"].ToString();
                lblPhone.Text = userData.Rows[0]["Phone"]?.ToString() ?? "Not provided";
            }
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            string currentPassword = txtCurrent.Text;
            string newPassword = txtNew.Text;
            string confirmPassword = txtConfirm.Text;

            if (string.IsNullOrEmpty(currentPassword))
            {
                MessageBox.Show("Please enter your current password");
                return;
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Please enter a new password");
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirmation don't match");
                return;
            }

            if (!VerifyCurrentPassword(currentPassword))
            {
                MessageBox.Show("Current password is incorrect");
                return;
            }

            if (UpdatePassword(newPassword))
            {
                MessageBox.Show("Password changed successfully!");
                txtCurrent.Clear();
                txtNew.Clear();
                txtConfirm.Clear();
            }
            else
            {
                MessageBox.Show("Failed to update password");
            }
        }

        private bool VerifyCurrentPassword(string currentPassword)
        {
            string query = "SELECT COUNT(*) FROM Users WHERE UserID = @UserID AND Password = @Password";
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@Password", currentPassword)
            };

            int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, parameters));
            return count > 0;
        }

        private bool UpdatePassword(string newPassword)
        {
            string query = "UPDATE Users SET Password = @NewPassword WHERE UserID = @UserID";
            SqlParameter[] parameters =
            {
                new SqlParameter("@NewPassword", newPassword),
                new SqlParameter("@UserID", userId)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}