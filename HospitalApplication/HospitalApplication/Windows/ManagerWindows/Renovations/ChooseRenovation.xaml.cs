using HospitalApplication.Windows.Manager.Renovationn;
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

namespace HospitalApplication.Windows.ManagerWindows.Renovations
{
    public partial class ChooseRenovation : Window
    {
        public ChooseRenovation()
        {
            InitializeComponent();
        }

        private void One_Clicked(object sender, RoutedEventArgs e)
        {
            AddRenovation window = new AddRenovation();
            window.Show();
            Close();
        }

        private void TwoToOne_Clicked(object sender, RoutedEventArgs e)
        {
            RenovateTwoRoomInOne window = new RenovateTwoRoomInOne();
            window.Show();
            Close();
        }

        private void OneToTwo_Clicked(object sender, RoutedEventArgs e)
        {
            RenovateOneRoomInTwo window = new RenovateOneRoomInTwo(true);
            window.Show();
            Close();
        }
    }
}
