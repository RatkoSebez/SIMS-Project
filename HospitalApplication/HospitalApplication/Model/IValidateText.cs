using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalApplication.Service.PatientValidation
{
    public interface IValidateText
    {
        public bool Validate(string input);
    }
}
