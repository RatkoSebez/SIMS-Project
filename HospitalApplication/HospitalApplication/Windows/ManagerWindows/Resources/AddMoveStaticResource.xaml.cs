using HospitalApplication.Controller;
using HospitalApplication.Model;
using HospitalApplication.Service;
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

namespace HospitalApplication.Windows.Manager.Resources
{
    public partial class AddMoveStaticResource : Window
    {
        private Resource newResource; 

        public AddMoveStaticResource(Resource forTransfer)
        {
            InitializeComponent();
            newResource = forTransfer;
        }

        private void Calendar(object sender, SelectionChangedEventArgs e)
        {
            selectedDate.Text = PickDate.SelectedDate.ToString();
        }

        public Random randomTransferId = new Random(DateTime.Now.Ticks.GetHashCode());
        private void Submit_Clicked(object sender, RoutedEventArgs e)
        {
            Transfer newTransfer = new Transfer() {
                idTransfer = randomTransferId.Next(),
                idRoomFrom = newResource.roomId,
                idRoomTo = int.Parse(textBoxRoomId.Text),
                date = DateTime.Parse(selectedDate.Text).AddHours(2),
                quantity = int.Parse(textBoxManufacturer.Text)
            };

            if(newTransfer.quantity > newResource.quantity)
                MessageBox.Show("That resource does not have that amount", "Error");
            else
            {
                //newResource.roomId = newTransfer.idRoomTo;
                //newResource.quantity = newTransfer.quantity;
                if (newTransfer.resource == null)
                {
                    newTransfer.resource = new List<Resource>();
                    newTransfer.resource.Add(newResource);
                }
                else
                    newTransfer.resource.Add(newResource); 

                ManagerController logic = new ManagerController();
                RelocationResourceService service = new RelocationResourceService();
                if (service.CheckDoesRoomExist(newTransfer) == true)
                {
                    logic.MakeScheduled(newTransfer);
                    Close();
                }
                else 
                    MessageBox.Show("Room id does not exist", "Error");
            }

        }
    }
}
