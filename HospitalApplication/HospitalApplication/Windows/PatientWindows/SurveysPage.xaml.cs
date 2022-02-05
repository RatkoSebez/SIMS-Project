using HospitalApplication.Controller;
using HospitalApplication.Model;
using HospitalApplication.Repository;
using HospitalApplication.Windows.Patient1;
using Logic;
using Model;
using System;
using System.Collections.Generic;
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
    public partial class SurveysPage : Page
    {
        private MainWindow mainWindow = MainWindow.Instance;
        private IFileSurveys fileSurveys = FileSurveys.Instance;
        //private FileSurveys fileSurveys = FileSurveys.Instance;

        public SurveysPage()
        {
            InitializeComponent();
        }

        private void RateDoctor_Click(object sender, RoutedEventArgs e)
        {
            List<string> doctorUsernames = fileSurveys.GetAllowedDoctorsForSurvey(mainWindow.PatientsUsername);
            if (doctorUsernames.Count < 1) {
                MessageBox.Show("You must attend examination before rating doctor.");
                return;
            }
            DoctorSurvey window = new DoctorSurvey(doctorUsernames.Distinct().ToList());
            window.ShowDialog();
        }

        private void RateHospital_Click(object sender, RoutedEventArgs e)
        {
            if (fileSurveys.IsHospitalSurveyAllowed(mainWindow.PatientsUsername)) {
                WindowRateHospital window = new WindowRateHospital();
                window.ShowDialog();
            }
        }
    }
}