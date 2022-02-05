using HospitalApplication.Model;
using HospitalApplication.Service;
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
    public partial class EditDrug : Window
    {
        private Drugs old;

        public EditDrug(Drugs forEdit)
        {
            InitializeComponent();
            textBoxName.Text = forEdit.Name;
            textBoxQuantity.Text = forEdit.Manufacturer;
            textBoxMultiline.Text = forEdit.Ingredients;
            textBoxReplacement.Text = forEdit.Replacement;
            old = forEdit;
        }

        private void Submit_Clicked(object sender, RoutedEventArgs e)
        {
            Drugs Edited = new Drugs
            {
                ItemId = old.ItemId,
                Name = textBoxName.Text,
                Manufacturer = textBoxQuantity.Text,
                Replacement = textBoxReplacement.Text,
                Ingredients = textBoxMultiline.Text
            };
            MedicinesService logic = new MedicinesService();
            logic.DeleteDrug(old);
            logic.CreateDrug(Edited);
            Close();
        }
    }
}
