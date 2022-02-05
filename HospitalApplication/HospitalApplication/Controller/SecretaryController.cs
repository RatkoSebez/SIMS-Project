using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using WorkWithFiles;

namespace HospitalApplication.Controller
{
    public class SecretaryController
    {
        public PatientService patientService = new PatientService();
        private FilePatients filePatients = FilePatients.Instance;

        public void CreatePatient(Patient newPatient)
        {
            patientService.CreatePatient(newPatient);
        }

        public void DeletePatient(string iDPatient)
        {
            patientService.DeletePatient(iDPatient);
        }

        public void UpdatePatient(Patient p)
        {
            patientService.UpdatePatient(p);
        }

        public void UpdateMedicalRecord(Patient p)
        {
            patientService.UpdateMedicalRecord(p);
        }

        public Patient GetPatient(string idPatient)
        {
            return filePatients.GetPatient(idPatient);
        }

        public List<Patient> GetPatients()
        {
            return filePatients.GetPatients();
        }
    }
}
