using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Model;
using Logic;
using Nancy.Json;
using HospitalApplication.Repository;
using Newtonsoft.Json;

namespace HospitalApplication.WorkWithFiles
{
    class FileDoctors : IFileDoctors
    {
        private static string path = "../../../Data/doctors.json";
        private static List<Doctor> doctors;
        private static FileDoctors instance;
        public static FileDoctors Instance
        {
            get
            {
                if (null == instance)
                    instance = new FileDoctors();
                return instance;
            }
        }

        private FileDoctors() {
            Read();
        }

        public void Read()
        {
            string json = File.ReadAllText(path);
            doctors = JsonConvert.DeserializeObject<List<Doctor>>(json);
        }

        public void Write()
        {
            string json = JsonConvert.SerializeObject(doctors, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public List<Doctor> GetDoctors() {
            return doctors;
        }

        public Doctor GetDoctor(string doctorsUsername) {
            for (int i = 0; i < doctors.Count; i++)
                if (doctorsUsername == doctors[i].Username) return doctors[i];
            return null;
        }
        
        public Doctor GetDoctorById(string idDoctor)
        {
            for (int i = 0; i < doctors.Count; i++)
            {
                if (doctors[i].Username.Equals(idDoctor)) return doctors[i];
            }
            return null;
        }

        public bool IsDoctorFree(string doctorsUsername, DateTime date)
        {
            for (int i = 0; i < doctors.Count; i++)
                if (doctors[i].Username == doctorsUsername)
                    for (int j = 0; j < doctors[i].Scheduled.Count; j++)
                        if (doctors[i].Scheduled[j] == date) return false;
            return true;
        }

        public void AddAppointmentToDoctor(string doctorsUsername, DateTime date)
        {
            for (int i = 0; i < doctors.Count; i++)
            {
                if (doctors[i].Username == doctorsUsername)
                {
                    doctors[i].Scheduled.Add(date);
                    Write();
                    break;
                }
            }
        }

        public void RemoveAppointmentFromDoctor(string doctorsUsername, DateTime date)
        {
            for (int i = 0; i < doctors.Count; i++){
                if (doctors[i].Username == doctorsUsername){
                    for (int j = 0; j < doctors[i].Scheduled.Count; j++){
                        if (doctors[i].Scheduled[j] == date){
                            doctors[i].Scheduled.RemoveAt(j);
                            break;
                        }
                    }
                    Write();
                    break;
                }
            }
        }
    }
}