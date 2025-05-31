using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        private void btnPayNow_Click(object sender, EventArgs e)
        {
            string cardNumber = txtCardNumber.Text.Trim();
            string nameOnCard = txtNameOnCard.Text.Trim();
            string expiryDate = txtExpiryDate.Text.Trim();
            string cvv = txtCVV.Text.Trim();

            if (string.IsNullOrEmpty(cardNumber) || string.IsNullOrEmpty(nameOnCard) ||
               string.IsNullOrEmpty(expiryDate) || string.IsNullOrEmpty(cvv))
            {
                MessageBox.Show("Please fill all payment details.");
                return;
            }

            // Simple validation (in real app, use proper validation)
            if (cardNumber.Length != 16 || !cardNumber.All(char.IsDigit))
            {
                MessageBox.Show("Please enter a valid 16-digit card number.");
                return;
            }

            if (cvv.Length != 3 || !cvv.All(char.IsDigit))
            {
                MessageBox.Show("Please enter a valid 3-digit CVV.");
                return;
            }

            // Process payment (in real app, integrate with payment gateway)
            MessageBox.Show("Payment processed successfully!");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Your payment logic here
        }
        private void PaymentForm_Load(object sender, EventArgs e)
        {

        }
    }
}