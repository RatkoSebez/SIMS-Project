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
    public partial class AddNewDoctorWindow : Window
    {

        private FileDoctors fileDoctors = FileDoctors.Instance;
        private DoctorService doctorService = new DoctorService();
        private AllDoctorsWindow allDoctorsWindow = AllDoctorsWindow.Instance;

        public AddNewDoctorWindow()
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
            Doctor newDoctor = new Doctor((DoctorType)Enum.Parse(typeof(DoctorType), ComboBox1.Text), new List<DateTime>(), textBoxFirstName.Text, textBoxLastName.Text, GenerateIdForNewDoctor(),
                                          GetSelectedDate(), textBoxPhoneNumber.Text, textBoxEmail.Text, textBoxPlaceOfResidance.Text, 0, textBoxUsername.Text, textBoxPassword.Text, textBoxJMBG.Text,
                                          GetSelectedSexType());

            doctorService.CreateDoctor(newDoctor);
            allDoctorsWindow.UpdateDoctors();
            Close();
        }

        private string GenerateIdForNewDoctor()
        {
            int n = fileDoctors.GetDoctors().Count;
            int idDoctor;
            if (n > 0){
                idDoctor = Int32.Parse(fileDoctors.GetDoctors()[n - 1].Id) + 1;
            }
            else idDoctor = 0;
            return idDoctor.ToString();
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

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
