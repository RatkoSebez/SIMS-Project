using HospitalApplication.Service;
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

namespace HospitalApplication.Windows.Manager.Renovationn
{
    public partial class AddRenovation : Window
    {
        public AddRenovation()
        {
            InitializeComponent();
        }

        public Random radnomIdRenovation = new Random(DateTime.Now.Ticks.GetHashCode());

        private void Submit_Clicked(object sender, RoutedEventArgs e)
        {
            int roomId = int.Parse(textBoxRoomId.Text);
            List<DateTime> daysBetween = new List<DateTime>();
            DateTime startDayDate = PickStartDate.SelectedDate.Value.Date;
            DateTime endDayDate = PickEndDate.SelectedDate.Value.Date;
            DateTime newDate = new DateTime();

            for (int i=0; i<(endDayDate-startDayDate).TotalDays + 1; i++)
            {
                newDate = startDayDate.Date.AddDays(i);
                daysBetween.Add(newDate);
            }

            Renovation newRenovation = new Renovation()
            {
                RoomId = roomId,
                StartDay = (DateTime)PickStartDate.SelectedDate,
                EndDay = (DateTime)PickEndDate.SelectedDate,
                Days = daysBetween,
                IdRenovation = radnomIdRenovation.Next()
            };

            RenovationsService service = new RenovationsService();
            if (service.CheckRenovation(newRenovation))
            {
                service.AddRenovation(newRenovation);
                Close();
            }
        }
    }
}
