using HospitalApplication.Model;
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

namespace HospitalApplication.Windows.Manager.Medicines
{
    public partial class InfoDrug : Window
    {
        public InfoDrug(Drugs selectedInfo)
        {
            InitializeComponent();
            textBoxName.Text = selectedInfo.Name;
            textBoxQuantity.Text = selectedInfo.Manufacturer;
            textBoxMultiline.Text = selectedInfo.Ingredients;
            textBoxReplacement.Text = selectedInfo.Replacement;
        }

        private void Ok_Clicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
