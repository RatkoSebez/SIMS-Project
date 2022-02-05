using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorkWithFiles;

namespace HospitalApplication.Windows.SecretaryWindows
{
    public partial class ReportWindow : Window
    {
        private FileAppointments fileAppointments = FileAppointments.Instance;
        private List<Appointment> oldAppointments = new List<Appointment>();
        private List<Appointment> newAppointments = new List<Appointment>();

        public ReportWindow(string fromDate, string toDate)
        {
            InitializeComponent();
            CenterWindow();
            oldAppointments = fileAppointments.GetAppointments();
            FilterAppointments(fromDate, toDate);
        }

        private void FilterAppointments(string fromDate, string toDate)
        {
            for (int i = 0; i < oldAppointments.Count(); i++)
            {
                if (oldAppointments[i].ExaminationStart >= Convert.ToDateTime(fromDate) && oldAppointments[i].ExaminationStart <= Convert.ToDateTime(toDate))
                    newAppointments.Add(oldAppointments[i]);
            }
            lvUsers.ItemsSource = newAppointments;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(print, "report");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }
    }
}
