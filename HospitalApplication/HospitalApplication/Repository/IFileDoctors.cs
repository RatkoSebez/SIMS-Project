using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalApplication.Repository
{
    interface IFileDoctors : IFile
    {
        public List<Doctor> GetDoctors();
        public Doctor GetDoctor(string doctorsUsername);
        public Doctor GetDoctorById(string doctorsId);
        public bool IsDoctorFree(string doctorsUsername, DateTime date);
        public void AddAppointmentToDoctor(string doctorsUsername, DateTime date);
        public void RemoveAppointmentFromDoctor(string doctorsUsername, DateTime date);
    }
}
