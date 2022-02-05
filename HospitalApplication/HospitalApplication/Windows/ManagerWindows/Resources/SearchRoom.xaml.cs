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

namespace HospitalApplication.Windows.Manager.Resources
{
    public partial class SearchRoom : Window
    {
        private int idRoomToFind;
        public SearchRoom()
        {
            InitializeComponent();
        }

        private void Search_Clicked(object sender, RoutedEventArgs e)
        {
            idRoomToFind = int.Parse(forSearch.Text);
            Resources window = new Resources(idRoomToFind);
            window.Show();
            Close();
        }
    }
}
