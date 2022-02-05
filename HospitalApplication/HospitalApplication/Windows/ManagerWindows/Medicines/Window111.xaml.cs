using HospitalApplication.Repository;
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

namespace HospitalApplication.Windows.ManagerWindows.Medicines
{
    /// <summary>
    /// Interaction logic for Window111.xaml
    /// </summary>
    public partial class Window111 : Window
    {
        private FileDrugs fileDrugs = FileDrugs.Instance;
        public Window111()
        {
            InitializeComponent();
            lvDrugs.ItemsSource = fileDrugs.AllDrugs();
            date.Text = DateTime.Now.ToString();
        }
    }
}
