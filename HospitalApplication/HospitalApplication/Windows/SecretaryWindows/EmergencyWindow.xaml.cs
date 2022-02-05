using HospitalApplication.Controller;
using HospitalApplication.Service;
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
    public partial class EmergencyWindow : Window
    {
        private RoomService roomService = new RoomService();
        private SecretaryController secretaryController = new SecretaryController();
        private AppointmentController appointmentController = new AppointmentController();
        private FileDoctors fileDoctors = FileDoctors.Instance;
        private FileAppointments fileAppointments = FileAppointments.Instance;
        private AppointmentService appointmentService = new AppointmentService();
        private DoctorService doctorService = new DoctorService();
        private List<Doctor> doctors = new List<Doctor>();
        private List<Doctor> filteredDoctors = new List<Doctor>();
        private List<Room> rooms = new List<Room>();
        private FileRooms fileRooms = FileRooms.Instance;
        private List<Appointment> appointments = new List<Appointment>();
        private Patient selectedPatient;
        private int idAppointment = 100000;
        Tuple<bool, int> isFreeRoom;
        public const int defaultValueOfPostpone = 1000;

        public EmergencyWindow(string idPatient)
        {
            InitializeComponent();
            CenterWindow();
            LoadPatientsDoctorsRoomsAndAppointments(idPatient);
            appointments.Sort();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (ComboFreeTerms.SelectedIndex == -1){
                ScheduleAndMoveAppointment();
                Close();
            }
            else
            {
                if (HasTwoAppointmentAtTheSameDateTime())
                    SheduleFreeAppointment();
            }
            Close();
        }

        private bool HasTwoAppointmentAtTheSameDateTime()
        {
            for (int i = 0; i < appointments.Count; i++)
            {
                if (appointments[i].ExaminationStart.Equals(GetTheClosestAppointment()) && appointments[i].PatientsId.Equals(selectedPatient.Username)){
                    MessageBox("Ne mozete zakazati 2 termina u isto vrijeme!");
                    return false;
                }
            }
            return true;
        }

        private void ButtonFilter_Click(object sender, RoutedEventArgs e)
        {
            isFreeRoom = fileRooms.IsRoomFree(GetTheClosestAppointment());
            if (isFreeRoom.Item1 && FilterDoctors()) {
                MessageBox("Imamo slobodan termin u najblizem roku!"); 
            }
            else{
                ClearComboBoxes();
                AddScheduledAppointmentsAndDoctorsInComboBoxes();
            }
        }

        private void AddScheduledAppointmentsAndDoctorsInComboBoxes()
        {
            DoctorType typeDoctor = (DoctorType)Enum.Parse(typeof(DoctorType), ComboTypeDoctor.Text);
            for (int i = 0; i < appointments.Count; i++)
            {
                if (appointments[i].ExaminationStart >= DateTime.Now){
                    ComboSheduledTerms.Items.Add(appointments[i].ExaminationStart.ToString());
                } 
                if (!ComboAvailableDoctors.Items.Contains(appointments[i].DoctorsId) && fileDoctors.GetDoctorById(appointments[i].DoctorsId).DoctorType.Equals(typeDoctor)){ 
                    ComboAvailableDoctors.Items.Add(appointments[i].DoctorsId);
                }
            }
        }

        private DateTime GetTheClosestAppointment()
        {
            if (DateTime.Now.Minute < 30){
                return DateTime.Today + new TimeSpan(DateTime.Now.Hour, 30, 0);
            }
            else{
                return DateTime.Today + new TimeSpan(DateTime.Now.Hour + 1, 0, 0);
            }
        }

        private void LoadPatientsDoctorsRoomsAndAppointments(string idPatient)
        {
            selectedPatient = secretaryController.GetPatient(idPatient);
            doctors = fileDoctors.GetDoctors();
            rooms = fileRooms.ShowAllRooms();
            appointments = appointmentService.appointments;
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

        private void ScheduleAndMoveAppointment()
        {
            DateTime selectedSheduledDateTime = DateTime.Parse(ComboSheduledTerms.SelectedItem.ToString());
            MoveAppointment(fileAppointments.GetAppointment(selectedSheduledDateTime), selectedSheduledDateTime);
            isFreeRoom = fileRooms.IsRoomFree(selectedSheduledDateTime);
            ScheduleAppointment(selectedSheduledDateTime);
            Close();
        }

        private void ScheduleAppointment(DateTime selectedSheduledDateTime)
        {
            Appointment appointment = new Appointment(selectedPatient.Username, ComboAvailableDoctors.Text, rooms[isFreeRoom.Item2].RoomId.ToString(),
                                                      selectedSheduledDateTime, (GenerateAppointmentId() + 1).ToString(), 0, defaultValueOfPostpone);
            appointmentService.ScheduleAppointment(appointment);
        }

        private void MoveAppointment(Appointment appointment, DateTime selectedDateTime)
        {
            DateTime newDate = selectedDateTime.AddDays(appointment.PostponeAppointment);
            appointment.PostponeAppointment = defaultValueOfPostpone;
            isFreeRoom = fileRooms  .IsRoomFree(newDate);
            if (IsFreeDoctorAndRoom(appointment, newDate)){
                appointmentService.MoveAppointment(appointment, newDate);
            }
        }

        private bool IsFreeDoctorAndRoom(Appointment appointment, DateTime newDate)
        {
            return (fileDoctors.IsDoctorFree(appointment.DoctorsId, newDate) == true && isFreeRoom.Item1 == true);
        }

        private void SheduleFreeAppointment()
        {
            Appointment appointment = new Appointment(selectedPatient.Username, ComboAvailableDoctors.Text, rooms[isFreeRoom.Item2].RoomId.ToString(),
                                                      GetTheClosestAppointment(), (GenerateAppointmentId() + 1).ToString(), 0, defaultValueOfPostpone);

            appointmentController.ScheduleAppointment(appointment); 
        }

        private System.Windows.MessageBoxResult MessageBox(string str)
        {
            return System.Windows.MessageBox.Show(str, "Info", MessageBoxButton.OK);
        }

        private void ClearComboBoxes()
        {
            ComboAvailableDoctors.Items.Clear();
            ComboFreeTerms.Items.Clear();
            ComboSheduledTerms.Items.Clear();
        }

        private bool FilterDoctors()
        {
            ClearComboBoxes();
            filteredDoctors.Clear();
            if (ComboTypeDoctor.Text == DoctorType.cardiology.ToString())
            {
                for (int i = 0; i < doctors.Count; i++)
                {
                    if (doctors[i].DoctorType.Equals(DoctorType.cardiology) && IsAvailableFiltredDoctors(doctors[i])) 
                    {
                        ComboAvailableDoctors.Items.Add(doctors[i].Username.ToString());
                        filteredDoctors.Add(doctors[i]); 
                        ComboFreeTerms.Items.Add(GetTheClosestAppointment());
                    }
                }
                return IsEmptyFiltredDoctors();
            }
            else if (ComboTypeDoctor.Text == DoctorType.surgery.ToString())
            {
                for (int i = 0; i < doctors.Count; i++)
                {
                    if (doctors[i].DoctorType.Equals(DoctorType.surgery) && IsAvailableFiltredDoctors(doctors[i]))
                    {
                        ComboAvailableDoctors.Items.Add(doctors[i].Username.ToString());
                        filteredDoctors.Add(doctors[i]); 
                        ComboFreeTerms.Items.Add(GetTheClosestAppointment());
                    }
                }
                return IsEmptyFiltredDoctors();
            }
            return true;
        }

        private bool IsEmptyFiltredDoctors()
        {
            return !(filteredDoctors.Count == 0);
        }

        private bool IsAvailableFiltredDoctors(Doctor selectedDoctor)
        {
            for (int i = 0; i < selectedDoctor.Scheduled.Count; i++)
            {
                if (selectedDoctor.Scheduled[i] == GetTheClosestAppointment()) {
                    return false;
                }
            }
            return true;
        }

        private int GenerateAppointmentId()
        {
            for (int i = 0; i < appointments.Count; i++)
            {
                if (Int32.Parse(appointments[i].ExaminationId) > idAppointment)
                    idAppointment = Int32.Parse(appointments[i].ExaminationId);
            }
            return idAppointment;
        }
    }
}
