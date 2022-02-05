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

namespace HospitalApplication.Windows.Manager.Renovationn
{
    public partial class EditRenovation : Window
    {
        private Renovation editedRenovation;
        public EditRenovation(Renovation selected)
        {
            InitializeComponent();
            textBoxRoomId.Text = selected.RoomId.ToString();
            PickStartDate.SelectedDate = selected.StartDay;
            PickEndDate.SelectedDate = selected.EndDay;
            editedRenovation = selected;
        }

        private void Submit_Clicked(object sender, RoutedEventArgs e)
        {
            Renovation newRenovatioin = new Renovation() {
                RoomId = int.Parse(textBoxRoomId.Text),
                StartDay = (DateTime)PickStartDate.SelectedDate,
                EndDay = (DateTime)PickEndDate.SelectedDate,
                Days = editedRenovation.Days
            };
            RenovationsService service = new RenovationsService();
            service.RemoveRenovation(editedRenovation);
            if (service.CheckRenovation(newRenovatioin))
            {
                service.AddRenovation(newRenovatioin);
                Close();
            }
            else
                service.AddRenovation(editedRenovation);
        }
    }
}
