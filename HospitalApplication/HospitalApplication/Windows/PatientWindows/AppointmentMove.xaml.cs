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
using HospitalApplication.Controller;
using HospitalApplication.Service;
using HospitalApplication.Windows.PatientWindows;
using Logic;
using Model;
using WorkWithFiles;

namespace HospitalApplication.Windows.Patient1
{
    public partial class WindowExaminationMove : Window
    {
        private PatientsPage patientsPage = PatientsPage.Instance;
        private AppointmentController controller = new AppointmentController();
        private FormService formService = new FormService();

        public WindowExaminationMove()
        {
            InitializeComponent();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            Appointment appointment = (Appointment)patientsPage.lvUsers.SelectedItem;
            DateTime oldDate = appointment.ExaminationStart;
            DateTime newDate = formService.GetDateAndTimeFromForm(Date.SelectedDate.Value.Date, Combo, 7, 13);
            if (!IsNewDateValid(oldDate, newDate)) return;
            controller.MoveAppointment(appointment, newDate);
            patientsPage.UpdateView();
            Close();
        }

        private bool IsNewDateValid(DateTime oldDate, DateTime newDate)
        {
            if (oldDate == newDate) MessageBox.Show("Your examination is already scheduled at this time.", "Info", MessageBoxButton.OK);
            else if ((oldDate - DateTime.Now).TotalDays < 1) MessageBox.Show("You can not move examination that starts in less than 24h.", "Info", MessageBoxButton.OK);
            else if (Math.Abs((oldDate - newDate).TotalDays) > 2) MessageBox.Show("You can not move examination start more than 2 days.", "Info", MessageBoxButton.OK);
            else return true;
            return false;
        }
    }
}
