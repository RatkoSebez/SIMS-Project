using HospitalApplication.Model;
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
    /// Interaction logic for PrintView.xaml
    /// </summary>
    public partial class PrintView : Window
    {
        private FileDrugs fileDrugs = FileDrugs.Instance;
        private IPrint logic = new GenerateLibary();
        public PrintView()
        {
            InitializeComponent();
            lvDrugs.ItemsSource = fileDrugs.AllDrugs();
            date.Text = DateTime.Now.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                this.IsEnabled = false;
                logic.PrintReport();
            }
            finally
            {
                this.IsEnabled = true;
            }
        }
    }
}
