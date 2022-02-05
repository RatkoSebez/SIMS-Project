using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HospitalApplication.Model;
using HospitalApplication.Repository;
using Model;
using Nancy.Json;
using Newtonsoft.Json;

namespace HospitalApplication.WorkWithFiles
{
    class FileNotifications : IFileNotifications
    {
        private string path = "../../../Data/notifications.json";
        private List<Notification> notifications;
        private static FileNotifications instance;
        public static FileNotifications Instance
        {
            get
            {
                if (null == instance)
                    instance = new FileNotifications();
                return instance;
            }
        }

        private FileNotifications() {
            Read();
        }

        public void Read()
        {
            string json = File.ReadAllText(path);
            notifications = JsonConvert.DeserializeObject<List<Notification>>(json);
        }

        public void Write()
        {
            string json = JsonConvert.SerializeObject(notifications, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public List<Notification> GetNotifications() {
            return notifications;
        }

        public List<Notification> GetNotifications(string patientsId)
        {
            List<Notification> patientsNotifications = new List<Notification>();
            for (int i = 0; i < notifications.Count; i++)
                if (notifications[i].PatientsId == patientsId && notifications[i].Dates[notifications[i].Dates.Count - 1] > DateTime.Now)
                    patientsNotifications.Add(notifications[i]);
            return patientsNotifications;
        }

        public int GenerateNotificationId(int notificationId)
        {
            if (notificationId == 100000)
                for (int i = 0; i < notifications.Count; i++)
                    notificationId = Math.Max(notificationId, Int32.Parse(notifications[i].NotificationsId));
            return notificationId;
        }
    }
}