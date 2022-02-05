using HospitalApplication.Model;
using HospitalApplication.Repository;
using HospitalApplication.WorkWithFiles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;

namespace HospitalApplication.Logic
{
    class NotificationService
    {
        private IFileNotifications fileNotifications = FileNotifications.Instance;
        //private FileNotifications fileNotifications = FileNotifications.Instance;
        private List<Notification> notifications;
        private List<Notification> patientsNotifications;
        public static bool FlagIsMarked { get; set; } = false;
        
        public NotificationService()
        {
            notifications = fileNotifications.GetNotifications();
        }

        public void ScheduleNotification(Notification notification)
        {
            notifications.Add(notification);
            fileNotifications.Write();
        }

        public void CancelNotification(Notification notification)
        {
            for (int i = 0; i < notifications.Count; i++)
                if (notifications[i].NotificationsId == notification.NotificationsId) notifications.RemoveAt(i);
            fileNotifications.Write();
        }

        public void EditNotification(Notification notification, string title, string descriptioin, string repeatForNextDays, DateTime newDate)
        {
            notification.Title = title;
            notification.Description = descriptioin;
            notification.Repeat = repeatForNextDays;
            List<DateTime> dates = new List<DateTime>();
            dates.Add(newDate);
            for (int i = 0; i < Int32.Parse(repeatForNextDays); i++)
                dates.Add(newDate.AddDays(1));
            notification.Dates = dates;
            fileNotifications.Write();
        }

        public void StartNotificationThread(string patientsUsername)
        {
            patientsNotifications = fileNotifications.GetNotifications(patientsUsername);
            //test notifikacija
            /*List<DateTime> dates = new List<DateTime>();
            DateTime date = new DateTime(2021, 5, 26, 15, 14, 0);
            dates.Add(date);
            Notification nt = new Notification(dates, "hello", "world", "1", "100050", "m");
            patientsNotifications.Add(nt);*/
            Thread workerThread = new Thread(new ThreadStart(NotificationThread));
            workerThread.SetApartmentState(ApartmentState.STA);
            workerThread.Start();
        }

        private void NotificationThread()
        {
            while (true){
                Thread.Sleep(1000);
                for (int i = 0; i < patientsNotifications.Count; i++){
                    for (int j = 0; j < patientsNotifications[i].Dates.Count; j++){
                        if (patientsNotifications[i].Dates[j].ToString("HH:mm") == DateTime.Now.ToString("HH:mm")){
                            MessageBox.Show(patientsNotifications[i].Description, patientsNotifications[i].Title);
                            Thread.Sleep(60000);
                        }
                    }
                }
                if (FlagIsMarked) break;
            }
        }
    }
}