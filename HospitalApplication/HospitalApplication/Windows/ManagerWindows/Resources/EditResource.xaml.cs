using HospitalApplication.Controller;
using HospitalApplication.Model;
using Logic;
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

namespace HospitalApplication.Windows.Manager.Resources
{
    public partial class EditResource : Window
    {
        private Resource oldResourceAtributes;
        public EditResource(Resource oldResource)
        {
            InitializeComponent();
            textBoxName.Text = oldResource.name;
            textBoxQuantity.Text = oldResource.quantity.ToString();
            checkBoxIsStatic.IsChecked = (bool)oldResource.isStatic;
            textBoxManufacturer.Text = oldResource.manufacturer;
            oldResourceAtributes = oldResource;
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Submit_Clicked(object sender, RoutedEventArgs e)
        {
            Resource newResource = new Resource() {
                idItem = oldResourceAtributes.idItem,
                name = textBoxName.Text,
                quantity = int.Parse(textBoxQuantity.Text),
                isStatic = (bool)checkBoxIsStatic.IsChecked,
                manufacturer = textBoxManufacturer.Text,
                roomId = oldResourceAtributes.roomId
            };
            ManagerController logic = new ManagerController();
            logic.RemoveItem(oldResourceAtributes);
            logic.AddItem(newResource);
            Close();
        }
    }
}
