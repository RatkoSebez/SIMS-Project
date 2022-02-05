using HospitalApplication.Controller;
using Logic;
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
using WorkWithFiles;

namespace HospitalApplication.Windows.Secretary
{
    public partial class FunMoveAppointmentWindow : Window
    {
        private MoveAppointment moveAppointmentWindow= MoveAppointment.Instance;
        private AppointmentController appointmentController = new AppointmentController();

        public FunMoveAppointmentWindow()
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

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            Appointment examination = (Appointment)moveAppointmentWindow.lvUsers.SelectedItem;
            DateTime oldDate = examination.ExaminationStart;
            DateTime newDate = GetDateAndTimeFromForm(Date.SelectedDate.Value.Date, Combo);
            if (!IsNewDateValid(oldDate, newDate)) return;
            appointmentController.MoveAppointment(examination, newDate);
            Close();
        }

        private bool IsNewDateValid(DateTime oldDate, DateTime newDate)
        {
            if (oldDate == newDate) MessageBox.Show("Your examination is already scheduled at this time.", "Info", MessageBoxButton.OK);
            else return true;
            return false;
        }

        private DateTime GetDateAndTimeFromForm(DateTime date, ComboBox Combo)
        {
            List<(int, int, int)> times = new List<(int, int, int)>();
            for (int i = 0; i < 13; i++){
                times.Add((7 + i, 0, 0));
                times.Add((7 + i, 30, 0));
            }
            (int, int, int) time = times[Combo.SelectedIndex];
            return date + new TimeSpan(time.Item1, time.Item2, time.Item3);
        }
    }
}
