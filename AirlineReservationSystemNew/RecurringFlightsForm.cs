using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AirlineReservationSystem
{
    public partial class RecurringFlightsForm : Form
    {
        public RecurringFlightsForm()
        {
            InitializeComponent();
            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today.AddMonths(3);
            dtpDepartureTime.Value = DateTime.Today.AddHours(8);
            dtpArrivalTime.Value = DateTime.Today.AddHours(10);
            InitializeForm();
        }

        private void InitializeForm()
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

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            string flightNumber = txtFlightNumber.Text.Trim();
            string departureAirport = txtDepartureAirport.Text.Trim();
            string arrivalAirport = txtArrivalAirport.Text.Trim();
            TimeSpan departureTime = dtpDepartureTime.Value.TimeOfDay;
            TimeSpan duration = dtpArrivalTime.Value.TimeOfDay - departureTime;
            int totalSeats = (int)numericTotalSeats.Value;
            decimal economyPrice = numericEconomyPrice.Value;
            decimal businessPrice = numericBusinessPrice.Value;
            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date;

            bool[] selectedDays = new bool[7];
            selectedDays[0] = chkSunday.Checked;
            selectedDays[1] = chkMonday.Checked;
            selectedDays[2] = chkTuesday.Checked;
            selectedDays[3] = chkWednesday.Checked;
            selectedDays[4] = chkThursday.Checked;
            selectedDays[5] = chkFriday.Checked;
            selectedDays[6] = chkSaturday.Checked;

            int flightsAdded = 0;
            DateTime currentDate = startDate;

            while (currentDate <= endDate)
            {
                if (selectedDays[(int)currentDate.DayOfWeek])
                {
                    DateTime fullDepartureTime = currentDate.Date.Add(departureTime);
                    DateTime arrivalTime = fullDepartureTime.Add(duration);

                    string checkQuery = @"SELECT COUNT(*) FROM Flights
                                        WHERE FlightNumber = @FlightNumber
                                        AND DepartureTime = @DepartureTime";
                    SqlParameter[] checkParams = {
                        new SqlParameter("@FlightNumber", flightNumber),
                        new SqlParameter("@DepartureTime", fullDepartureTime)
                    };

                    int count = (int)DatabaseHelper.ExecuteScalar(checkQuery, checkParams);

                    if (count == 0)
                    {
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
                            new SqlParameter("@DepartureAirport", departureAirport),
                            new SqlParameter("@ArrivalAirport", arrivalAirport),
                            new SqlParameter("@DepartureTime", fullDepartureTime),
                            new SqlParameter("@ArrivalTime", arrivalTime),
                            new SqlParameter("@TotalSeats", totalSeats),
                            new SqlParameter("@AvailableSeats", totalSeats),
                            new SqlParameter("@EconomyPrice", economyPrice),
                            new SqlParameter("@BusinessPrice", businessPrice)
                        };

                        try
                        {
                            DatabaseHelper.ExecuteNonQuery(insertQuery, insertParams);
                            flightsAdded++;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error adding flight for {currentDate.ToShortDateString()}: {ex.Message}");
                        }
                    }
                }
                currentDate = currentDate.AddDays(1);
            }

            MessageBox.Show($"Successfully scheduled {flightsAdded} flights between {startDate.ToShortDateString()} and {endDate.ToShortDateString()}");
            this.Close();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(txtFlightNumber.Text))
            {
                MessageBox.Show("Please enter flight number");
                return false;
            }

            if (string.IsNullOrEmpty(txtDepartureAirport.Text))
            {
                MessageBox.Show("Please enter departure airport");
                return false;
            }

            if (string.IsNullOrEmpty(txtArrivalAirport.Text))
            {
                MessageBox.Show("Please enter arrival airport");
                return false;
            }

            if (dtpDepartureTime.Value.TimeOfDay >= dtpArrivalTime.Value.TimeOfDay)
            {
                MessageBox.Show("Arrival time must be after departure time");
                return false;
            }

            if (numericTotalSeats.Value <= 0)
            {
                MessageBox.Show("Total seats must be greater than 0");
                return false;
            }

            if (dtpStartDate.Value > dtpEndDate.Value)
            {
                MessageBox.Show("End date must be after start date");
                return false;
            }

            if (!(chkSunday.Checked || chkMonday.Checked || chkTuesday.Checked ||
                chkWednesday.Checked || chkThursday.Checked || chkFriday.Checked ||
                chkSaturday.Checked))
            {
                MessageBox.Show("Please select at least one day of the week");
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            // Empty as requested
        }

        private void btnSchedule_Click_1(object sender, EventArgs e)
        {
            // Empty as requested
        }

        private void RecurringFlightsForm_Load(object sender, EventArgs e)
        {
            // Empty as requested
        }
    }
}