using HospitalApplication.Windows.Manager.Prostorije;
using HospitalApplication.Windows.ManagerWindows.Views;
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

namespace HospitalApplication.Windows.ManagerWindows
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class ManagerHome : Window
    {
        public ManagerHome()
        {
            InitializeComponent();
            Main.Content = new HomePage();
        }

        private void LogOut_Clicked(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }

        private void Home_Clicked(object sender, RoutedEventArgs e)
        {
            Main.Content = new HomePage();
        }

        private void Rooms_Clicked(object sender, RoutedEventArgs e)
        {
            Main.Content = new AllRoomsPage();
        }

        private void Resource_Clicked(object sender, RoutedEventArgs e)
        {
            Main.Content = new ResourcesPage();
        }
    }
}
