using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalApplication.Repository
{
    interface IFileAppointments : IFile
    {
        public List<Appointment> GetAppointments();
        public Appointment GetAppointment(DateTime date);
        public int GetAppointmentsIndex(Appointment appointment);
        public List<Appointment> GetAppointments(string patientsId);
        public List<Appointment> GetPastAppointments(string patientsId);
        public List<Appointment> GetAppointments(DateTime firstDate, DateTime secondDate, string patientsId);
        public List<Appointment> GetAppointmentsForSpecificDoctor(string doctorUsername);
        public int GenerateAppointmentsId(int appointmentId);
    }
}
