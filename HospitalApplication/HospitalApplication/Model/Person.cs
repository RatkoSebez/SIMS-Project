/***********************************************************************
 * Module:  KartonPacijenta.cs
 * Author:  korisnik
 * Purpose: Definition of the Class Ljekar.KartonPacijenta
 ***********************************************************************/

using System;

namespace Model
{
   public class Person : User
   {
        private string name;
        private string lastName;
        private string id;
        private DateTime dateOfBirth;
        private string phoneNumber;
        private string email;
        private string placeOfResidance;
        private TypeOfPerson typeOfPerson;
        private SexType sexType;
        string jmbg;

        public Person() { }
        public Person(string namee, string lastnamee, string idd, DateTime dateOfBirthh, string phoneNumberr, string emaill, string placeOfResidancee,
            TypeOfPerson typeOfPersonn, string usernameee, string passworddd, string jmbgg, SexType sexTypee ) : base(usernameee, passworddd)
        {
            name = namee;
            lastName = lastnamee;
            id = idd;
            dateOfBirth = dateOfBirthh;
            phoneNumber = phoneNumberr;
            email = emaill;
            placeOfResidance = placeOfResidancee;
            typeOfPerson = typeOfPersonn;
            jmbg = jmbgg;
            sexType = sexTypee;
        }

        public SexType SexType
        {
            get { return sexType; }
            set { sexType = value; }
        }

        public string Jmbg
        {
            get { return jmbg; }
            set { jmbg = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string PlaceOfResidance
        {
            get { return placeOfResidance; }
            set { placeOfResidance = value; }
        }

        public TypeOfPerson TypeOfPerson
        {
            get { return typeOfPerson; }
            set { typeOfPerson = value; }
        }
    }
}