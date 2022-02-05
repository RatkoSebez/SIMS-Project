using HospitalApplication.Controller;
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
using WorkWithFiles;

namespace HospitalApplication.Windows.Secretary
{
    public partial class MoveAppointment : Window
    {

       private FileAppointments fileAppointments = FileAppointments.Instance;


        private static MoveAppointment instance;
        public static MoveAppointment Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new MoveAppointment();
                }
                return instance;
            }
        }

        public MoveAppointment()
        {
            InitializeComponent();
            CenterWindow();
            instance = this;
            lvUsers.ItemsSource = fileAppointments.GetAppointments();
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

        private void MoveAppointment_Click(object sender, RoutedEventArgs e)
        {
            FunMoveAppointmentWindow window = new FunMoveAppointmentWindow();
            window.Show();
        }
        
        private void RefreshList_Click(object sender, RoutedEventArgs e)
        {
            lvUsers.ItemsSource = fileAppointments.GetAppointments();
        }
    }
}
