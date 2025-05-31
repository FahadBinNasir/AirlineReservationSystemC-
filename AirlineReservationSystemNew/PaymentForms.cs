using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AirlineReservationSystem
{
    public partial class PaymentForms : Form
    {
        private decimal amount;

        public PaymentForms(decimal amount)
        {
            InitializeComponent();
            this.amount = amount;
            lblAmount.Text = amount.ToString("C");
        }

        public PaymentForms()
        {
            InitializeComponent();
        }

        private bool ValidatePayment()
        {
            string cardNumber = txtCardNumber.Text.Trim();
            string nameOnCard = txtNameOnCard.Text.Trim();
            string expiryDate = txtExpiryDate.Text.Trim();
            string cvv = txtCVV.Text.Trim();

            if (string.IsNullOrEmpty(cardNumber))
            {
                MessageBox.Show("Please enter card number", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCardNumber.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(nameOnCard))
            {
                MessageBox.Show("Please enter name on card", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNameOnCard.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(expiryDate))
            {
                MessageBox.Show("Please enter expiry date", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtExpiryDate.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cvv))
            {
                MessageBox.Show("Please enter CVV", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCVV.Focus();
                return false;
            }

            if (cardNumber.Length != 16 || !cardNumber.All(char.IsDigit))
            {
                MessageBox.Show("Please enter a valid 16-digit card number", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCardNumber.Focus();
                return false;
            }

            if (cvv.Length != 3 || !cvv.All(char.IsDigit))
            {
                MessageBox.Show("Please enter a valid 3-digit CVV", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCVV.Focus();
                return false;
            }

            if (!Regex.IsMatch(expiryDate, @"^(0[1-9]|1[0-2])\/?([0-9]{2})$"))
            {
                MessageBox.Show("Please enter expiry date in MM/YY format", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtExpiryDate.Focus();
                return false;
            }

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidatePayment())
                return;

            MessageBox.Show("Payment processed successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnPayNow_Click(object sender, EventArgs e)
        {
            // Empty as requested
        }

        private void PaymentForms_Load(object sender, EventArgs e)
        {
            // Empty as requested
        }
    }
}