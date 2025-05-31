using AirlineReservationSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservationSystemNew
{
    public partial class UsersManagementForm: Form
    {
        public UsersManagementForm()
        {
            InitializeComponent();
            LoadUsers();
        }
        private void LoadUsers()
        {
            string query = "SELECT UserID, Username, FullName, Email, Role FROM Users";
            DataTable users = DatabaseHelper.ExecuteQuery(query);
            dataGridViewUsers.DataSource = users;
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to delete");
                return;
            }

            DataGridViewRow row = dataGridViewUsers.SelectedRows[0];
            int userId = Convert.ToInt32(row.Cells["UserID"].Value);
            string username = row.Cells["Username"].Value.ToString();

            // Prevent deleting admin accounts
            if (row.Cells["Role"].Value.ToString() == "Admin")
            {
                MessageBox.Show("Cannot delete admin accounts");
                return;
            }

            DialogResult result = MessageBox.Show($"Are you sure you want to delete user '{username}'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // First delete related bookings
                    string deleteBookingsQuery = "DELETE FROM Bookings WHERE UserID = @UserID";
                    SqlParameter[] bookingParams = { new SqlParameter("@UserID", userId) };
                    DatabaseHelper.ExecuteNonQuery(deleteBookingsQuery, bookingParams);

                    // Then delete the user
                    string deleteUserQuery = "DELETE FROM Users WHERE UserID = @UserID";
                    SqlParameter[] userParams = { new SqlParameter("@UserID", userId) };
                    int rowsAffected = DatabaseHelper.ExecuteNonQuery(deleteUserQuery, userParams);

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User deleted successfully");
                        LoadUsers();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting user: {ex.Message}");
                }
            }
        }
        private void UsersManagementForm_Load(object sender, EventArgs e)
        {

        }

      

        private void button3_Click(object sender, EventArgs e)
        {
        this.Close();

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {

        }
    }
}
