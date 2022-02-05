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
    public partial class EditRegisterPatientWindow : Window
    {
        private Patient selectedPatient;
        private AllPatientsWindow allPatientWindow = AllPatientsWindow.GetInstance();
        private SecretaryController secretaryController = new SecretaryController();

        public EditRegisterPatientWindow(string idPatient)
        {
            InitializeComponent();
            CenterWindow();
            selectedPatient = secretaryController.GetPatient(idPatient);
            DisplayValuesFromSelectedPatient();
        }

        private void DisplayValuesFromSelectedPatient()
        {
            ComboBox1.Text = selectedPatient.TypeAcc.ToString();
            textBoxFirstName.Text = selectedPatient.Name;
            textBoxLastName.Text = selectedPatient.LastName;
            textBoxJMBG.Text = selectedPatient.Jmbg;

            if (selectedPatient.SexType == SexType.male){
                MSex.IsChecked = true;
            }
            else{
                FSex.IsChecked = true;
            }

            BoxDateTime.Text = selectedPatient.DateOfBirth.ToString();
            textBoxPlaceOfResidance.Text = selectedPatient.PlaceOfResidance;
            textBoxEmail.Text = selectedPatient.Email;
            textBoxPhoneNumber.Text = selectedPatient.PhoneNumber;
            textBoxUsername.Text = selectedPatient.Username;
            textBoxPassword.Text = selectedPatient.Password;
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

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            SetNewValuesToSelectedPatient();
            secretaryController.UpdatePatient(selectedPatient);
            allPatientWindow.UpdateView();
            Close();
        }

        private void SetNewValuesToSelectedPatient()
        {
            selectedPatient.TypeAcc = (AccountType)Enum.Parse(typeof(AccountType), ComboBox1.Text);
            selectedPatient.Name = textBoxFirstName.Text;
            selectedPatient.LastName = textBoxLastName.Text;
            selectedPatient.Jmbg = textBoxJMBG.Text;
            selectedPatient.SexType = GetSelectedSexType();
            selectedPatient.DateOfBirth = GetNewSelectedDate();
            selectedPatient.PlaceOfResidance = textBoxPlaceOfResidance.Text;
            selectedPatient.Email = textBoxEmail.Text;
            selectedPatient.PhoneNumber = textBoxPhoneNumber.Text;
            selectedPatient.Username = textBoxUsername.Text;
            selectedPatient.Password = textBoxPassword.Text;
        }

        private SexType GetSelectedSexType()
        {
            SexType sex = SexType.male;
            if (Convert.ToBoolean(MSex.IsChecked))
                sex = SexType.male;
            else if (Convert.ToBoolean(FSex.IsChecked))
                sex = SexType.female;
            return sex;
        }

        private DateTime GetNewSelectedDate()
        {
            string date = BoxDateTime.Text;
            string[] entries = date.Split('/');
            int year = Int32.Parse(entries[2]);
            int month = Int32.Parse(entries[0]);
            int day = Int32.Parse(entries[1]);
            return new DateTime(year, month, day);
        }

        private void Cancle_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
