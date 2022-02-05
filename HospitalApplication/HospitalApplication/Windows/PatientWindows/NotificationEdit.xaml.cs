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
using HospitalApplication.Controller;
using HospitalApplication.Logic;
using HospitalApplication.Model;
using HospitalApplication.Service;
using HospitalApplication.Service.PatientValidation;
using HospitalApplication.Service.PatientValidation.ValidateDatePicker;
using HospitalApplication.Windows;
using HospitalApplication.Windows.PatientWindows;
using HospitalApplication.Windows.PatientWindows.Validation.ValidateText;

namespace HospitalApplication.Windows.Patient1
{
    public partial class WindowNotificationEdit : Window
    {
        private NotificationsPage notificationsPage = NotificationsPage.Instance;
        private NotificationController controller = new NotificationController();
        private FormService formService = new FormService();
        private PatientValidationService validationService = new PatientValidationService();
        private bool isRepeatValid = true;
        private bool isDescriptionValid = true;
        private bool isTitleValid = true;
        private bool isDateValid = true;
        private Brush defaultBorderBrush;

        public WindowNotificationEdit()
        {
            InitializeComponent();
            defaultBorderBrush = Title.BorderBrush;
            Notification notification = (Notification)notificationsPage.lvUsers.SelectedItem;
            Date.SelectedDate = notification.Dates[0];
            Title.Text = notification.Title;
            Description.Text = notification.Description;
            Repeat.Text = notification.Repeat;
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (Validate() == false){
                if (!isRepeatValid) Repeat.BorderBrush = Brushes.Red;
                else Repeat.BorderBrush = defaultBorderBrush;
                if (!isTitleValid) Title.BorderBrush = Brushes.Red;
                else Title.BorderBrush = defaultBorderBrush;
                if (!isDescriptionValid) Description.BorderBrush = Brushes.Red;
                else Description.BorderBrush = defaultBorderBrush;
                if (!isDateValid) Date.BorderBrush = Brushes.Red;
                else Date.BorderBrush = defaultBorderBrush;
                Error.Text = "*wrong input";
                return;
            }
            Notification notification = (Notification)notificationsPage.lvUsers.SelectedItem;
            DateTime newDate = formService.GetDateAndTimeFromForm(Date.SelectedDate.Value.Date, Combo, 0, 24);
            controller.EditNotification(notification, Title.Text, Description.Text, Repeat.Text, newDate);
            notificationsPage.UpdateView();
            Close();
        }

        private bool Validate(){
            bool repeatValidation = true;
            bool titleValidation = true;
            bool descriptionValidation = true;
            bool dateValidation = true;
            validationService.SetValidateTextStrategy(new TxtOnlyNumbers());
            if (!validationService.ValidateText(Repeat.Text)) repeatValidation = false;
            validationService.SetValidateTextStrategy(new TxtNotEmpty());
            if (!validationService.ValidateText(Description.Text)) descriptionValidation = false;
            if (!validationService.ValidateText(Title.Text)) titleValidation = false;
            if (!validationService.ValidateText(Repeat.Text)) repeatValidation = false;
            validationService.SetValidateTextStrategy(new TxtNotificationTitle());
            if (!validationService.ValidateText(Title.Text)) titleValidation = false;
            validationService.SetValidateTextStrategy(new TxtNotificationDescription());
            if (!validationService.ValidateText(Description.Text)) descriptionValidation = false;
            validationService.SetValidateTextStrategy(new TxtRepeatNotification());
            if (!validationService.ValidateText(Repeat.Text)) repeatValidation = false;
            validationService.SetValidateDatePickerStrategy(new DpNotEmpty());
            if (!validationService.ValidateDatePicker(Date)) dateValidation = false;
            isRepeatValid = repeatValidation;
            isTitleValid = titleValidation;
            isDescriptionValid = descriptionValidation;
            isDateValid = dateValidation;
            return (isDateValid && isRepeatValid && isDescriptionValid && isTitleValid);
        }
    }
}