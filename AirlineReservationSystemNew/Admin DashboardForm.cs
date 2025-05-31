using AirlineReservationSystemNew;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AirlineReservationSystem
{
    public partial class AdminDashboard : Form
    {
        private int userId;
        private string fullName;

        public AdminDashboard(int userId, string fullName)
        {
            InitializeComponent();
            this.userId = userId;
            this.fullName = fullName;
            lblWelcome.Text = $"Welcome, {fullName} (Admin)";
        }

        private void LogAction(string action, string details)
        {
            try
            {
                string query = @"INSERT INTO AdminLogs (Action, Details, AdminID, Timestamp)
                                VALUES (@Action, @Details, @AdminID, GETDATE())";

                SqlParameter[] parameters = {
                    new SqlParameter("@Action", action),
                    new SqlParameter("@Details", details),
                    new SqlParameter("@AdminID", userId)
                };

                DatabaseHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging action: {ex.Message}");
            }
        }

        private void btnFlights_Click(object sender, EventArgs e)
        {
            FlightsManagementForm flightsForm = new FlightsManagementForm(userId);
            flightsForm.Show();
            LogAction("Navigation", "Accessed Flights Management");
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            UsersManagementForm usersForm = new UsersManagementForm();
            usersForm.Show();
            LogAction("Navigation", "Accessed Users Management");
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            ReportsForm reportsForm = new ReportsForm();
            reportsForm.Show();
            LogAction("Navigation", "Accessed Reports");
        }

        private void btnScheduleRecurring_Click(object sender, EventArgs e)
        {
            RecurringFlightsForm recurringForm = new RecurringFlightsForm();
            recurringForm.Show();
            LogAction("Navigation", "Accessed Recurring Flights");
        }

        private void btnLogs_Click(object sender, EventArgs e)
        {
            AdminLogsForm logsForm = new AdminLogsForm(userId);
            logsForm.Show();
            LogAction("Navigation", "Viewed Admin Logs");
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LogAction("Logout", "Admin logged out");
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}