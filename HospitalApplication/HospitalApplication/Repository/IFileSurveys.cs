using HospitalApplication.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalApplication.Repository
{
    interface IFileSurveys : IFile
    {
        public List<Survey> GetSurveys();
        public bool IsHospitalSurveyAllowed(string patientsUsername);
        public bool IsApplicationSurveyAllowed(string patientsUsername);
        public List<string> GetAllowedDoctorsForSurvey(string patientsUsername);
    }
}
