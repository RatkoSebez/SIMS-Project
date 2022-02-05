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

namespace HospitalApplication.Windows.Doctor1
{
    /// <summary>
    /// Interaction logic for DoctorWindow.xaml
    /// </summary>
    public partial class DoctorWindow : Window
    {
        public DoctorWindow()
        {
            InitializeComponent();
        }

        private void btnSchedule_Click(object sender, RoutedEventArgs e)
        {

            ScheduleExaminationWindow window = new ScheduleExaminationWindow();
            window.Show();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            CancelExaminationWindow window = new CancelExaminationWindow();
            window.Show();

        }

        private void btnMove_Click(object sender, RoutedEventArgs e)
        {

            MoveExaminationWindow window = new MoveExaminationWindow();
            window.Show();

        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            ShowExaminationWindow window = new ShowExaminationWindow();
            window.Show();

        }

        private void btnScheduleO_Click(object sender, RoutedEventArgs e)
        {

            ScheduleOperationWindow window = new ScheduleOperationWindow();
            window.Show();

        }

        private void btnCancelO_Click(object sender, RoutedEventArgs e)
        {
            CancelOperationWindow window = new CancelOperationWindow();
            window.Show();

        }

        private void btnMoveO_Click(object sender, RoutedEventArgs e)
        {

            MoveOperationWindow window = new MoveOperationWindow();
            window.Show();

        }

        private void btnShowO_Click(object sender, RoutedEventArgs e)
        {
            ShowOperationWindow window = new ShowOperationWindow();
            window.Show();

        }
    }
}
