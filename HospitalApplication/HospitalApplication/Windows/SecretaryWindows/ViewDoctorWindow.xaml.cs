using HospitalApplication.Service;
using HospitalApplication.WorkWithFiles;
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

namespace HospitalApplication.Windows.SecretaryWindows
{
    public partial class ViewDoctorWindow : Window
    {
        private Doctor currentSelectedDoctor;

        public ViewDoctorWindow(Doctor selectedDoctor)
        {
            InitializeComponent();
            CenterWindow();
            currentSelectedDoctor = selectedDoctor;
            DisplayValuesFromSelectedDoctor(selectedDoctor);
        }

        private void DisplayValuesFromSelectedDoctor(Doctor selectedDoctor)
        {
            ComboBox1.Text = selectedDoctor.DoctorType.ToString();
            textBoxFirstName.Text = selectedDoctor.Name;
            textBoxLastName.Text = selectedDoctor.LastName;
            textBoxJMBG.Text = selectedDoctor.Jmbg;
            textBoxSex.Text = selectedDoctor.SexType.ToString();
            textBoxDateOfBirth.Text = selectedDoctor.DateOfBirth.ToString();
            textBoxPlaceOfResidance.Text = selectedDoctor.PlaceOfResidance;
            textBoxPhoneNumber.Text = selectedDoctor.PhoneNumber;
            textBoxEmail.Text = selectedDoctor.Email;
            textBoxUsername.Text = selectedDoctor.Username;
            textBoxPassword.Text = selectedDoctor.Password;
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

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
