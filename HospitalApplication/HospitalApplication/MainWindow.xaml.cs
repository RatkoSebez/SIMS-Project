using HospitalApplication.Windows.Manager;
﻿
using HospitalApplication.Windows.Doctor1;
using HospitalApplication.Windows.Secretary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Model;
using Logic;
using HospitalApplication.Controller;
using HospitalApplication.Windows.PatientWindows;
using HospitalApplication.Windows.ManagerWindows;

namespace HospitalApplication
{
    public partial class MainWindow : Window
    {
        public string PatientsUsername { get; set; }
        private FilePatients filePatients = FilePatients.Instance;

        private static MainWindow instance;
        public static MainWindow Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new MainWindow();
                }
                return instance;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            instance = this;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Username.Text.Equals("sekretar") && Password.Password.Equals("123"))
            {
                HomeWindow window = new HomeWindow();
                Close();
                window.Show();
            }
            else if (Username.Text.Equals("upravnik") && Password.Password.Equals("123"))
            {
                WindowsManager window = new WindowsManager();
                Close();
                window.Show();
            }
            else if (Username.Text.Equals("lekar") && Password.Password.Equals("123"))
            {
                DoctorWindow window = new DoctorWindow();
                Close();
                window.Show();
            }
            else 
            {
                List<Patient> patients = filePatients.GetPatients();
                string username;
                string password;
                PatientsUsername = Username.Text;
                string enteredPassword = Password.Password;
                for (int i = 0; i < patients.Count; i++)
                {
                    username = patients[i].Username;
                    password = patients[i].Password;
                    if (PatientsUsername == username && enteredPassword == password){
                        Home window = new Home();
                        Close();
                        window.Show();
                    }
                }
                InvalidInfoLabel.Content = "* invalid username or password";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PatientLogin window = new PatientLogin();
            window.Show();
            Close();
        }
    }
}
