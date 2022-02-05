using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalApplication.Model
{
    class Survey
    {
        public string WrittenAnswer { get; set; }
        public int[] NumericalAnswers { get; set; }
        public string PatientsUsername { get; set; }
        public DateTime DateOfTheSurvey { get; set; }
        public string SurveyIsAbout { get; set; }

        public Survey(int[] numericalAnswers, string writtenAnswer, string patientsUsername, DateTime dateOfTheSurvey, string surveyIsAbout)
        {
            NumericalAnswers = numericalAnswers;
            WrittenAnswer = writtenAnswer;
            PatientsUsername = patientsUsername;
            DateOfTheSurvey = dateOfTheSurvey;
            SurveyIsAbout = surveyIsAbout;
        }
    }
}