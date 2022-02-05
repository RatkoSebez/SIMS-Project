using HospitalApplication.Model;
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

namespace HospitalApplication.Windows.Manager.Resources
{
    public partial class Resources : Window
    {
        private int idRoomWithResource;
        private bool isStatic;
        private FileRooms fileRooms = FileRooms.Instance;
        public Resources(int idRoom)
        {
            InitializeComponent();
            idRoomWithResource = idRoom;
            RoomService service = new RoomService();
            Room selectedRoom = fileRooms.GetRoom(idRoom);
            service.DeleteResourceIfZero(selectedRoom);
            lvDataBinding.ItemsSource = selectedRoom.Resource;
        }

        private void Refresh_Clicked(object sender, RoutedEventArgs e)
        {
            RoomService service = new RoomService();
            Room selectedRoom = fileRooms.GetRoom(idRoomWithResource);
            service.DeleteResourceIfZero(selectedRoom);
            lvDataBinding.ItemsSource = selectedRoom.Resource;
        }

        private void AddItem_Clicked(object sender, RoutedEventArgs e)
        {
            AddResource window = new AddResource(idRoomWithResource);
            window.Show();
        }

        private void Delete_Clicked(object sender, RoutedEventArgs e)
        {
            RoomService service = new RoomService();
            Resource selected = (Resource)lvDataBinding.SelectedItem;
            if (selected != null)
                service.RemoveResource(selected);
            Room selectedRoom = fileRooms.GetRoom(idRoomWithResource);
            lvDataBinding.ItemsSource = selectedRoom.Resource;
        }

        private void EditResource_Clicked(object sender, RoutedEventArgs e)
        {
            Resource selected = (Resource)lvDataBinding.SelectedItem;
            if (selected != null)
            {
                EditResource window = new EditResource(selected);
                window.Show();
            }
        }

        private void Static_Clicked(object sender, RoutedEventArgs e)
        {
            Room selectedRoom = fileRooms.GetRoom(idRoomWithResource);
            List<Resource> allStaticResource = new List<Resource>();
            for(int i = 0; i < selectedRoom.Resource.Count; i++)
            {
                if(selectedRoom.Resource[i].isStatic == true)
                    allStaticResource.Add(selectedRoom.Resource[i]);
            }
            lvDataBinding.ItemsSource = allStaticResource;
            isStatic = true;
        }

        private void Dynamic_Clicked(object sender, RoutedEventArgs e)
        {
            Room selectedRoom = fileRooms.GetRoom(idRoomWithResource);
            List<Resource> allDynamicResource = new List<Resource>();
            for (int i = 0; i < selectedRoom.Resource.Count; i++)
            {
                if (selectedRoom.Resource[i].isStatic != true)
                    allDynamicResource.Add(selectedRoom.Resource[i]);
            }
            lvDataBinding.ItemsSource = allDynamicResource;
            isStatic = false;
        }

        private void Switching_Clicked(object sender, RoutedEventArgs e)
        {
            Resource selected = (Resource)lvDataBinding.SelectedItem;
            if (selected.isStatic == true)
            {
                MoveStaticResource window = new MoveStaticResource(selected);
                window.Show();
            }
            else
            {
                MoveDynamicResource window = new MoveDynamicResource(selected);
                window.Show();
            }
        }
    }
}
