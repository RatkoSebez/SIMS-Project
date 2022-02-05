using HospitalApplication.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Model;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HospitalApplication.Windows.ManagerWindows.Renovations;
using WorkWithFiles;

namespace HospitalApplication.Windows.Manager.Renovationn
{
    public partial class Renovations : Window
    {
        private FileRooms fileRooms = FileRooms.Instance;
        public Renovations()
        {
            InitializeComponent();
            RenovationsService service = new RenovationsService();
            service.DeleteOldRenovations();
            List<Renovation> renovations = fileRooms.ShowAllRenovations();
            lvDataBinding.ItemsSource = renovations;
        }

        private void Delete_Clicked(object sender, RoutedEventArgs e)
        {
            RenovationsService logic = new RenovationsService();
            Renovation selected = (Renovation)lvDataBinding.SelectedItem;
            if(selected != null)
                logic.RemoveRenovation(selected);
            List<Renovation> renovations = fileRooms.ShowAllRenovations();
            lvDataBinding.ItemsSource = renovations;
        }

        private void Add_Clicked(object sender, RoutedEventArgs e)
        {
            ChooseRenovation window = new ChooseRenovation();
            window.Show();
        }

        private void Refresh_Clicked(object sender, RoutedEventArgs e)
        {
            RenovationsService logic = new RenovationsService();
            List<Renovation> renovations = fileRooms.ShowAllRenovations();
            logic.DeleteOldRenovations();
            lvDataBinding.ItemsSource = renovations;
        }

        private void Edit_Clicked(object sender, RoutedEventArgs e)
        {
            Renovation selected = (Renovation)lvDataBinding.SelectedItem;
            if (selected != null)
            {
                EditRenovation window = new EditRenovation(selected);
                window.Show();
            }
        }
    }
}
