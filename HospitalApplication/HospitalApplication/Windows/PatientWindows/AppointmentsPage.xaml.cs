using HospitalApplication.Controller;
using HospitalApplication.Logic;
using HospitalApplication.Model;
using HospitalApplication.Repository;
using HospitalApplication.Windows.Patient1;
using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkWithFiles;

namespace HospitalApplication.Windows.PatientWindows
{
    public partial class PatientsPage : Page
    {
        private MainWindow mainWindow = MainWindow.Instance;
        private AppointmentController controller = new AppointmentController();
        private IFileAppointments fileAppointments = FileAppointments.Instance;
        //private FileAppointments fileAppointments = FileAppointments.Instance;
        private List<Appointment> appointments = new List<Appointment>();
        private List<Appointment> pastAppointments = new List<Appointment>();
        private ObservableCollection<Appointment> pastAppointmentsObservable;
        //public static RoutedCommand MyCommand = new RoutedCommand();

        private static PatientsPage instance;
        public static PatientsPage Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new PatientsPage();
                }
                return instance;
            }
        }

        public PatientsPage()
        {
            InitializeComponent();
            instance = this;
            appointments = fileAppointments.GetAppointments(mainWindow.PatientsUsername);
            appointments.Sort((x, y) => DateTime.Compare(x.ExaminationStart, y.ExaminationStart));
            lvUsers.ItemsSource = appointments;
            pastAppointments = fileAppointments.GetPastAppointments(mainWindow.PatientsUsername);
            pastAppointments.Sort((x, y) => DateTime.Compare(x.ExaminationStart, y.ExaminationStart));
            pastAppointmentsObservable = new ObservableCollection<Appointment>(pastAppointments);
            lvUsersPast.ItemsSource = pastAppointmentsObservable;
            NotificationService notificationService = new NotificationService();
            notificationService.StartNotificationThread(mainWindow.Username.Text);
            //MyCommand.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
            //CommandBindings.Add(new CommandBinding(MyCommand, ScheduleAppointment_Click));
        }

        public void UpdateView()
        {
            List<Appointment> examinations = fileAppointments.GetAppointments(mainWindow.PatientsUsername);
            examinations.Sort((x, y) => DateTime.Compare(x.ExaminationStart, y.ExaminationStart));
            lvUsers.ItemsSource = null;
            lvUsers.ItemsSource = examinations;
        }

        private void ScheduleAppointment_Click(object sender, RoutedEventArgs e)
        {
            WindowExaminationSchedule window = new WindowExaminationSchedule();
            window.ShowDialog();
        }

        private void CancelAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvUsers.SelectedIndex > -1)) return;
            Appointment appointment = (Appointment)lvUsers.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Do you want to cancel examination?", "Confirmation", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    controller.CancelAppointment(appointment);
                    UpdateView();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void MoveAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvUsers.SelectedIndex > -1)) return;
            WindowExaminationMove window = new WindowExaminationMove();
            window.ShowDialog();
        }

        private void EditAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvUsers.SelectedIndex > -1)) return;
            WindowExaminationEdit window = new WindowExaminationEdit();
            window.ShowDialog();
        }

        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            Report window = new Report();
            window.ShowDialog();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            pastAppointmentsObservable.Clear();
            for (int i = 0; i < pastAppointments.Count; i++)
                if (pastAppointments[i].ExaminationStart.ToString().Contains(Search.Text)) pastAppointmentsObservable.Add(pastAppointments[i]);
        }
    }
}