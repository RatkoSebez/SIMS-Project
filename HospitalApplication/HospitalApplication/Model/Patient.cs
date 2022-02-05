using System;
using System.Collections.Generic;

namespace Model
{
   public class Patient : Person
   {
        public Appointment examination;
        public MedicalRecord medicalRecord;
        private AccountType typeAcc;
        private List<Allergen> listAllergens;
        public Tuple<int, DateTime, bool> Penalty { get; set; }

        public Patient() { }
        public Patient(AccountType typeAccc, string namee, string lastnamee, string idd, DateTime dateOfBirthh, string phoneNumberr, string emaill, string placeOfResidancee,
            TypeOfPerson typeOfPersonn, string usernameee, string passworddd, string jmbggg, SexType sexTypeee, MedicalRecord mr, List<Allergen> listAllergenss, Tuple<int, DateTime, bool> penalty) : base(namee, lastnamee, idd, dateOfBirthh, phoneNumberr, emaill, placeOfResidancee,
             typeOfPersonn, usernameee, passworddd, jmbggg, sexTypeee)
        {
            typeAcc = typeAccc;
            medicalRecord = mr;
            listAllergens = listAllergenss;
            Penalty = penalty;
        }

        public Model.AccountType TypeAcc
        {
            get { return typeAcc; }
            set { typeAcc = value; }
        }

        public List<Allergen> ListAllergens
        {
            get { return listAllergens; }
            set { listAllergens = value; }
        }
    }
}