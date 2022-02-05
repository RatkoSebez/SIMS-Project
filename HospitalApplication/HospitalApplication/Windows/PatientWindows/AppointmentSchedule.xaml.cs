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
using HospitalApplication.Repository;
using HospitalApplication.Service;
using HospitalApplication.Windows.PatientWindows;
using HospitalApplication.WorkWithFiles;
using Logic;
using Model;
using WorkWithFiles;

namespace HospitalApplication
{
    public partial class WindowExaminationSchedule : Window
    {
        private AppointmentController controller = new AppointmentController();
        private PatientsPage pagePatients = PatientsPage.Instance;
        private int appointmentsId = 100000;
        private MainWindow mainWindow = MainWindow.Instance;
        private List<Doctor> doctors = new List<Doctor>();
        private int roomIndex = 0;
        private List<DateTime> newDates = new List<DateTime>();
        private List<(int, int, int)> term = new List<(int, int, int)>();
        private IFileDoctors fileDoctors = FileDoctors.Instance;
        //private FileDoctors fileDoctors = FileDoctors.Instance;
        private FormService formService = new FormService();
        private IFileAppointments fileAppointments = FileAppointments.Instance;
        //private FileAppointments fileAppointments = FileAppointments.Instance;
        private FileRooms fileRooms = FileRooms.Instance;

        public WindowExaminationSchedule()
        {
            InitializeComponent();
            GenerateTerms();
            doctors = fileDoctors.GetDoctors();
            for (int i = 0; i < doctors.Count; i++)
                Combo3.Items.Add(doctors[i].Username.ToString());
        }

        private void ButtonOkFilters_Click(object sender, RoutedEventArgs e)
        {
            Combo4.Items.Clear();
            DateTime firstDate = formService.GetDateAndTimeFromForm(Date.SelectedDate.Value.Date, Combo, 7, 13);
            DateTime secondDate = formService.GetDateAndTimeFromForm(Date2.SelectedDate.Value.Date, Combo2, 7, 13);
            GetFreeAppointments(firstDate, secondDate, term);
            if (newDates.Count > 0) return;
            if (priorityDoctor.IsChecked == true) GetAlternativeAppointmentsForDoctorPriority(firstDate, secondDate, term);
            else if(priorityTime.IsChecked == true) GetAlternativeAppointmentsForTimePriority(firstDate, secondDate, term);
        }

        private void ButtonOkCombo_Click(object sender, RoutedEventArgs e)
        {
            string doctorsUsername;
            doctorsUsername = doctors[Combo3.SelectedIndex].Username;
            string dateAndDoctor = Combo4.SelectedItem.ToString();
            string[] tokens = dateAndDoctor.Split(",");
            if (tokens.Length == 2) doctorsUsername = tokens[1];
            DateTime newDate = DateTime.Parse(tokens[0]);
            appointmentsId = fileAppointments.GenerateAppointmentsId(appointmentsId);
            Appointment appointment = new Appointment(mainWindow.PatientsUsername, doctorsUsername, FileRooms.GetRoomId(roomIndex), newDate, (appointmentsId + 1).ToString(), 0, Int32.Parse(textBox111.Text));
            controller.ScheduleAppointment(appointment);
            pagePatients.UpdateView();
            Close();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            DateTime newDate = formService.GetDateAndTimeFromForm(Date.SelectedDate.Value.Date, Combo, 7, 13);
            appointmentsId = fileAppointments.GenerateAppointmentsId(appointmentsId);
            Appointment appointment = new Appointment(mainWindow.PatientsUsername, doctors[Combo3.SelectedIndex].Username, "0", newDate, (appointmentsId + 1).ToString(), 0, Int32.Parse(textBox111.Text));
            controller.ScheduleAppointment(appointment);
            pagePatients.UpdateView();
            Close();
        }

