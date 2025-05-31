using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AirlineReservationSystem
{
    public partial class MyBookingsForm : Form
    {
        private int userId;
        private int flightId;
        private decimal economyPrice;
        private decimal businessPrice;

        public MyBookingsForm(int userId, int flightId, string flightNumber, string departure,
            string arrival, string departureTime, decimal economyPrice, decimal businessPrice)
        {
            InitializeComponent();
            this.userId = userId;
            this.flightId = flightId;
            this.economyPrice = economyPrice;
            this.businessPrice = businessPrice;

            lblFlightName.Text = flightNumber;
            lblDate.Text = departureTime;
            lblFrom.Text = departure;
            lblTo.Text = arrival;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string passport = txtPassport.Text.Trim();
            string seatClass = comboBoxSeatClass.SelectedItem?.ToString();
            string seatNumber = txtSeatNumber.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(passport) ||
                string.IsNullOrEmpty(seatClass) || string.IsNullOrEmpty(seatNumber))
            {
                MessageBox.Show("Please fill all required fields.");
                return;
            }

            decimal totalPrice = seatClass == "Economy" ? economyPrice : businessPrice;

            PaymentForms paymentForm = new PaymentForms(totalPrice);
            if (paymentForm.ShowDialog() == DialogResult.OK)
            {
                string query = @"INSERT INTO Bookings (UserID, FlightID, SeatClass, TotalPrice)
                                VALUES (@UserID, @FlightID, @SeatClass, @TotalPrice);
                                UPDATE Flights SET AvailableSeats = AvailableSeats - 1 WHERE FlightID = @FlightID;";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", userId),
                    new SqlParameter("@FlightID", flightId),
                    new SqlParameter("@SeatClass", seatClass),
                    new SqlParameter("@TotalPrice", totalPrice)
                };

                try
                {
                    int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Booking confirmed!");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error confirming booking: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            // Empty as requested
        }

        private void label12_Click(object sender, EventArgs e)
        {
            // Empty as requested
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            // Empty as requested
        }
    }
}