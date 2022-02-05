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
    public partial class ViewPatientWindow : Window
    {
        private SecretaryController secretaryController = new SecretaryController();
        private Patient selectedPatient;

        public ViewPatientWindow(string idPatient)
        {
            InitializeComponent();
            CenterWindow();
            selectedPatient = secretaryController.GetPatient(idPatient);
            DisplayValuesFromSelectedPatient();
        }

        private void DisplayValuesFromSelectedPatient()
        {
            TypeAcc.Content = TypeAcc.Content + selectedPatient.TypeAcc.ToString();
            FirstName.Content = FirstName.Content + selectedPatient.Name;
            LastName.Content = LastName.Content + selectedPatient.LastName;
            Jmbg.Content = Jmbg.Content + selectedPatient.Jmbg;
            Sex.Content = Sex.Content + selectedPatient.SexType.ToString();
            DateOfBirth.Content = DateOfBirth.Content + selectedPatient.DateOfBirth.ToString("dd.MM.yyyy.");
            PlaceOfResidance.Content = PlaceOfResidance.Content + selectedPatient.PlaceOfResidance;
            Email.Content = Email.Content + selectedPatient.Email;
            PhoneNumber.Content = PhoneNumber.Content + selectedPatient.PhoneNumber;
            Username.Content = Username.Content + selectedPatient.Username;
            Password.Content = Password.Content + selectedPatient.Password;
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
    }
}
