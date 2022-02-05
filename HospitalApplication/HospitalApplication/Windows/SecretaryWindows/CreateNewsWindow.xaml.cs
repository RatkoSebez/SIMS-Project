using HospitalApplication.Controller;
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

namespace HospitalApplication.Windows.Secretary
{
    public partial class CreateNewsWindow : Window
    {
        private NewsController newsController = new NewsController();
        private HomeWindow homeWindow = HomeWindow.Instance;

        public CreateNewsWindow()
        {
            InitializeComponent();
            CenterWindow();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            NewsFactory newsFactory = GetTypeOfNews();
            newsController.CreateNews(newsFactory.GetNews());
            homeWindow.UpdateNews();
            Close();
        }

        private NewsFactory GetTypeOfNews()
        {
            NewsFactory newsFactory = null;
            switch (ComboBox1.Text)
            {
                case "General":
                    newsFactory = new GeneralFactory(IdGenerator(), ComboBox1.Text, textBoxTitle.Text, textBoxDescription.Text, DateTime.Now, textBoxDuration.Text);
                    break;
                case "Discount":
                    newsFactory = new DiscountFactory(IdGenerator(), ComboBox1.Text, textBoxTitle.Text, textBoxDescription.Text, DateTime.Now, textBoxDuration.Text);
                    break;
                case "Important":
                    newsFactory = new ImportantFactory(IdGenerator(), ComboBox1.Text, textBoxTitle.Text, textBoxDescription.Text, DateTime.Now, textBoxDuration.Text);
                    break;
                default:
                    break;
            }

            return newsFactory;
        }

        private string IdGenerator()
        {
            int n = newsController.GetAllNews().Count;
            int idNews;
            if (n > 0){
                idNews = Int32.Parse(newsController.GetAllNews()[n - 1].Id) + 1;
            }
            else idNews = 0;
            return idNews.ToString();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
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
