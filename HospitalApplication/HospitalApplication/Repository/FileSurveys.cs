using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using HospitalApplication.Model;
using Model;
using Nancy.Json;
using Newtonsoft.Json;
using WorkWithFiles;

namespace HospitalApplication.Repository
{
    class FileSurveys : IFileSurveys
    {
        private string path = "../../../Data/surveys.json";
        private List<Survey> surveys;
        private static FileSurveys instance;
        public static FileSurveys Instance
        {
            get
            {
                if (null == instance)
                    instance = new FileSurveys();
                return instance;
            }
        }

        private FileSurveys() {
            Read();
        }

        public void Read()
        {
            string json = File.ReadAllText(path);
            surveys = JsonConvert.DeserializeObject<List<Survey>>(json);
        }

        public void Write()
        {
            string json = JsonConvert.SerializeObject(surveys, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public List<Survey> GetSurveys() {
            return surveys;
        }

        public bool IsHospitalSurveyAllowed(string patientsUsername) {
            for (int i = 0; i < surveys.Count; i++){
                if (surveys[i].PatientsUsername == patientsUsername && (surveys[i].DateOfTheSurvey - DateTime.Now).Days < 30 && surveys[i].SurveyIsAbout == "Hospital"){
                    MessageBox.Show("You can rate hospital only once in 30 days.");
                    return false;
                }
            }
            return true;
        }

        public bool IsApplicationSurveyAllowed(string username) {
            for (int i = 0; i < surveys.Count; i++){
                if (surveys[i].PatientsUsername == username && (surveys[i].DateOfTheSurvey - DateTime.Now).Days < 30 && surveys[i].SurveyIsAbout == "Application"){
                    MessageBox.Show("You can rate hospital only once in 30 days.");
                    return false;
                }
            }
            return true;
        }

        public List<string> GetAllowedDoctorsForSurvey(string patientsUsername) {
            FileAppointments fileAppointments = FileAppointments.Instance;
            List<Appointment> appointments = fileAppointments.GetAppointments();
            List<string> doctorUsernames = new List<string>();
            for (int i = 0; i < appointments.Count; i++){
                if (appointments[i].PatientsId == patientsUsername && appointments[i].ExaminationStart < DateTime.Now){
                    bool ok = true;
                    for (int j = 0; j < surveys.Count; j++)
                        if (surveys[j].PatientsUsername == patientsUsername && surveys[j].SurveyIsAbout == appointments[i].DoctorsId && surveys[j].DateOfTheSurvey > appointments[i].ExaminationStart)
                            ok = false;
                    if (ok) doctorUsernames.Add(appointments[i].DoctorsId);
                }
            }
            return doctorUsernames;
        }
    }
}