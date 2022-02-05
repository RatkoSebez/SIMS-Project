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
    public partial class AddResource : Window
    {
        private int idRoomTo;
        private Random RandomIdItem = new Random(DateTime.Now.Ticks.GetHashCode());

        public AddResource(int idRoom)
        {
            InitializeComponent();
            idRoomTo = idRoom;
        }

        private void Submit_Clicked(object sender, RoutedEventArgs e)
        {
            Resource newResource = new Resource()
            {
                idItem = RandomIdItem.Next(1, 999999),
                name = textBoxName.Text,
                quantity = int.Parse(textBoxQuantity.Text),
                isStatic = (bool)checkBoxIsStatic.IsChecked,
                manufacturer = textBoxManufacturer.Text,
                roomId = idRoomTo
            };
            ManagerController logic = new ManagerController();
            logic.AddItem(newResource);
            Close();
        }
    }
}
