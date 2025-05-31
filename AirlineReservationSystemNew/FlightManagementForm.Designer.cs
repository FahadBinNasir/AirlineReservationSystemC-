namespace AirlineReservationSystem
{
    partial class FlightsManagementForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridViewFlights = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtFlightNumber = new System.Windows.Forms.TextBox();
            this.dtpDepartureTime = new System.Windows.Forms.DateTimePicker();
            this.dtpArrivalTime = new System.Windows.Forms.DateTimePicker();
            this.numericTotalSeats = new System.Windows.Forms.NumericUpDown();
            this.numericEconomyPrice = new System.Windows.Forms.NumericUpDown();
            this.numericBusinessPrice = new System.Windows.Forms.NumericUpDown();
            this.btnAddFlight = new System.Windows.Forms.Button();
            this.btnDeleteFlight = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnEditFlight = new System.Windows.Forms.Button();
            this.cmbDepartureAirport = new System.Windows.Forms.ComboBox();
            this.cmbArrivalAirport = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFlights)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTotalSeats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEconomyPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBusinessPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(273, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fligh Management";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(690, 459);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 31);
            this.button1.TabIndex = 1;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridViewFlights
            // 
            this.dataGridViewFlights.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFlights.Location = new System.Drawing.Point(16, 212);
            this.dataGridViewFlights.Name = "dataGridViewFlights";
            this.dataGridViewFlights.RowHeadersWidth = 51;
            this.dataGridViewFlights.RowTemplate.Height = 24;
            this.dataGridViewFlights.Size = new System.Drawing.Size(830, 219);
            this.dataGridViewFlights.TabIndex = 2;
            this.dataGridViewFlights.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFlights_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(182, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 16);
            this.label2.TabIndex = 3;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(134, 36);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(100, 22);
            this.txtSearch.TabIndex = 4;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(771, 459);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 30);
            this.button2.TabIndex = 5;
            this.button2.Text = "Exit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtFlightNumber
            // 
            this.txtFlightNumber.Location = new System.Drawing.Point(420, 39);
            this.txtFlightNumber.Name = "txtFlightNumber";
            this.txtFlightNumber.Size = new System.Drawing.Size(100, 22);
            this.txtFlightNumber.TabIndex = 6;
            // 
            // dtpDepartureTime
            // 
            this.dtpDepartureTime.Location = new System.Drawing.Point(132, 124);
            this.dtpDepartureTime.Name = "dtpDepartureTime";
            this.dtpDepartureTime.Size = new System.Drawing.Size(200, 22);
            this.dtpDepartureTime.TabIndex = 9;
            // 
            // dtpArrivalTime
            // 
            this.dtpArrivalTime.Location = new System.Drawing.Point(132, 162);
            this.dtpArrivalTime.Name = "dtpArrivalTime";
            this.dtpArrivalTime.Size = new System.Drawing.Size(200, 22);
            this.dtpArrivalTime.TabIndex = 10;
            // 
            // numericTotalSeats
            // 
            this.numericTotalSeats.Location = new System.Drawing.Point(668, 39);
            this.numericTotalSeats.Name = "numericTotalSeats";
            this.numericTotalSeats.Size = new System.Drawing.Size(120, 22);
            this.numericTotalSeats.TabIndex = 11;
            // 
            // numericEconomyPrice
            // 
            this.numericEconomyPrice.Location = new System.Drawing.Point(668, 80);
            this.numericEconomyPrice.Name = "numericEconomyPrice";
            this.numericEconomyPrice.Size = new System.Drawing.Size(120, 22);
            this.numericEconomyPrice.TabIndex = 12;
            // 
            // numericBusinessPrice
            // 
            this.numericBusinessPrice.Location = new System.Drawing.Point(668, 130);
            this.numericBusinessPrice.Name = "numericBusinessPrice";
            this.numericBusinessPrice.Size = new System.Drawing.Size(120, 22);
            this.numericBusinessPrice.TabIndex = 13;
            // 
            // btnAddFlight
            // 
            this.btnAddFlight.Location = new System.Drawing.Point(354, 157);
            this.btnAddFlight.Name = "btnAddFlight";
            this.btnAddFlight.Size = new System.Drawing.Size(111, 31);
            this.btnAddFlight.TabIndex = 14;
            this.btnAddFlight.Text = "Add Flights";
            this.btnAddFlight.UseVisualStyleBackColor = true;
            this.btnAddFlight.Click += new System.EventHandler(this.btnAddFlight_Click_2);
            // 
            // btnDeleteFlight
            // 
            this.btnDeleteFlight.Location = new System.Drawing.Point(355, 121);
            this.btnDeleteFlight.Name = "btnDeleteFlight";
            this.btnDeleteFlight.Size = new System.Drawing.Size(110, 30);
            this.btnDeleteFlight.TabIndex = 15;
            this.btnDeleteFlight.Text = "Delete Flights";
            this.btnDeleteFlight.UseVisualStyleBackColor = true;
            this.btnDeleteFlight.Click += new System.EventHandler(this.btnDeleteFlight_Click_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(324, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 16);
            this.label4.TabIndex = 17;
            this.label4.Text = "Arrival Airport";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(324, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "Flight Number";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "Arrival Airport";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 16);
            this.label6.TabIndex = 19;
            this.label6.Text = "Departure Airport";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 16);
            this.label7.TabIndex = 20;
            this.label7.Text = "Arrival Time";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 130);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 16);
            this.label8.TabIndex = 21;
            this.label8.Text = "Departure Time";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(603, 168);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 16);
            this.label9.TabIndex = 22;
            this.label9.Text = "View Flights";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(564, 132);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 16);
            this.label10.TabIndex = 23;
            this.label10.Text = "Business Price";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(564, 83);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 16);
            this.label11.TabIndex = 24;
            this.label11.Text = "Economy Price";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(564, 45);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 16);
            this.label12.TabIndex = 25;
            this.label12.Text = "Total Seats";
            // 
            // btnEditFlight
            // 
            this.btnEditFlight.Location = new System.Drawing.Point(97, 458);
            this.btnEditFlight.Name = "btnEditFlight";
            this.btnEditFlight.Size = new System.Drawing.Size(101, 30);
            this.btnEditFlight.TabIndex = 26;
            this.btnEditFlight.Text = "Edit Flights";
            this.btnEditFlight.UseVisualStyleBackColor = true;
            this.btnEditFlight.Click += new System.EventHandler(this.btnEditFlight_Click);
            // 
            // cmbDepartureAirport
            // 
            this.cmbDepartureAirport.FormattingEnabled = true;
            this.cmbDepartureAirport.Location = new System.Drawing.Point(134, 80);
            this.cmbDepartureAirport.Name = "cmbDepartureAirport";
            this.cmbDepartureAirport.Size = new System.Drawing.Size(121, 24);
            this.cmbDepartureAirport.TabIndex = 27;
            // 
            // cmbArrivalAirport
            // 
            this.cmbArrivalAirport.FormattingEnabled = true;
            this.cmbArrivalAirport.Location = new System.Drawing.Point(420, 80);
            this.cmbArrivalAirport.Name = "cmbArrivalAirport";
            this.cmbArrivalAirport.Size = new System.Drawing.Size(121, 24);
            this.cmbArrivalAirport.TabIndex = 28;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(471, 157);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(126, 31);
            this.button3.TabIndex = 29;
            this.button3.Text = "Update Flights";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnUpdateFlight_Click);
            // 
            // FlightsManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 500);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.cmbArrivalAirport);
            this.Controls.Add(this.cmbDepartureAirport);
            this.Controls.Add(this.btnEditFlight);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnDeleteFlight);
            this.Controls.Add(this.btnAddFlight);
            this.Controls.Add(this.numericBusinessPrice);
            this.Controls.Add(this.numericEconomyPrice);
            this.Controls.Add(this.numericTotalSeats);
            this.Controls.Add(this.dtpArrivalTime);
            this.Controls.Add(this.dtpDepartureTime);
            this.Controls.Add(this.txtFlightNumber);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridViewFlights);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "FlightsManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FlightManagementForm";
            this.Load += new System.EventHandler(this.FlightsManagementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFlights)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTotalSeats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericEconomyPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBusinessPrice)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridViewFlights;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtFlightNumber;
        private System.Windows.Forms.DateTimePicker dtpDepartureTime;
        private System.Windows.Forms.DateTimePicker dtpArrivalTime;
        private System.Windows.Forms.NumericUpDown numericTotalSeats;
        private System.Windows.Forms.NumericUpDown numericEconomyPrice;
        private System.Windows.Forms.NumericUpDown numericBusinessPrice;
        private System.Windows.Forms.Button btnAddFlight;
        private System.Windows.Forms.Button btnDeleteFlight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnEditFlight;
        private System.Windows.Forms.ComboBox cmbDepartureAirport;
        private System.Windows.Forms.ComboBox cmbArrivalAirport;
        private System.Windows.Forms.Button button3;
    }
}