using HospitalApplication.Service.PatientValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalApplication.Windows.PatientWindows.Validation.ValidateText
{
    class TxtRepeatNotification : IValidateText
    {
        public bool Validate(string input)
        {
            return input.Length <= 3;
        }
    }
}
