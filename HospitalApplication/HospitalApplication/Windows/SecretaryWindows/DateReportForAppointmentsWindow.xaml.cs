using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    public partial class DateReportForAppointmentsWindow : Window
    {
        public DateReportForAppointmentsWindow()
        {
            InitializeComponent();
            CenterWindow();
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

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            ReportWindow window = new ReportWindow(BoxDateTimeFrom.Text, BoxDateTimeTo.Text);
            window.Show();
            this.Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonThisWeek_Click(object sender, RoutedEventArgs e)
        {
            DateTime monday = GetMonday(DateTime.Now);
            BoxDateTimeFrom.Text = monday.ToString();
            BoxDateTimeTo.Text = monday.AddDays(6).ToString();
        }

        public static DateTime GetMonday(DateTime time)
        {
            if (time.DayOfWeek != DayOfWeek.Monday)
                return time.Subtract(new TimeSpan((int)time.DayOfWeek - 1, 0, 0, 0));

            return time;
        }
    }
}
