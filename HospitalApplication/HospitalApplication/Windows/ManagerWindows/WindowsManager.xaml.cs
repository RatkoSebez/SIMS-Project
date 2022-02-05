using HospitalApplication.Windows.Manager.Medicines;
using HospitalApplication.Windows.Manager.Prostorije;
using HospitalApplication.Windows.Manager.Resources;
using HospitalApplication.Windows.ManagerWindows;
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

namespace HospitalApplication.Windows.Manager
{
    public partial class WindowsManager : Window
    {
        public WindowsManager()
        {
            InitializeComponent();
        }

        private void Rooms_Clicked(object sender, RoutedEventArgs e)
        {
            AllRooms window = new AllRooms();
            window.Show();
        }

        private void Resources_Clicked(object sender, RoutedEventArgs e)
        {
            Resources.Menu window = new Resources.Menu();
            window.Show();
        }

        private void Renovation_Clicked(object sender, RoutedEventArgs e)
        {
            Renovationn.Renovations window = new Renovationn.Renovations();
            window.Show();
        }

        private void Medicines_Clicked(object sender, RoutedEventArgs e)
        {
            Medicines.main window = new main();
            window.Show();
        }

        private void RateApp_Clicked(object sender, RoutedEventArgs e)
        {
            RateApp window = new RateApp();
            window.Show();
        }
    }
}
