using HospitalApplication.Controller;
using HospitalApplication.WorkWithFiles;
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
    public partial class AddAppointment : Window
    {
        private AppointmentService examinationService = new AppointmentService();
        private AppointmentController patientController = new AppointmentController();
        private FileDoctors fileDoctors = FileDoctors.Instance;
        private List<Doctor> doctors = new List<Doctor>();
        private List<Appointment> appointments = new List<Appointment>();
        private int idAppointment = 100000;
        private string patientUsername;

        public AddAppointment(string username)
        {
            InitializeComponent();
            CenterWindow();
            patientUsername = username;
            InsertDoctorsInComboBox();
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            appointments = examinationService.appointments;
        }

        private void InsertDoctorsInComboBox()
        {
            doctors = fileDoctors.GetDoctors();
            for (int i = 0; i < doctors.Count; i++){
                Combo3.Items.Add(doctors[i].Username.ToString());
            }
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
            Appointment newAppointment = new Appointment(patientUsername, doctors[Combo3.SelectedIndex].Username, "0", GetSelectedDateAndTime(), GenerateIdForAppointment(),
                                                        (ExaminationType)Enum.Parse(typeof(ExaminationType), ComboTypeApppointment.Text), Int32.Parse(textBox111.Text));

            patientController.ScheduleAppointment(newAppointment);
            Close();
        }

        private string GenerateIdForAppointment()
        {
            if (idAppointment == 100000)
                for (int i = 0; i < appointments.Count; i++)
                    idAppointment = Math.Max(idAppointment, Int32.Parse(appointments[i].ExaminationId));
            return (idAppointment + 1).ToString();
        }

        private DateTime GetSelectedDateAndTime()
        {
            List<(int, int, int)> appointment = new List<(int, int, int)>();
            for (int i = 0; i < 13; i++){
                appointment.Add((7 + i, 0, 0));
                appointment.Add((7 + i, 30, 0));
            }
            (int, int, int) a = appointment[Combo.SelectedIndex];
            return Date.SelectedDate.Value.Date + new TimeSpan(a.Item1, a.Item2, a.Item3);
        }
    }
}
