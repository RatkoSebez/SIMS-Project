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
using HospitalApplication.Model;
using HospitalApplication.Repository;
using WorkWithFiles;

namespace HospitalApplication.Windows.Patient1
{
    public partial class WindowRateHospital : Window
    {
        private List<Survey> surveys;
        private IFileSurveys fileSurveys = FileSurveys.Instance;
        //private FileSurveys fileSurveys = FileSurveys.Instance;
        private MainWindow mainWindow = MainWindow.Instance;
        int[] numericalAnswers = new int[100];

        public WindowRateHospital()
        {
            InitializeComponent();
            setQuestions();
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
            for (int i = 1; i <= Constants.HOSPITAL_SURVEY_QUESTIONS; i++) if (numericalAnswers[i] == 0) isValid = false;
            if (WrittenAnswer.Text.Length == 0) isValid = false;
            if (!isValid){
                Error.Text = "*please answer to all questions";
                return;
            }
            Survey survey = new Survey(numericalAnswers, WrittenAnswer.Text, mainWindow.PatientsUsername, DateTime.Now, "Hospital");
            surveys.Add(survey);
            fileSurveys.Write();
            Close();
        }

        private void setQuestions() {
            Question1.Text = "1. How easy was it to schedule an appointment at our facility?";
            Question2.Text = "2. If you used the patient portal prior to your appointment, was it easy to use?";
            Question3.Text = "3. How easy is it to navigate our facility?";
            Question4.Text = "4. Were we able to answer all your questions?";
            Question5.Text = "5. During last stay at this hospital, how clean was your room kept?";
            Question6.Text = "6. Overall, how hygienic do you feel this hospital is?";
            Question7.Text = "7. How noisy was your room or sleeping place at this hospital?";
            Question8.Text = "8. How well did the meals served at this hospital meet your dietary needs?";
            Question9.Text = "9. How would you rate the professionalism of our staff?";
            Question10.Text = "10. Based on your complete experience with our medical care facility, how likely are you to recommend us to a friend or colleague?";
            Question11.Text ="11. What are the things you feel we should improve upon?";
        }
    }
}