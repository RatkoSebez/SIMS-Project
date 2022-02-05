using HospitalApplication.Controller;
using HospitalApplication.Windows.Manager.Rooms;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WorkWithFiles;

namespace HospitalApplication.Windows.ManagerWindows.Views
{
    /// <summary>
    /// Interaction logic for AllRoomsPage.xaml
    /// </summary>
    public partial class AllRoomsPage : Page
    {
        private FileRooms fileRooms = FileRooms.Instance;
        public AllRoomsPage()
        {
            InitializeComponent();
            lvDataBinding.ItemsSource = fileRooms.ShowAllRooms();
        }

        private void AddRoom_Clicked(object sender, RoutedEventArgs e)
        {
            AddRoom window = new AddRoom();
            window.Show();
        }

        private void Refresh_Clicked(object sender, RoutedEventArgs e)
        {
            List<Room> allRooms = fileRooms.ShowAllRooms();
            lvDataBinding.ItemsSource = allRooms;
        }

        private void Search_Clicked(object sender, RoutedEventArgs e)
        {
            ShowRoomi window = new ShowRoomi();
            window.Show();
        }

        private void Deleted_Clicked(object sender, RoutedEventArgs e)
        {
            ManagerController logic = new ManagerController();
            Room selected = (Room)lvDataBinding.SelectedItem;
            if (selected != null)
                logic.RemoveRoom(selected);
            List<Room> allRooms = fileRooms.ShowAllRooms();
            lvDataBinding.ItemsSource = allRooms;
        }

        private void Edit_Clicked(object sender, RoutedEventArgs e)
        {
            Room selected = (Room)lvDataBinding.SelectedItem;
            if (selected != null)
            {
                EditR window = new EditR(selected);
                window.Show();
            }
        }
    }
}
