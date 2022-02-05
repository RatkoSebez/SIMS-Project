using HospitalApplication.Model;
using HospitalApplication.Windows.PatientWindows;
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

namespace HospitalApplication.Windows.Patient1
{
    public partial class WindowNotificationInfo : Window
    {
        private NotificationsPage notificationsPage = NotificationsPage.Instance;

        public WindowNotificationInfo()
        {
            InitializeComponent();
            Notification notification = (Notification)notificationsPage.lvUsers.SelectedItem;
            Date.Text = notification.Dates[0].ToString();
            DrugName.Text = notification.Title;
            Description.Text = notification.Description;
            Days.Text = notification.Repeat;
        }
    }
}