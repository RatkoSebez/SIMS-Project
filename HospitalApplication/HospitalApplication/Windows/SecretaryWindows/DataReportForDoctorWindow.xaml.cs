using Model;
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

namespace HospitalApplication.Windows.SecretaryWindows
{
    public partial class DataReportForDoctorWindow : Window
    {
        private Doctor doctor;
        public DataReportForDoctorWindow(Doctor selectedDoctor)
        {
            InitializeComponent();
            CenterWindow();
            doctor = selectedDoctor;
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

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            DoctorReportWindow window = new DoctorReportWindow(BoxDateTimeFrom.Text, BoxDateTimeTo.Text, doctor);
            window.Show();
            this.Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
