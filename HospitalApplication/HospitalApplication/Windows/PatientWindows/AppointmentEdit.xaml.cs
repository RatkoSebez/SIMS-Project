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
using HospitalApplication.Logic;
using HospitalApplication.Repository;
using HospitalApplication.Windows.PatientWindows;
using HospitalApplication.WorkWithFiles;
using Logic;
using Model;
using WorkWithFiles;

namespace HospitalApplication.Windows.Patient1
{
    public partial class WindowExaminationEdit : Window
    {
        private IFileDoctors fileDoctors = FileDoctors.Instance;
        //private FileDoctors fileDoctors = FileDoctors.Instance;
        private List<Doctor> doctors;
        private PatientsPage patientsPage = PatientsPage.Instance;
        private AppointmentController controller = new AppointmentController();

        public WindowExaminationEdit()
        {
            InitializeComponent();
            doctors = fileDoctors.GetDoctors();
            for (int i = 0; i < doctors.Count; i++)
                Combo.Items.Add(doctors[i].Username.ToString());
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            Appointment appointment = (Appointment)patientsPage.lvUsers.SelectedItem;
            controller.EditAppointment(appointment, Combo.SelectedItem.ToString());
            patientsPage.UpdateView();
            Close();
        }
    }
}