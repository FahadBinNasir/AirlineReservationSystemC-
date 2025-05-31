using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AirlineReservationSystemNew
{
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            if (cmbReportType.SelectedItem == null)
            {
                MessageBox.Show("Please select a report type");
                return;
            }

            string reportType = cmbReportType.SelectedItem.ToString();
            DateTime startDate = dtpStartDate.Value;
            DateTime endDate = dtpEndDate.Value;

            try
            {
                DataTable reportData = null;
                string reportTitle = "";

                switch (reportType)
                {
                    case "Flight Occupancy":
                        reportData = GetFlightOccupancyReport(startDate, endDate);
                        reportTitle = "Flight Occupancy Report";
                        break;
                    case "Revenue":
                        reportData = GetRevenueReport(startDate, endDate);
                        reportTitle = "Revenue Report";
                        break;
                    case "User Activity":
                        reportData = GetUserActivityReport(startDate, endDate);
                        reportTitle = "User Activity Report";
                        break;
                }

                if (reportData != null)
                {
                    dataGridViewReport.DataSource = reportData;
                    lblReportTitle.Text = $"{reportTitle} ({startDate.ToShortDateString()} to {endDate.ToShortDateString()})";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}");
            }
        }

        private DataTable GetFlightOccupancyReport(DateTime startDate, DateTime endDate)
        {
            string query = @"SELECT
                            f.FlightNumber,
                            f.DepartureAirport,
                            f.ArrivalAirport,
                            CONVERT(VARCHAR, f.DepartureTime, 120) AS DepartureTime,
                            f.TotalSeats,
                            f.AvailableSeats,
                            (f.TotalSeats - f.AvailableSeats) AS BookedSeats,
                            CONVERT(DECIMAL(5,2),
                            ((f.TotalSeats - f.AvailableSeats) * 100.0 / f.TotalSeats)) AS OccupancyRate
                            FROM Flights f
                            WHERE f.DepartureTime BETWEEN @StartDate AND @EndDate
                            ORDER BY f.DepartureTime";

            SqlParameter[] parameters = {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate.AddDays(1))
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        private DataTable GetRevenueReport(DateTime startDate, DateTime endDate)
        {
            string query = @"SELECT
                            b.BookingID,
                            f.FlightNumber,
                            u.Username,
                            b.SeatClass,
                            b.TotalPrice,
                            CONVERT(VARCHAR, b.BookingDate, 120) AS BookingDate
                            FROM Bookings b
                            JOIN Flights f ON b.FlightID = f.FlightID
                            JOIN Users u ON b.UserID = u.UserID
                            WHERE b.BookingDate BETWEEN @StartDate AND @EndDate
                            ORDER BY b.BookingDate";

            SqlParameter[] parameters = {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate.AddDays(1))
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        private DataTable GetUserActivityReport(DateTime startDate, DateTime endDate)
        {
            string query = @"SELECT
                            u.UserID,
                            u.Username,
                            u.FullName,
                            COUNT(b.BookingID) AS TotalBookings,
                            SUM(b.TotalPrice) AS TotalSpent
                            FROM Users u
                            LEFT JOIN Bookings b ON u.UserID = b.UserID
                            AND b.BookingDate BETWEEN @StartDate AND @EndDate
                            GROUP BY u.UserID, u.Username, u.FullName
                            ORDER BY TotalBookings DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate.AddDays(1))
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        private void btnExportReport_Click(object sender, EventArgs e)
        {
            if (dataGridViewReport.DataSource == null)
            {
                MessageBox.Show("No report data to export");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV file (*.csv)|*.csv";
            saveFileDialog.Title = "Export Report";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataTable dt = (DataTable)dataGridViewReport.DataSource;
                    StringBuilder sb = new StringBuilder();

                    foreach (DataColumn column in dt.Columns)
                    {
                        sb.Append(column.ColumnName + ",");
                    }
                    sb.AppendLine();

                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (var item in row.ItemArray)
                        {
                            sb.Append(item.ToString() + ",");
                        }
                        sb.AppendLine();
                    }

                    File.WriteAllText(saveFileDialog.FileName, sb.ToString());
                    MessageBox.Show("Report exported successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting report: {ex.Message}");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {

        }


    }
}