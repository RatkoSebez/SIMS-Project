using HospitalApplication.Repository;
using HospitalApplication.Service;
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

namespace HospitalApplication.Windows.Manager.Medicines
{
    public partial class ReportedDrugs : Window
    {
        private FileReportedDrugs fileReportedDrugs = FileReportedDrugs.Instance;
        public ReportedDrugs()
        {
            InitializeComponent();
            ReportService logic = new ReportService();
            List<Model.ReportedDrugs> reported = fileReportedDrugs.GetAllReports();
            lvDataBinding.ItemsSource = reported;
            Model.ReportedDrugs newReport = new Model.ReportedDrugs(){
                IdReportedItem = 123,
                Manufacturer = "Gelenika",
                Name = "Aspirin",
                Problem = "Nedovoljno vitamina B"
            };
            logic.AddReport(newReport);
        }

        private void Delete_Clicked(object sender, RoutedEventArgs e)
        {
            ReportService logic = new ReportService();
            Model.ReportedDrugs selected = (Model.ReportedDrugs)lvDataBinding.SelectedItem;
            if (selected != null)
                logic.DeleteReport(selected);
            List<Model.ReportedDrugs> reported = fileReportedDrugs.GetAllReports();
            lvDataBinding.ItemsSource = reported;
        }

        private void Refresh_Clicked(object sender, RoutedEventArgs e)
        {
            List<Model.ReportedDrugs> reported = fileReportedDrugs.GetAllReports();
            lvDataBinding.ItemsSource = reported;
        }
    }
}
