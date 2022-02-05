using HospitalApplication.Model;
using HospitalApplication.Repository;
using HospitalApplication.WorkWithFiles;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HospitalApplication.Windows.PatientWindows
{
    public partial class DoctorSurvey : Window
    {
        private List<Survey> surveys = new List<Survey>();
        private IFileSurveys fileSurveys = FileSurveys.Instance;
        //private FileSurveys fileSurveys = FileSurveys.Instance;
        private MainWindow mainWindow = MainWindow.Instance;
        private int[] numericalAnswers = new int[100];

        public DoctorSurvey(List<string> doctorsUsernames)
        {
            InitializeComponent();
            setQuestions();
            for (int i = 0; i < doctorsUsernames.Count; i++)
                ComboDoctors.Items.Add(doctorsUsernames[i]);
            surveys = fileSurveys.GetSurveys();
        }

        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            int key = (Int32.Parse(radioButton.Name.ToString().Remove(0, 11)) + 4) / 5;
            int value = Int32.Parse(radioButton.Content.ToString());
            numericalAnswers[key] = value;
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true;
            for (int i = 1; i <= Constants.DOCTOR_SURVEY_QUESTIONS; i++) if (numericalAnswers[i] == 0) isValid = false;
            if (WrittenAnswer.Text.Length == 0) isValid = false;
            if (!isValid){
                Error.Text = "*please answer to all questions";
                return;
            }
            Survey survey = new Survey(numericalAnswers, WrittenAnswer.Text, mainWindow.PatientsUsername, DateTime.Now, ComboDoctors.SelectedItem.ToString());
            surveys.Add(survey);
            fileSurveys.Write();
            Close();
        }

        private void setQuestions()
        {
            Question1.Text = "1. How happy are you with the doctor’s treatment?";
            Question2.Text = "2. Were the doctor empathetic to your needs?";
            Question3.Text = "3. Were the ambulatory staff quick to respond to your medical care request ?";
            Question4.Text = "4. How often did the doctor describe possible side effects before administering your medicine?";
            Question5.Text = "5. Were you satisfied with the answers provided?";
            Question6.Text = "6. Did doctor listen carefully to you?";
            Question7.Text = "7. How respectfull did the doctor treat you?";
            Question8.Text = "8. Did the doctor and/or medical staff involve you in decisions about your treatment?";
            Question9.Text = "9. Do you believe the doctor is trustworthy?";
            Question10.Text = "10. Would you be happy to see this doctor again?";
            Question11.Text = "11. What are the things you feel doctor should improve upon?";
        }
    }
}