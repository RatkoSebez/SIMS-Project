using HospitalApplication.Model;
using HospitalApplication.Repository;
using Model;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WorkWithFiles
{
   public class FilePatients : IFile
   {
        private static string path = "../../../Data/patients.json";
        private static List<Patient> patients;
        private static FilePatients instance;
        public static FilePatients Instance
        {
            get
            {
                if (null == instance)
                    instance = new FilePatients();
                return instance;
            }
        }

        private FilePatients()
        {
            Read();
        }

        public void Read()
        {
            string json = File.ReadAllText(path);
            patients = new JavaScriptSerializer().Deserialize<List<Patient>>(json);
        }

        public void Write()
        {
            string json = new JavaScriptSerializer().Serialize(patients);
            File.WriteAllText(path, json);
        }

        public List<Patient> GetPatients()
        {
            return patients;
        }

        public Patient GetPatient(string iDPatient)
        {
            Patient p = new Patient();
            for (int i = 0; i < patients.Count; i++)
            {
                if (patients[i].Id == iDPatient){
                    p = patients[i]; break;
                }
            }
            return p;
        }

        public Patient GetPatientByUsername(string patientsUsername)
        {
            for (int i = 0; i < patients.Count; i++)
                if (patients[i].Username == patientsUsername) return patients[i];
            return null;
        }

        public void SetPatientsPenalty(Patient patient, int earnedPenalty)
        {
            int currentPenalty = patient.Penalty.Item1 + earnedPenalty;
            DateTime dateOfLastActivity = patient.Penalty.Item2;
            bool isPenaltyGreaterThanAllowed = patient.Penalty.Item3;
            currentPenalty = Math.Max(0, currentPenalty - (int)(DateTime.Now - dateOfLastActivity).TotalDays * Constants.SUBSTRACT_PENALTY_EVERY_DAY);
            dateOfLastActivity = DateTime.Now;
            if (currentPenalty > Constants.MAX_ALLOWED_PENALTY) isPenaltyGreaterThanAllowed = true;
            patient.Penalty = new Tuple<int, DateTime, bool>(currentPenalty, dateOfLastActivity, isPenaltyGreaterThanAllowed);
            Write();
        }
    }
}