using HospitalApplication.Service;
using HospitalApplication.Windows.Secretary;
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
    public partial class AllDoctorsWindow : Window
    {
        private FileDoctors fileDoctors = FileDoctors.Instance;
        private DoctorService doctorService = new DoctorService();
        private static AllDoctorsWindow instance;
        public static AllDoctorsWindow Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new AllDoctorsWindow();
                }
                return instance;
            }
        }

        public AllDoctorsWindow()
        {
            InitializeComponent();
            CenterWindow();
            instance = this;
            UpdateDoctors();
        }

        public void UpdateDoctors()
        {
            lvUsers.ItemsSource = null;
            lvUsers.ItemsSource = fileDoctors.GetDoctors();
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

        private void BackHome_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow window = new HomeWindow();
            this.Close();
            window.Show();
        }

        private void AddDoctor_Click(object sender, RoutedEventArgs e)
        {
            AddNewDoctorWindow window = new AddNewDoctorWindow();
            window.Show();
        }

        private void DeleteDoctor_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvUsers.SelectedIndex > -1)) { return; }
            Doctor selectedDoctor = (Doctor)lvUsers.SelectedItem;
            doctorService.DeleteDoctor(selectedDoctor.Id);
            UpdateDoctors();
        }

        private void EditDoctor_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvUsers.SelectedIndex > -1)) { return; }
            Doctor selectedDoctor = (Doctor)lvUsers.SelectedItem;
            EditDoctorWindow window = new EditDoctorWindow(selectedDoctor);
            window.Show();
        }

        private void ViewDoctor_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvUsers.SelectedIndex > -1)) { return; }
            Doctor selectedDoctor = (Doctor)lvUsers.SelectedItem;
            ViewDoctorWindow window = new ViewDoctorWindow(selectedDoctor);
            window.Show();
        }

        private void menuNew_Click(object sender, RoutedEventArgs e)
        {
            AddNewDoctorWindow window = new AddNewDoctorWindow();
            window.Show();
        }

        private void menuDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvUsers.SelectedIndex > -1)) { return; }
            Doctor selectedDoctor = (Doctor)lvUsers.SelectedItem;
            doctorService.DeleteDoctor(selectedDoctor.Id);
            UpdateDoctors();
        }

        private void menuEdit_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvUsers.SelectedIndex > -1)) { return; }
            Doctor selectedDoctor = (Doctor)lvUsers.SelectedItem;
            EditDoctorWindow window = new EditDoctorWindow(selectedDoctor);
            window.Show();
        }

        private void menuView_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvUsers.SelectedIndex > -1)) { return; }
            Doctor selectedDoctor = (Doctor)lvUsers.SelectedItem;
            ViewDoctorWindow window = new ViewDoctorWindow(selectedDoctor);
            window.Show();
        }

        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PrintReport_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvUsers.SelectedIndex > -1)) { return; }
            Doctor selectedDoctor = (Doctor)lvUsers.SelectedItem;
            DataReportForDoctorWindow window = new DataReportForDoctorWindow(selectedDoctor);
            window.Show();
        }
    }
}
