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

namespace HospitalApplication.Windows.PatientWindows
{
    public partial class PatientLogin : Window
    {
        private FilePatients filePatients = FilePatients.Instance;
        private MainWindow mainWindow = MainWindow.Instance;

        public PatientLogin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Patient> patients = filePatients.GetPatients();
            string username;
            string password;
            string enteredUsername = Username.Text;
            string enteredPassword = Password.Password;
            for (int i = 0; i < patients.Count; i++)
            {
                username = patients[i].Username;
                password = patients[i].Password;
                if (enteredUsername == username && enteredPassword == password)
                {
                    mainWindow.PatientsUsername = enteredUsername;
                    Home window = new Home();
                    Close();
                    window.Show();
                }
            }
            Error.Text = "* invalid username or password";
        }
    }
}
