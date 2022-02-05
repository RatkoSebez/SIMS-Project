using HospitalApplication.Model;
using HospitalApplication.Windows.PatientWindows;
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

namespace HospitalApplication.Windows
{
    public partial class AnamnesisInfo : Window
    {
        public AnamnesisInfo()
        {
            InitializeComponent();
            AnamnesisPage anamnesisPage = AnamnesisPage.Instance;
            Anamnesis anamnesis = (Anamnesis)anamnesisPage.lvUsers.SelectedItem;
            GeneralInformation.Text = anamnesis.GeneralInformation;
            MainProblems.Text = anamnesis.MainProblems;
            CurrentDisease.Text = anamnesis.CurrentDisease;
            GeneralProblems.Text = anamnesis.GeneralProblems;
            PatientsComment.Text = anamnesis.PatientsComment;
            PatientId.Text = anamnesis.PatientsId;
            ExaminationId.Text = anamnesis.ExaminationId;
            Date.Text = anamnesis.Date.ToString();
        }
    }
}
