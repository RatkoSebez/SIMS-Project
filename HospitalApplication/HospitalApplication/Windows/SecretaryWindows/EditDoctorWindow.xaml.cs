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
    public partial class EditDoctorWindow : Window
    {
        private DoctorService doctorService = new DoctorService();
        private AllDoctorsWindow allDoctorsWindow = AllDoctorsWindow.Instance;
        private Doctor currentSelectedDoctor;
        public EditDoctorWindow(Doctor selectedDoctor)
        {
            InitializeComponent();
            CenterWindow();
            currentSelectedDoctor = selectedDoctor;
            DisplayValuesFromSelectedDoctor(selectedDoctor);
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

        private void DisplayValuesFromSelectedDoctor(Doctor selectedDoctor)
        {
            ComboBox1.Text = selectedDoctor.DoctorType.ToString();
            textBoxFirstName.Text = selectedDoctor.Name;
            textBoxLastName.Text = selectedDoctor.LastName;
            textBoxJMBG.Text = selectedDoctor.Jmbg;
            if (selectedDoctor.SexType == SexType.male)
                MSex.IsChecked = true;
            else
                FSex.IsChecked = true;
            BoxDateTime.Text = selectedDoctor.DateOfBirth.ToString();
            textBoxPlaceOfResidance.Text = selectedDoctor.PlaceOfResidance;
            textBoxPhoneNumber.Text = selectedDoctor.PhoneNumber;
            textBoxEmail.Text = selectedDoctor.Email;
            textBoxUsername.Text = selectedDoctor.Username;
            textBoxPassword.Text = selectedDoctor.Password;
        }

        private void SetNewValuesToSelectedDoctor()
        {
            currentSelectedDoctor.DoctorType = (DoctorType)Enum.Parse(typeof(DoctorType), ComboBox1.Text);
            currentSelectedDoctor.Name = textBoxFirstName.Text;
            currentSelectedDoctor.LastName = textBoxLastName.Text;
            currentSelectedDoctor.Jmbg = textBoxJMBG.Text;
            currentSelectedDoctor.SexType = GetSelectedSexType();
            currentSelectedDoctor.DateOfBirth = GetSelectedDate();
            currentSelectedDoctor.PlaceOfResidance = textBoxPlaceOfResidance.Text;
            currentSelectedDoctor.PhoneNumber = textBoxPhoneNumber.Text;
            currentSelectedDoctor.Email = textBoxEmail.Text;
            currentSelectedDoctor.Username = textBoxUsername.Text;
            currentSelectedDoctor.Password = textBoxPassword.Text;
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

        private DateTime GetSelectedDate()
        {
            string date = BoxDateTime.Text;
            string[] entries = date.Split('/');
            int year = Int32.Parse(entries[2]);
            int month = Int32.Parse(entries[0]);
            int day = Int32.Parse(entries[1]);
            return new DateTime(year, month, day);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            SetNewValuesToSelectedDoctor();
            doctorService.UpdateDoctor(currentSelectedDoctor);
            allDoctorsWindow.UpdateDoctors();
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
