using HospitalApplication.Repository;
using HospitalApplication.WorkWithFiles;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalApplication.Service
{
    public class DoctorService
    {
        private List<Doctor> doctors;
        private IFileDoctors fileDoctors = FileDoctors.Instance;
        //private FileDoctors fileDoctors = FileDoctors.Instance;

        public DoctorService() {
            doctors = fileDoctors.GetDoctors();
        }

        public void CreateDoctor(Doctor newDoctor)
        {
            doctors.Add(newDoctor);
            fileDoctors.Write();
        }

        public void DeleteDoctor(string idDoctor)
        {
            for (int i = 0; i < doctors.Count; i++)
            {
                if (doctors[i].Id == idDoctor){
                    doctors.RemoveAt(i); break;
                }
            }
            fileDoctors.Write();
        }

        public void UpdateDoctor(Doctor currentDoctor)
        {
            for (int i = 0; i < doctors.Count; i++)
            {
                if (doctors[i].Id.Equals(currentDoctor.Id))
                {
                    doctors[i].DoctorType = currentDoctor.DoctorType;
                    doctors[i].Name = currentDoctor.Name;
                    doctors[i].LastName = currentDoctor.LastName;
                    doctors[i].Jmbg = currentDoctor.Jmbg;
                    doctors[i].SexType = currentDoctor.SexType;
                    doctors[i].DateOfBirth = currentDoctor.DateOfBirth;
                    doctors[i].PlaceOfResidance = currentDoctor.PlaceOfResidance;
                    doctors[i].Email = currentDoctor.Email;
                    doctors[i].PhoneNumber = currentDoctor.PhoneNumber;
                    doctors[i].Username = currentDoctor.Username;
                    doctors[i].Password = currentDoctor.Password;
                    break;
                }
            }
            fileDoctors.Write();
        }
    }
}