        private void GetFreeAppointments(DateTime firstDate, DateTime secondDate, List<(int, int, int)> term) {
            Doctor selectedDoctor = fileDoctors.GetDoctor(Combo3.SelectedItem.ToString());
            DateTime newDate;
            for (int i = 0; i < (secondDate - firstDate).TotalDays + 1; i++){
                for (int j = 0; j < term.Count; j++){
                    newDate = firstDate.Date.AddDays(i) + new TimeSpan(term[j].Item1, term[j].Item2, term[j].Item3);
                    Tuple<bool, int> roomIsFree = fileRooms.IsRoomFree(newDate);
                    roomIndex = roomIsFree.Item2;
                    if (fileDoctors.IsDoctorFree(selectedDoctor.Username, newDate) == true && roomIsFree.Item1 == true && newDate <= secondDate && newDate >= firstDate){
                        newDates.Add(newDate);
                        Combo4.Items.Add(newDates[i].ToString());
                    }
                }
            }
        }

        private void GetAlternativeAppointmentsForDoctorPriority(DateTime firstDate, DateTime secondDate, List<(int, int, int)> term) {
            Doctor selectedDoctor = fileDoctors.GetDoctor(Combo3.SelectedItem.ToString());
            DateTime newDate = new DateTime();
            for (int j = 0; j < 3; j++){
                for (int i = 0; i < term.Count; i++){
                    newDate = secondDate.Date.AddDays(j) + new TimeSpan(term[i].Item1, term[i].Item2, term[i].Item3);
                    Tuple<bool, int> roomIsFree = fileRooms.IsRoomFree(newDate);
                    roomIndex = roomIsFree.Item2;
                    if (fileDoctors.IsDoctorFree(selectedDoctor.Username, newDate) == true && roomIsFree.Item1 == true && newDate > secondDate)
                        newDates.Add(newDate);
                }
            }
            for (int j = 0; j < 3; j++){
                for (int i = 0; i < term.Count; i++){
                    newDate = secondDate.Date.AddDays(-j) + new TimeSpan(term[i].Item1, term[i].Item2, term[i].Item3);
                    Tuple<bool, int> roomIsFree = fileRooms.IsRoomFree(newDate);
                    roomIndex = roomIsFree.Item2;
                    if (fileDoctors.IsDoctorFree(selectedDoctor.Username, newDate) == true && roomIsFree.Item1 == true && newDate < firstDate && newDate > DateTime.Now)
                        newDates.Add(newDate);
                }
            }
            newDates.Sort((x, y) => DateTime.Compare(x, y));
            for (int i = 0; i < newDates.Count; i++)
                Combo4.Items.Add(newDates[i].ToString());
        }

        private void GetAlternativeAppointmentsForTimePriority(DateTime firstDate, DateTime secondDate, List<(int, int, int)> time) {
            Doctor doctor;
            DateTime newDate;
            for (int doctorsIndex = 0; doctorsIndex < doctors.Count; doctorsIndex++){
                doctor = doctors[doctorsIndex];
                for (int j = 0; j < (secondDate - firstDate).TotalDays + 1; j++){
                    for (int i = 0; i < time.Count; i++){
                        newDate = firstDate.Date.AddDays(j) + new TimeSpan(time[i].Item1, time[i].Item2, time[i].Item3);
                        Tuple<bool, int> roomIsFree = fileRooms.IsRoomFree(newDate);
                        roomIndex = roomIsFree.Item2;
                        if (fileDoctors.IsDoctorFree(doctor.Username, newDate) == true && roomIsFree.Item1 == true && newDate <= secondDate && newDate >= firstDate){
                            newDates.Add(newDate);
                            Combo4.Items.Add(newDate.ToString() + "," + doctor.Username);
                        }
                    }
                }
            }
        }

        private void GenerateTerms() {
            for (int i = 0; i < 13; i++){
                term.Add((7 + i, 0, 0));
                term.Add((7 + i, 30, 0));
            }
        }
    }
}