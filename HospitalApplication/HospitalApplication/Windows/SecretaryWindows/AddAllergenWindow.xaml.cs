using HospitalApplication.Controller;
using Logic;
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
using WorkWithFiles;

namespace HospitalApplication.Windows.Secretary
{
    public partial class AddAllergenWindow : Window
    {
        private Patient patient;
        private SecretaryController secretaryController = new SecretaryController();
        private PatientService patientService = new PatientService();
        private AllergensService allergensService = new AllergensService();
        private FileAllergens fileAllergens = FileAllergens.Instance;

        public AddAllergenWindow(string idPatient)
        {
            InitializeComponent();
            CenterWindow();
            patient = secretaryController.GetPatient(idPatient);
            AddExistingTypeAllergensInComboBox();
        }

        private void AddExistingTypeAllergensInComboBox()
        {
            foreach (var item in fileAllergens.GetAllergens()){
                ComboBox1.Items.Add(item.Name);
            }
        }

        private void CenterWindow()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Allergen newAlergen = new Allergen(fileAllergens.GetIdAllergen(ComboBox1.Text), ComboBox1.Text, textBoxTypeAllergen.Text);
            patient.ListAllergens.Add(newAlergen);
            allergensService.UpdateAllergen(patient);
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
