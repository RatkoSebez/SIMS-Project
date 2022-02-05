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
    public partial class SearchedResource : Window
    {
        public SearchedResource(string resourceName, int quantity)
        {
            InitializeComponent();
            RoomService service = new RoomService();
            List<Resource> satisfactory = service.AddResourceToSatisfiesList(resourceName, quantity);
            lvDataBinding.ItemsSource = satisfactory;
        }
    }
}
