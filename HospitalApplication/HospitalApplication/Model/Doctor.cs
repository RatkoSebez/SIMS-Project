using System;
using System.Collections.Generic;

namespace Model
{
   public class Doctor : Person
   {
        private String doctorId;
        private DoctorType doctorType;
        private List<DateTime> scheduled;

        public Doctor() { }
        public Doctor(DoctorType doctorTypee, List<DateTime> scheduledd, string namee, string lastnamee, string idd, DateTime dateOfBirthh, string phoneNumberr, string emaill, string placeOfResidancee,
                     TypeOfPerson typeOfPersonn, string usernameee, string passworddd, string jmbggg, SexType sexTypeee) : base(namee, lastnamee, idd, dateOfBirthh, phoneNumberr, emaill, placeOfResidancee,
                     typeOfPersonn, usernameee, passworddd, jmbggg, sexTypeee)
        {
            doctorType = doctorTypee;
            scheduled = scheduledd;
        }

        public DoctorType DoctorType
        {
            get { return doctorType; }
            set { doctorType = value; }
        }

        public List<DateTime> Scheduled
        {
            get { return scheduled; }
            set { scheduled = value; }
        }
   }
}