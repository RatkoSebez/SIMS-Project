using HospitalApplication.Service.PatientValidation;
using HospitalApplication.Windows.Patient1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace HospitalApplication.Service
{
    public class PatientValidationService
    {
        private IValidateText validateTextStrategy;
        private IValidateDatePicker validateDatePickerStrategy;

        public void SetValidateTextStrategy(IValidateText strategy){
            validateTextStrategy = strategy;
        }

        public void SetValidateDatePickerStrategy(IValidateDatePicker strategy){
            validateDatePickerStrategy = strategy;
        }

        public bool ValidateText(string input){
            return validateTextStrategy.Validate(input);
        }

        public bool ValidateDatePicker(DatePicker datePicker){
            return validateDatePickerStrategy.Validate(datePicker);
        }
    }
}