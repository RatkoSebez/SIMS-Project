using HospitalApplication.Controller;
using HospitalApplication.Windows.Secretary;
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
    public partial class PatientAndAppointmentWindow : Window
    {
        private SecretaryController secretaryController = new SecretaryController();
        public PatientAndAppointmentWindow()
        {
            InitializeComponent();
            CenterWindow();
            lvUsers.ItemsSource = secretaryController.GetPatients();
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

        private void BackHome_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow window = new HomeWindow();
            this.Close();
            window.Show();
        }

        private void AddAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvUsers.SelectedIndex > -1)) { return; }
            Patient selectedPatient = (Patient)lvUsers.SelectedItem;
            AddAppointment window = new AddAppointment(selectedPatient.Username);
            window.Show();
        }

        private void Emergency_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvUsers.SelectedIndex > -1)) { return; }
            Patient selectedPatient = (Patient)lvUsers.SelectedItem;
            EmergencyWindow window = new EmergencyWindow(selectedPatient.Id);
            window.Show();
        }

        private void ViewPatientAppointments_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvUsers.SelectedIndex > -1)) { return; }
            Patient selectedPatient = (Patient)lvUsers.SelectedItem;
            MakeAppointmentWindow window = new MakeAppointmentWindow(selectedPatient.Id);
            this.Close();
            window.Show();
        }

        private void menuNew_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvUsers.SelectedIndex > -1)) { return; }
            Patient selectedPatient = (Patient)lvUsers.SelectedItem;
            AddAppointment window = new AddAppointment(selectedPatient.Username);
            window.Show();
        }

        private void menuEmergency_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvUsers.SelectedIndex > -1)) { return; }
            Patient selectedPatient = (Patient)lvUsers.SelectedItem;
            EmergencyWindow window = new EmergencyWindow(selectedPatient.Id);
            window.Show();
        }

        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void menuView_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvUsers.SelectedIndex > -1)) { return; }
            Patient selectedPatient = (Patient)lvUsers.SelectedItem;
            MakeAppointmentWindow window = new MakeAppointmentWindow(selectedPatient.Id);
            this.Close();
            window.Show();
        }

        private void PrintReport_Click(object sender, RoutedEventArgs e)
        {
            DateReportForAppointmentsWindow window = new DateReportForAppointmentsWindow();
            window.Show();
        }
    }
}
