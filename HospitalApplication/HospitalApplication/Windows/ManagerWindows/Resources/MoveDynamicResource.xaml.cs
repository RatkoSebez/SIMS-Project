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
    public partial class MoveDynamicResource : Window
    {
        private Resource resourceForMove;
        public MoveDynamicResource(Resource oldResource)
        {
            InitializeComponent();
            textBoxName.Text = oldResource.name;
            textBoxQuantity.Text = oldResource.quantity.ToString();
            resourceForMove = oldResource;
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Submit_Clicked(object sender, RoutedEventArgs e)
        {
            Resource newResource = new Resource();
            newResource.roomId = int.Parse(textBoxRoomId.Text);
            newResource.name = resourceForMove.name;
            newResource.isStatic = resourceForMove.isStatic;
            newResource.manufacturer = resourceForMove.manufacturer;
            newResource.idItem = resourceForMove.idItem;

            ManagerController mc = new ManagerController();

            if (int.Parse(textBoxQuantity.Text) > int.Parse(textBoxKolicina.Text)) 
            {
                newResource.quantity = int.Parse(textBoxKolicina.Text);
                mc.TransferDynamicItem(newResource, int.Parse(textBoxKolicina.Text));
                mc.RemoveQuantity(resourceForMove, int.Parse(textBoxKolicina.Text));

                if (resourceForMove.quantity == 0)
                    mc.RemoveItem(resourceForMove);
                Close();
            }
            else
                MessageBox.Show(" You don't have that many resources " );
        }
    }
}
