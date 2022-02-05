using HospitalApplication.Model;
using HospitalApplication.Repository;
using HospitalApplication.Windows.Patient1;
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

namespace HospitalApplication.Windows.PatientWindows
{
    public partial class AnamnesisPage : Page
    {
        private IFileAnamnesis fileAnamnesis = FileAnamnesis.Instance;
        //private FileAnamnesis fileAnamnesis = FileAnamnesis.Instance;
        private List<Anamnesis> anamnesis;
        private List<Anamnesis> filteredAnamnesis = new List<Anamnesis>();
        private MainWindow mainWindow = MainWindow.Instance;

        private static AnamnesisPage instance;
        public static AnamnesisPage Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new AnamnesisPage();
                }
                return instance;
            }
        }

        public AnamnesisPage()
        {
            InitializeComponent();
            instance = this;
            anamnesis = fileAnamnesis.GetAnamnesis(mainWindow.PatientsUsername);
            lvUsers.ItemsSource = anamnesis;
        }

        private void SubmitComment_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvUsers.SelectedIndex > -1)) return;
            Anamnesis anamnesis = (Anamnesis)lvUsers.SelectedItem;
            anamnesis.PatientsComment = AnamnesisComment.Text.ToString();
            fileAnamnesis.Write();
            MessageBox.Show("Anamnesis comment is successfully added.");
        }

        private void AnamneseInformation_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvUsers.SelectedIndex > -1)) return;
            AnamnesisInfo window = new AnamnesisInfo();
            window.ShowDialog();
        }

        private void CustomNotification_Click(object sender, RoutedEventArgs e)
        {
            WindowNotificationMake window = new WindowNotificationMake();
            window.ShowDialog();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            filteredAnamnesis.Clear();
            for (int i = 0; i < anamnesis.Count; i++)
                if (anamnesis[i].ExaminationId.Contains(Search.Text)) filteredAnamnesis.Add(anamnesis[i]);
            lvUsers.ItemsSource = null;
            lvUsers.ItemsSource = filteredAnamnesis;
        }
    }
}