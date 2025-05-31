using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AirlineReservationSystem
{
    public partial class FlightSearchForm : Form
    {
        private readonly int userId;
        private DataTable allFlights;

        public FlightSearchForm(int userId, string from = null, string to = null, DateTime? date = null)
        {
            try
            {
                InitializeComponent();
                this.userId = userId;
                InitializeControls();
                LoadAllFlights();

                if (from != null && cmbFrom.Items.Count > 0)
                    cmbFrom.SelectedValue = from;
                if (to != null && cmbTo.Items.Count > 0)
                    cmbTo.SelectedValue = to;
                if (date.HasValue)
                    dtpDate.Value = date.Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing form: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void InitializeControls()
        {
            LoadAirports();
            dtpDate.Value = DateTime.Today;
            dtpDate.MinDate = DateTime.Today;
            dtpDate.MaxDate = DateTime.Today.AddMonths(6);

            cmbSort.Items.AddRange(new string[] {
                "Departure Time (Earliest First)",
                "Departure Time (Latest First)",
                "Price (Lowest First)",
                "Price (Highest First)",
                "Duration (Shortest First)"
            });
            cmbSort.SelectedIndex = 0;
        }

        private void LoadAirports()
        {
            try
            {
                string query = "SELECT DISTINCT AirportCode FROM Airports ORDER BY AirportCode";
                DataTable airports = DatabaseHelper.ExecuteQuery(query);

                cmbFrom.DataSource = airports.Copy();
                cmbFrom.DisplayMember = "AirportCode";
                cmbFrom.ValueMember = "AirportCode";

                cmbTo.DataSource = airports.Copy();
                cmbTo.DisplayMember = "AirportCode";
                cmbTo.ValueMember = "AirportCode";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading airports: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAllFlights()
        {
            try
            {
                string query = @"SELECT
                                FlightID,
                                FlightNumber,
                                DepartureAirport,
                                ArrivalAirport,
                                FORMAT(DepartureTime, 'yyyy-MM-dd HH:mm') AS DepartureTime,
                                FORMAT(ArrivalTime, 'yyyy-MM-dd HH:mm') AS ArrivalTime,
                                AvailableSeats,
                                EconomyPrice,
                                BusinessPrice,
                                Status,
                                DATEDIFF(MINUTE, DepartureTime, ArrivalTime) AS DurationMinutes
                                FROM Flights
                                WHERE AvailableSeats > 0
                                AND Status = 'Scheduled'
                                AND DepartureTime >= GETDATE()
                                ORDER BY DepartureTime";

                allFlights = DatabaseHelper.ExecuteQuery(query);
                dataGridViewFlights.DataSource = allFlights;
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading flights: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            if (dataGridViewFlights == null || dataGridViewFlights.Columns.Count == 0)
                return;

            try
            {
                dataGridViewFlights.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewFlights.ReadOnly = true;
                dataGridViewFlights.AllowUserToAddRows = false;
                dataGridViewFlights.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewFlights.MultiSelect = false;

                if (dataGridViewFlights.Columns.Contains("FlightNumber"))
                {
                    dataGridViewFlights.Columns["FlightNumber"].HeaderText = "Flight #";
                    dataGridViewFlights.Columns["FlightNumber"].Width = 80;
                }

                if (dataGridViewFlights.Columns.Contains("DepartureAirport"))
                {
                    dataGridViewFlights.Columns["DepartureAirport"].HeaderText = "From";
                    dataGridViewFlights.Columns["DepartureAirport"].Width = 60;
                }

                string[] columnsToHide = { "FlightID", "TotalSeats", "Status" };
                foreach (string col in columnsToHide)
                {
                    if (dataGridViewFlights.Columns.Contains(col))
                        dataGridViewFlights.Columns[col].Visible = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error configuring grid: {ex.Message}");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string from = cmbFrom.SelectedValue?.ToString();
                string to = cmbTo.SelectedValue?.ToString();
                DateTime date = dtpDate.Value.Date;

                if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
                {
                    MessageBox.Show("Please select both departure and arrival airports");
                    return;
                }

                if (from == to)
                {
                    MessageBox.Show("Departure and arrival airports cannot be the same");
                    return;
                }

                string query = @"SELECT
                                FlightID,
                                FlightNumber,
                                DepartureAirport,
                                ArrivalAirport,
                                FORMAT(DepartureTime, 'yyyy-MM-dd HH:mm') AS DepartureTime,
                                FORMAT(ArrivalTime, 'yyyy-MM-dd HH:mm') AS ArrivalTime,
                                AvailableSeats,
                                EconomyPrice,
                                BusinessPrice,
                                Status,
                                DATEDIFF(MINUTE, DepartureTime, ArrivalTime) AS DurationMinutes
                                FROM Flights
                                WHERE DepartureAirport = @From
                                AND ArrivalAirport = @To
                                AND CAST(DepartureTime AS DATE) = @Date
                                AND AvailableSeats > 0
                                AND Status = 'Scheduled'";

                SqlParameter[] parameters = {
                    new SqlParameter("@From", from),
                    new SqlParameter("@To", to),
                    new SqlParameter("@Date", date)
                };

                allFlights = DatabaseHelper.ExecuteQuery(query, parameters);
                ApplySorting();

                if (allFlights.Rows.Count == 0)
                    ShowAlternativeFlights(from, to, date);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching flights: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowAlternativeFlights(string from, string to, DateTime date)
        {
            string suggestionQuery = @"SELECT TOP 3
                                    CAST(DepartureTime AS DATE) AS FlightDate,
                                    COUNT(*) AS FlightCount
                                    FROM Flights
                                    WHERE DepartureAirport = @From
                                    AND ArrivalAirport = @To
                                    AND AvailableSeats > 0
                                    AND Status = 'Scheduled'
                                    GROUP BY CAST(DepartureTime AS DATE)
                                    ORDER BY ABS(DATEDIFF(DAY, CAST(DepartureTime AS DATE), @Date))";

            SqlParameter[] parameters = {
                new SqlParameter("@From", from),
                new SqlParameter("@To", to),
                new SqlParameter("@Date", date)
            };

            DataTable suggestions = DatabaseHelper.ExecuteQuery(suggestionQuery, parameters);

            if (suggestions.Rows.Count > 0)
            {
                string message = "No flights found for selected date. Try these dates:\n";
                foreach (DataRow row in suggestions.Rows)
                {
                    message += $"- {((DateTime)row["FlightDate"]).ToString("ddd, MMM dd")} ({row["FlightCount"]} flights)\n";
                }
                MessageBox.Show(message, "No Flights Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No flights found for the selected route", "No Flights Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ApplySorting()
        {
            if (allFlights == null || allFlights.Rows.Count == 0)
                return;

            DataView dv = allFlights.DefaultView;
            switch (cmbSort.SelectedIndex)
            {
                case 0: dv.Sort = "DepartureTime ASC"; break;
                case 1: dv.Sort = "DepartureTime DESC"; break;
                case 2: dv.Sort = "EconomyPrice ASC"; break;
                case 3: dv.Sort = "EconomyPrice DESC"; break;
                case 4: dv.Sort = "DurationMinutes ASC"; break;
                default: dv.Sort = "DepartureTime DESC"; break;
            }

            dataGridViewFlights.DataSource = dv.ToTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridViewFlights.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a flight to book");
                return;
            }

            try
            {
                DataGridViewRow row = dataGridViewFlights.SelectedRows[0];
                int flightId = Convert.ToInt32(row.Cells["FlightID"].Value);
                string flightNumber = row.Cells["FlightNumber"].Value.ToString();
                string departure = row.Cells["DepartureAirport"].Value.ToString();
                string arrival = row.Cells["ArrivalAirport"].Value.ToString();
                string departureTime = row.Cells["DepartureTime"].Value.ToString();
                decimal economyPrice = Convert.ToDecimal(row.Cells["EconomyPrice"].Value);
                decimal businessPrice = Convert.ToDecimal(row.Cells["BusinessPrice"].Value);

                using (var bookingForm = new MyBookingsForm(userId, flightId, flightNumber,
                    departure, arrival, departureTime, economyPrice, businessPrice))
                {
                    if (bookingForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadAllFlights();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error booking flight: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridViewFlights_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Empty as requested
        }

        private void FlightSearchForm_Load(object sender, EventArgs e)
        {
            // Empty as requested
        }

        private void FlightSearchForm_Load_1(object sender, EventArgs e)
        {
            // Empty as requested
        }
    }
}