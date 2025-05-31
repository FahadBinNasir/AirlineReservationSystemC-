using AirlineReservationSystem;
using AirlineReservationSystemNew;
using System;
using System.Net;
using System.Windows.Forms;

namespace AirlineReservationSystemNew
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());

            System.Net.ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12 | SecurityProtocolType.Tls12;
        }
    }
}