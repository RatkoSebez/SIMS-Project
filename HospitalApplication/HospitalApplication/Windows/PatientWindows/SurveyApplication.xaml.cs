using HospitalApplication.Model;
using HospitalApplication.Repository;
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
    public partial class SurveyApplication : Window
    {
        private List<Survey> surveys = new List<Survey>();
        private IFileSurveys fileSurveys = FileSurveys.Instance;
        //private FileSurveys fileSurveys = FileSurveys.Instance;
        private MainWindow mainWindow = MainWindow.Instance;
        private int[] numericalAnswers = new int[100];

        public SurveyApplication()
        {
            InitializeComponent();
            surveys = fileSurveys.GetSurveys();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            int key = (Int32.Parse(radioButton.Name.ToString().Remove(0, 11)) + 4) / 5;
            int value = Int32.Parse(radioButton.Content.ToString());
            numericalAnswers[key] = value;
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true;
            if (numericalAnswers[1] == 0) isValid = false;
            if (WrittenAnswer.Text.Length == 0) isValid = false;
            if (!isValid){
                Error.Text = "*please fill all fields";
                return;
            }
            Survey survey = new Survey(numericalAnswers, WrittenAnswer.Text, mainWindow.PatientsUsername, DateTime.Now, "Application");
            surveys.Add(survey);
            fileSurveys.Write();
            Close();
        }
    }
}