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
    public partial class DefineAllergenWindow : Window
    {
        private AllergensService allergenService = new AllergensService();
        private FileAllergens fileAllergens = FileAllergens.Instance;

        public DefineAllergenWindow()
        {
            InitializeComponent();
            CenterWindow();
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
            Allergen newAllergen = new Allergen(GenerateIdForNewAllergen(), textBoxTypeAllergen.Text);
            allergenService.CreateAllergen(newAllergen);
            Close();
        }

        private string GenerateIdForNewAllergen()
        {
            int n = fileAllergens.GetAllergens().Count;
            int idAllergen;
            if (n > 0){
                idAllergen = Int32.Parse(fileAllergens.GetAllergens()[n - 1].Id) + 1;
            }
            else idAllergen = 0;
            return idAllergen.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
