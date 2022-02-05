using HospitalApplication.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalApplication.Repository
{
    interface IFileNotifications : IFile
    {
        public List<Notification> GetNotifications();
        public List<Notification> GetNotifications(string patientsId);
        public int GenerateNotificationId(int notificationId);
    }
}
