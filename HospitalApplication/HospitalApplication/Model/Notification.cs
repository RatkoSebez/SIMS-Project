using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalApplication.Model
{
    class Notification
    {
        public List<DateTime> Dates { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Repeat { get; set; }
        public string NotificationsId { get; set; }
        public string PatientsId { get; set; }

        public Notification() { }

        public Notification(List<DateTime> datess, string titlee, string descriptionn, string repeatt, string idd, string patientIdd)
        {
            Dates = datess;
            Title = titlee;
            Description = descriptionn;
            Repeat = repeatt;
            NotificationsId = idd;
            PatientsId = patientIdd;
        }
    }
}