using HospitalApplication.Logic;
using HospitalApplication.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalApplication.Controller
{
    class NotificationController
    {
        private NotificationService notificationService = new NotificationService();

        public void ScheduleNotification(Notification notification)
        {
            notificationService.ScheduleNotification(notification);
        }

        public void CancelNotification(Notification notification)
        {
            notificationService.CancelNotification(notification);
        }

        public void EditNotification(Notification notification, string title, string descriptioin, string repForNextDays, DateTime date)
        {
            notificationService.EditNotification(notification, title, descriptioin, repForNextDays, date);
        }
    }
}