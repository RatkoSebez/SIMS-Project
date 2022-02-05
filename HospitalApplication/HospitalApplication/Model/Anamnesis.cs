using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalApplication.Model
{
    class Anamnesis
    {
        public string GeneralInformation { get; set; }
        public string MainProblems { get; set; }
        public string CurrentDisease { get; set; }
        public string GeneralProblems { get; set; }
        public string PatientsId { get; set; }
        public string ExaminationId { get; set; }
        public DateTime Date { get; set; }
        public string PatientsComment { get; set; }

        public Anamnesis() { }

        public Anamnesis(string generalInfo, string mainProb, string currDisease, string generalProb, string patientId, string examId, DateTime datte) {
            GeneralInformation = generalInfo;
            MainProblems = mainProb;
            CurrentDisease = currDisease;
            GeneralProblems = generalProb;
            PatientsId = patientId;
            ExaminationId = examId;
            Date = datte;
        }
    }
}