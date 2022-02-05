using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace HospitalApplication.Service.PatientValidation
{
    class TxtOnlyNumbers : IValidateText
    {
        public bool Validate(string input)
        {
            for (int i = 0; i < input.Length; i++) {
                if (input[i] < '0' || input[i] > '9') return false;
            }
            return true;
        }
    }
}
