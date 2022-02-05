using HospitalApplication.Controller;
using HospitalApplication.Model;
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

namespace HospitalApplication.Windows.Secretary
{
    public partial class ViewNewsWindow : Window
    {
        private News currentSelectedNews;

        public ViewNewsWindow(News selectedNews)
        {
            InitializeComponent();
            CenterWindow();
            currentSelectedNews = selectedNews;
            SetValuesFromNews();
        }

        private void SetValuesFromNews()
        {
            textBoxTypeNews.Text = currentSelectedNews.TypeNews;
            textBoxTitle.Text = currentSelectedNews.Title;
            textBoxDescription.Text = currentSelectedNews.Description;
            textBoxDuration.Text = currentSelectedNews.DurationNews;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CenterWindow()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }
    }
}
