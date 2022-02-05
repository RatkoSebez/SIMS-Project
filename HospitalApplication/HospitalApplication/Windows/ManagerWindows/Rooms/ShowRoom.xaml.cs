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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkWithFiles;

namespace HospitalApplication.Windows.Manager.Rooms
{
    public partial class ShowRoomi : Window
    {
        private int roomId;
        private FileRooms fileRooms = FileRooms.Instance;
        public ShowRoomi()
        {
            InitializeComponent();
        }

        private void Submit_Clicked(object sender, RoutedEventArgs e)
        {
            roomId = int.Parse(forShw.Text);
            Room roomForShow = fileRooms.GetRoom(roomId);
            forShw.Text = String.Empty;

            string message = "     Room type: " + roomForShow.RoomType + "        Capacity: " + roomForShow.Capacity + "        Number of floors: " +
                roomForShow.NumberOfFloors + "        Occupied: " + roomForShow.Occupied + "        Room id: " + roomForShow.RoomId +
                "        Room number: " + roomForShow.RoomNumber  + "     ";

            fallback.Text = message;
        }
    }
}
