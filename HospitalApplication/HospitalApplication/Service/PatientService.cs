using System;
using System.Collections.Generic;
using System.Windows;
using HospitalApplication.Repository;
using Model;
using WorkWithFiles;

namespace Logic
{
   public class PatientService
   {
        private List<Patient> patients;
        private List<Appointment> appointments;
        private IFileAppointments fileAppointments = FileAppointments.Instance;
        //private FileAppointments fileAppointments = FileAppointments.Instance;
        private FilePatients filePatients = FilePatients.Instance;

        public PatientService()
        {
            patients = filePatients.GetPatients();
            appointments = fileAppointments.GetAppointments();
        }

        public void CreatePatient(Patient newPatient)
        {
            patients.Add(newPatient);
            filePatients.Write();
        }

        public void DeletePatient(string iDPatient)
        {
            string patientsUsername = "";
            for (int i = 0; i < patients.Count; i++)
            {
                if (patients[i].Id.Equals(iDPatient)){
                    patientsUsername = patients[i].Username;
                    patients.RemoveAt(i); break;
                }
            }
            DeleteAppointmentsFromPatient(patientsUsername);
            fileAppointments.Write();
            filePatients.Write();
        }

        private void DeleteAppointmentsFromPatient(string patientsUsername)
        {
            for (int i = 0; i < appointments.Count; i++)
                if (appointments[i].PatientsId.Equals(patientsUsername)) appointments.RemoveAt(i);
        }

        public void UpdatePatient(Patient p)
        {
            for (int i = 0; i < patients.Count; i++)
            {
                if (patients[i].Id.Equals(p.Id)){
                    patients[i].TypeAcc = p.TypeAcc;
                    patients[i].Name = p.Name;
                    patients[i].LastName = p.LastName;
                    patients[i].Jmbg = p.Jmbg;
                    patients[i].SexType = p.SexType;
                    patients[i].DateOfBirth = p.DateOfBirth;
                    patients[i].PlaceOfResidance = p.PlaceOfResidance;
                    patients[i].Email = p.Email;
                    patients[i].PhoneNumber = p.PhoneNumber;
                    patients[i].Username = p.Username;
                    patients[i].Password = p.Password;
                    patients[i].medicalRecord.TypeAcc = p.TypeAcc;
                    patients[i].medicalRecord.FirstName = p.Name;
                    patients[i].medicalRecord.LastName = p.LastName;
                    patients[i].medicalRecord.Jmbg = p.Jmbg;
                    patients[i].medicalRecord.SexType = p.SexType;
                    patients[i].medicalRecord.DateOfBirth = p.DateOfBirth;
                    patients[i].medicalRecord.PlaceOfResidance = p.PlaceOfResidance;
                    patients[i].medicalRecord.PhoneNumber = p.PhoneNumber;
                }
            }
            filePatients.Write();
        }

        public void UpdateMedicalRecord(Patient p)
        {
            for (int i = 0; i < patients.Count; i++)
            {
                if (patients[i].Id.Equals(p.Id)){
                    patients[i].medicalRecord.TypeAcc = p.medicalRecord.TypeAcc;
                    patients[i].medicalRecord.MartialStatus = p.medicalRecord.MartialStatus;
                    patients[i].medicalRecord.FirstName = p.medicalRecord.FirstName;
                    patients[i].medicalRecord.NameParent = p.medicalRecord.NameParent;
                    patients[i].medicalRecord.LastName = p.medicalRecord.LastName;
                    patients[i].medicalRecord.Jmbg = p.medicalRecord.Jmbg;
                    patients[i].medicalRecord.SexType = p.medicalRecord.SexType;
                    patients[i].medicalRecord.DateOfBirth = p.medicalRecord.DateOfBirth;
                    patients[i].medicalRecord.NumberOfHealthCard = p.medicalRecord.NumberOfHealthCard;
                    patients[i].medicalRecord.PlaceOfResidance = p.medicalRecord.PlaceOfResidance;
                    patients[i].medicalRecord.PhoneNumber = p.medicalRecord.PhoneNumber;
                    patients[i].TypeAcc = p.medicalRecord.TypeAcc;
                    patients[i].Name = p.medicalRecord.FirstName;
                    patients[i].LastName = p.medicalRecord.LastName;
                    patients[i].Jmbg = p.medicalRecord.Jmbg;
                    patients[i].SexType = p.medicalRecord.SexType;
                    patients[i].DateOfBirth = p.medicalRecord.DateOfBirth;
                    patients[i].PlaceOfResidance = p.medicalRecord.PlaceOfResidance;
                    patients[i].PhoneNumber = p.medicalRecord.PhoneNumber;
                }
            }
            filePatients.Write();
        }
    }
}