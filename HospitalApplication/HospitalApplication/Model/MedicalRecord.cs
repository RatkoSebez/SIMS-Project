using System;
using System.Collections.Generic;

namespace Model
{
    public class MedicalRecord
    {
        private string id;
        private AccountType typeAcc;
        private MaritalStatus martialStatus;
        private string firstName;
        private string lastName;
        private string nameParent;
        private string jmbg;
        private DateTime dateOfBirth;
        private string numberOfHealthCard;
        private string placeOfResidance;
        private string phoneNumber;
        private SexType sexType;

        public MedicalRecord() { }
        public MedicalRecord(string idd, AccountType typeAccc, MaritalStatus martialStatuss, string firstNamee, string lastNamee, string nameParentt, string jmbgg, DateTime dateOfBirthh,
                            string numberOfHealthCardd, string placeOfResidancee, string phoneNumberr, SexType sexTypee)
        {
            id = idd;
            typeAcc = typeAccc;
            martialStatus = martialStatuss;
            firstName = firstNamee;
            lastName = lastNamee;
            nameParent = nameParentt;
            jmbg = jmbgg;
            dateOfBirth = dateOfBirthh;
            numberOfHealthCard = numberOfHealthCardd;
            placeOfResidance = placeOfResidancee;
            phoneNumber = phoneNumberr;
            sexType = sexTypee;
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public AccountType TypeAcc
        {
            get { return typeAcc; }
            set { typeAcc = value; }
        }

        public MaritalStatus MartialStatus
        {
            get { return martialStatus; }
            set { martialStatus = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string NameParent
        {
            get { return nameParent; }
            set { nameParent = value; }
        }

        public string Jmbg
        {
            get { return jmbg; }
            set { jmbg = value; }
        }

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        public string NumberOfHealthCard
        {
            get { return numberOfHealthCard; }
            set { numberOfHealthCard = value; }
        }

        public string PlaceOfResidance
        {
            get { return placeOfResidance; }
            set { placeOfResidance = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public SexType SexType
        {
            get { return sexType; }
            set { sexType = value; }
        }
    }
}