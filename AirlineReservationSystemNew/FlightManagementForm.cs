using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AirlineReservationSystem
{
    public partial class FlightsManagementForm : Form
    {
        public FlightsManagementForm(int userId)
        {
            InitializeComponent();
            InitializeAirportComboBoxes();
            LoadFlights();
            InitializeDateTimePickers();
        }

        private void InitializeAirportComboBoxes()
        {
            LoadAirportData();
        }

        private void LoadAirportData()
        {
            try
            {
                string query = "SELECT DISTINCT AirportCode FROM Airports ORDER BY AirportCode";
                DataTable airports = DatabaseHelper.ExecuteQuery(query);

                cmbDepartureAirport.DataSource = airports;
                cmbDepartureAirport.DisplayMember = "AirportCode";
                cmbDepartureAirport.ValueMember = "AirportCode";

                cmbArrivalAirport.DataSource = airports.Copy();
                cmbArrivalAirport.DisplayMember = "AirportCode";
                cmbArrivalAirport.ValueMember = "AirportCode";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading airport data: {ex.Message}");
            }
        }

        private void InitializeDateTimePickers()
        {
            dtpDepartureTime.Format = DateTimePickerFormat.Custom;
            dtpDepartureTime.CustomFormat = "MM/dd/yyyy HH:mm";
            dtpArrivalTime.Format = DateTimePickerFormat.Custom;
            dtpArrivalTime.CustomFormat = "MM/dd/yyyy HH:mm";
        }

        private void LoadFlights()
        {
            string query = @"SELECT
                            FlightID,
                            FlightNumber,
                            DepartureAirport,
                            ArrivalAirport,
                            CONVERT(VARCHAR, DepartureTime, 120) AS DepartureTime,
                            CONVERT(VARCHAR, ArrivalTime, 120) AS ArrivalTime,
                            TotalSeats,
                            AvailableSeats,
                            EconomyPrice,
                            BusinessPrice,
                            Status,
                            DATEDIFF(MINUTE, DepartureTime, ArrivalTime) AS DurationMinutes
                            FROM Flights
                            ORDER BY DepartureTime DESC";

            DataTable flights = DatabaseHelper.ExecuteQuery(query);
            dataGridViewFlights.DataSource = flights;
            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            dataGridViewFlights.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewFlights.ReadOnly = true;
            dataGridViewFlights.AllowUserToAddRows = false;
            dataGridViewFlights.AllowUserToDeleteRows = false;
            dataGridViewFlights.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            if (dataGridViewFlights.Columns.Contains("DurationMinutes"))
            {
                dataGridViewFlights.Columns["DurationMinutes"].HeaderText = "Duration (min)";
            }
        }

        private bool ValidateFlightInputs()
        {
            if (string.IsNullOrEmpty(txtFlightNumber.Text))
            {
                MessageBox.Show("Please enter flight number");
                return false;
            }

            if (cmbDepartureAirport.SelectedItem == null)
            {
                MessageBox.Show("Please select departure airport");
                return false;
            }

            if (cmbArrivalAirport.SelectedItem == null)
            {
                MessageBox.Show("Please select arrival airport");
                return false;
            }

            string departureCode = cmbDepartureAirport.SelectedValue.ToString();
            string arrivalCode = cmbArrivalAirport.SelectedValue.ToString();

            if (departureCode == arrivalCode)
            {
                MessageBox.Show("Departure and arrival airports cannot be the same");
                return false;
            }

            if (dtpDepartureTime.Value >= dtpArrivalTime.Value)
            {
                MessageBox.Show("Arrival time must be after departure time");
                return false;
            }

            if (numericTotalSeats.Value <= 0)
            {
                MessageBox.Show("Total seats must be greater than 0");
                return false;
            }

            return true;
        }

        private void ClearInputs()
        {
            txtFlightNumber.Text = "";
            cmbDepartureAirport.SelectedIndex = -1;
            cmbArrivalAirport.SelectedIndex = -1;
            dtpDepartureTime.Value = DateTime.Now;
            dtpArrivalTime.Value = DateTime.Now.AddHours(1);
            numericTotalSeats.Value = 100;
            numericEconomyPrice.Value = 10000;
            numericBusinessPrice.Value = 20000;
            btnAddFlight.Text = "Add Flight";
            btnAddFlight.Tag = null;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            string query = @"SELECT
                            FlightID,
                            FlightNumber,
                            DepartureAirport,
                            ArrivalAirport,
                            CONVERT(VARCHAR, DepartureTime, 120) AS DepartureTime,
                            CONVERT(VARCHAR, ArrivalTime, 120) AS ArrivalTime,
                            TotalSeats,
                            AvailableSeats,
                            EconomyPrice,
                            BusinessPrice,
                            Status
                            FROM Flights
                            WHERE FlightNumber LIKE @Search OR
                            DepartureAirport LIKE @Search OR
                            ArrivalAirport LIKE @Search
                            ORDER BY DepartureTime DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@Search", $"%{searchText}%")
            };

            DataTable flights = DatabaseHelper.ExecuteQuery(query, parameters);
            dataGridViewFlights.DataSource = flights;
        }

        private void btnEditFlight_Click(object sender, EventArgs e)
        {
            if (dataGridViewFlights.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a flight to edit");
                return;
            }

            DataGridViewRow row = dataGridViewFlights.SelectedRows[0];

            try
            {
                int flightId = Convert.ToInt32(row.Cells["FlightID"].Value);
                txtFlightNumber.Text = row.Cells["FlightNumber"].Value.ToString();

                string departureCode = row.Cells["DepartureAirport"].Value.ToString();
                string arrivalCode = row.Cells["ArrivalAirport"].Value.ToString();

                cmbDepartureAirport.SelectedValue = departureCode;
                cmbArrivalAirport.SelectedValue = arrivalCode;

                dtpDepartureTime.Value = DateTime.Parse(row.Cells["DepartureTime"].Value.ToString());
                dtpArrivalTime.Value = DateTime.Parse(row.Cells["ArrivalTime"].Value.ToString());

                int totalSeats = Convert.ToInt32(row.Cells["TotalSeats"].Value);
                numericTotalSeats.Value = Math.Min(Math.Max(totalSeats, (int)numericTotalSeats.Minimum),
                    (int)numericTotalSeats.Maximum);

                decimal economyPrice = Convert.ToDecimal(row.Cells["EconomyPrice"].Value);
                numericEconomyPrice.Value = Math.Min(Math.Max(economyPrice, numericEconomyPrice.Minimum),
                    numericEconomyPrice.Maximum);

                decimal businessPrice = Convert.ToDecimal(row.Cells["BusinessPrice"].Value);
                numericBusinessPrice.Value = Math.Min(Math.Max(businessPrice, numericBusinessPrice.Minimum),
                    numericBusinessPrice.Maximum);

                btnAddFlight.Text = "Update Flight";
                btnAddFlight.Tag = flightId;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading flight data: {ex.Message}");
            }
        }

        private void btnAddFlight_Click(object sender, EventArgs e)
        {
            // Empty as requested
        }

        private void btnAddFlight_Click_2(object sender, EventArgs e)
        {
            if (!ValidateFlightInputs())
                return;

            string flightNumber = txtFlightNumber.Text.Trim();
            string departureCode = cmbDepartureAirport.SelectedValue.ToString();
            string arrivalCode = cmbArrivalAirport.SelectedValue.ToString();
            DateTime departureTime = dtpDepartureTime.Value;
            DateTime arrivalTime = dtpArrivalTime.Value;
            int totalSeats = (int)numericTotalSeats.Value;
            decimal economyPrice = numericEconomyPrice.Value;
            decimal businessPrice = numericBusinessPrice.Value;

            string checkQuery = @"SELECT COUNT(*) FROM Flights
                                WHERE FlightNumber = @FlightNumber
                                AND DepartureTime = @DepartureTime";

            SqlParameter[] checkParams = {
                new SqlParameter("@FlightNumber", flightNumber),
                new SqlParameter("@DepartureTime", departureTime)
            };

            int count = (int)DatabaseHelper.ExecuteScalar(checkQuery, checkParams);

            if (count > 0)
            {
                MessageBox.Show("A flight with this number already exists at the specified departure time.");
                return;
            }

            string insertQuery = @"INSERT INTO Flights (
                                FlightNumber, DepartureAirport, ArrivalAirport,
                                DepartureTime, ArrivalTime, TotalSeats, AvailableSeats,
                                EconomyPrice, BusinessPrice, Status)
                                VALUES (
                                @FlightNumber, @DepartureAirport, @ArrivalAirport,
                                @DepartureTime, @ArrivalTime, @TotalSeats, @AvailableSeats,
                                @EconomyPrice, @BusinessPrice, 'Scheduled')";

            SqlParameter[] insertParams = {
                new SqlParameter("@FlightNumber", flightNumber),
                new SqlParameter("@DepartureAirport", departureCode),
                new SqlParameter("@ArrivalAirport", arrivalCode),
                new SqlParameter("@DepartureTime", departureTime),
                new SqlParameter("@ArrivalTime", arrivalTime),
                new SqlParameter("@TotalSeats", totalSeats),
                new SqlParameter("@AvailableSeats", totalSeats),
                new SqlParameter("@EconomyPrice", economyPrice),
                new SqlParameter("@BusinessPrice", businessPrice)
            };

            try
            {
                int rowsAffected = DatabaseHelper.ExecuteNonQuery(insertQuery, insertParams);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Flight added successfully!");
                    LoadFlights();
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding flight: " + ex.Message);
            }
        }

        private void btnDeleteFlight_Click_1(object sender, EventArgs e)
        {
            if (dataGridViewFlights.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a flight to delete");
                return;
            }

            int flightId = Convert.ToInt32(dataGridViewFlights.SelectedRows[0].Cells["FlightID"].Value);
            string flightNumber = dataGridViewFlights.SelectedRows[0].Cells["FlightNumber"].Value.ToString();

            DialogResult result = MessageBox.Show($"Are you sure you want to delete flight {flightNumber}?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                string deleteQuery = "DELETE FROM Flights WHERE FlightID = @FlightID";
                SqlParameter[] parameters = { new SqlParameter("@FlightID", flightId) };

                try
                {
                    int rowsAffected = DatabaseHelper.ExecuteNonQuery(deleteQuery, parameters);
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Flight deleted successfully!");
                        LoadFlights();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting flight: " + ex.Message);
                }
            }
        }

        private void FlightsManagementForm_Load(object sender, EventArgs e)
        {
            numericTotalSeats.Minimum = 1;
            numericTotalSeats.Maximum = 500;
            numericTotalSeats.Value = 150;

            numericEconomyPrice.DecimalPlaces = 2;
            numericEconomyPrice.Minimum = 0;
            numericEconomyPrice.Maximum = 100000;
            numericEconomyPrice.Value = 10000;

            numericBusinessPrice.DecimalPlaces = 2;
            numericBusinessPrice.Minimum = 0;
            numericBusinessPrice.Maximum = 100000;
            numericBusinessPrice.Value = 20000;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Empty as requested
        }

        private void btnUpdateFlight_Click(object sender, EventArgs e)
        {
            if (!ValidateFlightInputs())
                return;

            int flightId = (int)btnAddFlight.Tag;
            string flightNumber = txtFlightNumber.Text.Trim();
            string departureCode = cmbDepartureAirport.SelectedValue.ToString();
            string arrivalCode = cmbArrivalAirport.SelectedValue.ToString();
            DateTime departureTime = dtpDepartureTime.Value;
            DateTime arrivalTime = dtpArrivalTime.Value;
            int totalSeats = (int)numericTotalSeats.Value;
            decimal economyPrice = numericEconomyPrice.Value;
            decimal businessPrice = numericBusinessPrice.Value;

            string updateQuery = @"UPDATE Flights SET
                                FlightNumber = @FlightNumber,
                                DepartureAirport = @DepartureAirport,
                                ArrivalAirport = @ArrivalAirport,
                                DepartureTime = @DepartureTime,
                                ArrivalTime = @ArrivalTime,
                                TotalSeats = @TotalSeats,
                                EconomyPrice = @EconomyPrice,
                                BusinessPrice = @BusinessPrice
                                WHERE FlightID = @FlightID";

            SqlParameter[] updateParams = {
                new SqlParameter("@FlightNumber", flightNumber),
                new SqlParameter("@DepartureAirport", departureCode),
                new SqlParameter("@ArrivalAirport", arrivalCode),
                new SqlParameter("@DepartureTime", departureTime),
                new SqlParameter("@ArrivalTime", arrivalTime),
                new SqlParameter("@TotalSeats", totalSeats),
                new SqlParameter("@EconomyPrice", economyPrice),
                new SqlParameter("@BusinessPrice", businessPrice),
                new SqlParameter("@FlightID", flightId)
            };

            try
            {
                int rowsAffected = DatabaseHelper.ExecuteNonQuery(updateQuery, updateParams);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Flight updated successfully!");
                    LoadFlights();
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating flight: " + ex.Message);
            }
        }

        private void dataGridViewFlights_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Empty as requested
        }

        private void btnDeleteFlight_Click(object sender, EventArgs e)
        {
            // Empty as requested
        }
    }
}