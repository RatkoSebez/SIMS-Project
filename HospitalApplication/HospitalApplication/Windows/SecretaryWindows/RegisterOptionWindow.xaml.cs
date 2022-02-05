using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HospitalApplication.Windows.Secretary
{
    public partial class RegisterOptionWindow : Window
    {
        public RegisterOptionWindow()
        {
            InitializeComponent();
            CenterWindow();
        }

        private void CenterWindow()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void PermanentAccount_Click(object sender, RoutedEventArgs e)
        {
            RegisterPatientWindow window = new RegisterPatientWindow();
            window.Show();
            Close();
        }

        private void GuestAccount_Click(object sender, RoutedEventArgs e)
        {
            RegisterGuestPatient window = new RegisterGuestPatient();
            window.Show();
            Close();
        }
    }
}
