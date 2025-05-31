using AirlineReservationSystemNew;
using System;
using System.Windows.Forms;

namespace AirlineReservationSystem
{
    public partial class UserDashboardForm : Form
    {
        private int userId;
        private string fullName;

        public UserDashboardForm(int userId, string fullName)
        {
            InitializeComponent();
            this.userId = userId;
            this.fullName = fullName;
            lblWelcome.Text = $"Welcome, {fullName}";
        }

        private void btnMyProfile_Click(object sender, EventArgs e)
        {
            UserProfileForm profileForm = new UserProfileForm(userId);
            profileForm.Show();
        }

        private void btnSearchFlights_Click(object sender, EventArgs e)
        {
            try
            {
                FlightSearchForm searchForm = new FlightSearchForm(userId);
                searchForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening flight search: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}