using HospitalApplication.Controller;
using HospitalApplication.Model;
using HospitalApplication.Windows.ManagerWindows;
using HospitalApplication.Windows.SecretaryWindows;
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

namespace HospitalApplication.Windows.Secretary
{
    public partial class HomeWindow : Window
    {
        private NewsController newsController = new NewsController();
        private static HomeWindow instance;
        public static HomeWindow Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new HomeWindow();
                }
                return instance;
            }
        }

        public HomeWindow()
        {
            InitializeComponent();
            CenterWindow();
            instance = this;
            UpdateNews();
        }

        public void UpdateNews()
        {
            lvUsers.ItemsSource = null;
            lvUsers.ItemsSource = newsController.GetAllNews();
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

        private void CreateNews_Click(object sender, RoutedEventArgs e)
        {
            CreateNewsWindow window = new CreateNewsWindow();
            window.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvUsers.SelectedIndex > -1)) { return; }
            News selectedNews = (News)lvUsers.SelectedItem;
            newsController.DeleteNews(selectedNews.Id);
            UpdateNews();
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvUsers.SelectedIndex > -1)) { return; }
            News selectedNews = (News)lvUsers.SelectedItem;
            ViewNewsWindow window = new ViewNewsWindow(selectedNews);
            window.Show();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvUsers.SelectedIndex > -1)) { return; }
            News selectedNews = (News)lvUsers.SelectedItem;
            EditNewsWindow window = new EditNewsWindow(selectedNews);
            window.Show();
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Close();
            window.Show();
        }

        private void AllPatients_Click(object sender, RoutedEventArgs e)
        {
            AllPatientsWindow window = new AllPatientsWindow();
            this.Close();
            window.Show();
        }

        private void EmergencyButton_Click(object sender, RoutedEventArgs e)
        {
            PatientAndAppointmentWindow window = new PatientAndAppointmentWindow();
            this.Close();
            window.Show();
        }

        private void MakeAppointment_Click(object sender, RoutedEventArgs e)
        {
            PatientAndAppointmentWindow window = new PatientAndAppointmentWindow();
            this.Close();
            window.Show();
        }

        private void Doctors_Click(object sender, RoutedEventArgs e)
        {
            AllDoctorsWindow window = new AllDoctorsWindow();
            this.Close();
            window.Show();
        }

        private void RateApp_Click(object sender, RoutedEventArgs e)
        {
            RateApp window = new RateApp();
            window.Show();
        }
    }
}
