using System;
using System.Data;
using System.Windows.Forms;

namespace AirlineReservationSystem
{
    public partial class AdminLogsForm : Form
    {
        private int adminId;

        public AdminLogsForm(int adminId)
        {
            InitializeComponent();
            this.adminId = adminId;
            LoadLogs();
        }

        private void LoadLogs()
        {
            try
            {
                string query = @"SELECT
                                LogID,
                                Action,
                                Details,
                                CONVERT(VARCHAR, Timestamp, 120) AS Timestamp,
                                (SELECT Username FROM Users WHERE UserID = AdminID) AS AdminName
                                FROM AdminLogs
                                ORDER BY Timestamp DESC";

                DataTable logs = DatabaseHelper.ExecuteQuery(query);
                dataGridViewLogs.DataSource = logs;
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading logs: {ex.Message}");
            }
        }

        private void ConfigureDataGridView()
        {
            dataGridViewLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewLogs.ReadOnly = true;
            dataGridViewLogs.AllowUserToAddRows = false;

            if (dataGridViewLogs.Columns.Contains("LogID"))
                dataGridViewLogs.Columns["LogID"].HeaderText = "Log ID";
            if (dataGridViewLogs.Columns.Contains("AdminName"))
                dataGridViewLogs.Columns["AdminName"].HeaderText = "Admin";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Empty as requested
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}