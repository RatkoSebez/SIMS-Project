using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalApplication.Service.PatientValidation
{
    class TxtNotificationTitle : IValidateText
    {
        public bool Validate(string input)
        {
            return input.Length <= 40;
        }
    }
}
