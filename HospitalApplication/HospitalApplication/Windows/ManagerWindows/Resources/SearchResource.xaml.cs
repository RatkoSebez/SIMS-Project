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
    public partial class SearchResource : Window
    {
        public SearchResource()
        {
            InitializeComponent();
        }

        private void Search_Clicked(object sender, RoutedEventArgs e)
        {
            string resourceName = forSearch.Text;
            int quantity = int.Parse(num.Text);
            SearchedResource window = new SearchedResource(resourceName, quantity);
            window.Show();
            Close();
        }
    }
}
