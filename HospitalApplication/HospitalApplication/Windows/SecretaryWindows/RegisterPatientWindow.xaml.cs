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
    public partial class RegisterPatientWindow : Window
    {
        private SecretaryController secretaryController = new SecretaryController();
        private AllPatientsWindow allPatientsWindow = AllPatientsWindow.GetInstance();

        public RegisterPatientWindow()
        {
            InitializeComponent();
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

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            MedicalRecord newMedicalRecord = new MedicalRecord(GenerateIdForNewPatient(), 0, 0, textBoxFirstName.Text, textBoxLastName.Text, "empty", textBoxJMBG.Text, GetSelectedDate(), "empty", 
                                                               textBoxPlaceOfResidance.Text, textBoxPhoneNumber.Text, GetSelectedSexType());

            Patient newPatient = new Patient(0, textBoxFirstName.Text, textBoxLastName.Text, GenerateIdForNewPatient(), GetSelectedDate(), textBoxPhoneNumber.Text, textBoxEmail.Text, textBoxPlaceOfResidance.Text, 
                                            (TypeOfPerson)Enum.Parse(typeof(TypeOfPerson), "Patient"), textBoxUsername.Text, textBoxPassword.Text, textBoxJMBG.Text, GetSelectedSexType(), newMedicalRecord, 
                                            new List<Allergen>(), new Tuple<int, DateTime, bool>(0, DateTime.Now, false));

            secretaryController.CreatePatient(newPatient);
            allPatientsWindow.UpdateView();
            Close();
        }

        private SexType GetSelectedSexType()
        {
            SexType sex = SexType.male;
            if (Convert.ToBoolean(MSex.IsChecked)){
                sex = SexType.male;
            }
            else if (Convert.ToBoolean(FSex.IsChecked)){
                sex = SexType.female;
            }
            return sex;
        }

        private string GenerateIdForNewPatient()
        {
            int n = secretaryController.GetPatients().Count;
            int idPatient;
            if (n > 0)
            {
                idPatient = Int32.Parse(secretaryController.GetPatients()[n - 1].Id) + 1;
            }
            else idPatient = 0;
            return idPatient.ToString();
        }

        private DateTime GetSelectedDate()
        {
            string date = BoxDateTime.Text;
            string[] entries = date.Split('/');
            int year = Int32.Parse(entries[2]);
            int month = Int32.Parse(entries[0]);
            int day = Int32.Parse(entries[1]);
            return new DateTime(year, month, day);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
