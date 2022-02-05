using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace HospitalApplication.Service.PatientValidation.ValidateDatePicker
{
    class DpNotEmpty : IValidateDatePicker
    {
        public bool Validate(DatePicker datePicker)
        {
            if (datePicker.SelectedDate == null) return false;
            return true;
        }
    }
}
