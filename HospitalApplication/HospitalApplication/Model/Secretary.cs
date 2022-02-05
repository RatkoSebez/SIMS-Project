using System;

namespace Model
{
   public class Secretary : Person
   {
        public Secretary() { }
        public Secretary(string namee, string lastnamee, string idd, DateTime dateOfBirthh, string phoneNumberr, string emaill, string placeOfResidancee,
            TypeOfPerson typeOfPersonn, string usernameee, string passworddd, string jmbggg, SexType sexTypeee) : base(namee, lastnamee, idd, dateOfBirthh, phoneNumberr, emaill, placeOfResidancee,
             typeOfPersonn, usernameee, passworddd, jmbggg, sexTypeee)
        { }
    }
}