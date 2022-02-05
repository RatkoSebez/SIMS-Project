using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalApplication.Service.PatientValidation
{
    class TxtNotEmpty : IValidateText
    {
        public bool Validate(string input)
        {
            if (input.Length < 1) return false;
            return true;
        }
    }
}
