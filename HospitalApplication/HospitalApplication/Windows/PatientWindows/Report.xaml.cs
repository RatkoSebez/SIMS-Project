using HospitalApplication.Repository;
using HospitalApplication.Service;
using HospitalApplication.Service.PatientValidation;
using HospitalApplication.Service.PatientValidation.ValidateDatePicker;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using Paragraph = iTextSharp.text.Paragraph;

namespace HospitalApplication.Windows.PatientWindows
{
    public partial class Report : Window
    {
        private MainWindow mainWindow = MainWindow.Instance;
        private IFileAppointments fileAppointments = FileAppointments.Instance;
        //private FileAppointments fileAppointments = FileAppointments.Instance;
        private PatientValidationService validationService = new PatientValidationService();
        private bool isFirstDateValid = true;
        private bool isSecondDateValid = true;
        private Brush defaultBorderBrush;

        public Report()
        {
            InitializeComponent();
            defaultBorderBrush = FirstDate.BorderBrush;
        }

        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            if (Validate() == false){
                if (!isFirstDateValid) FirstDate.BorderBrush = Brushes.Red;
                else FirstDate.BorderBrush = defaultBorderBrush;
                if (!isSecondDateValid) SecondDate.BorderBrush = Brushes.Red;
                else SecondDate.BorderBrush = defaultBorderBrush;
                Error.Text = "*please select both dates";
                return;
            }
            DateTime firstDate = FirstDate.SelectedDate.Value.Date;
            DateTime secondDate = SecondDate.SelectedDate.Value.Date;
            List<Appointment> appointments = fileAppointments.GetAppointments(firstDate, secondDate, mainWindow.PatientsUsername);
            try
            {
                Document pdfDoc = new Document(PageSize.A4, 40f, 40f, 60f, 60f);
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Pdf Files|*.pdf";
                save.FileName = "ZdravoReport.pdf";
                save.ShowDialog();
                PdfWriter.GetInstance(pdfDoc, new FileStream(save.FileName, FileMode.OpenOrCreate));

                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font headerFont = new iTextSharp.text.Font(bf, 32, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font tableFont = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
                Paragraph par = new Paragraph("Appointments report", headerFont);
                par.Alignment = Element.ALIGN_CENTER;
                Paragraph spacer = new Paragraph("") { SpacingBefore = 10f, SpacingAfter = 10f };
                

                PdfPTable headerTablee = new PdfPTable(new[] { .75f, 2f })
                {
                    HorizontalAlignment = (int)Left,
                    WidthPercentage = 75,
                    DefaultCell = { MinimumHeight = 22f }
                };
                SetHeaderTable(headerTablee, firstDate, secondDate);

                PdfPTable contentTable = new PdfPTable(4)
                {
                    HorizontalAlignment = (int)Left,
                    WidthPercentage = 100,
                    DefaultCell = { MinimumHeight = 22f },
                };
                SetContentTable(contentTable, appointments, tableFont);

                pdfDoc.Open();
                pdfDoc.Add(par);
                pdfDoc.Add(spacer);
                pdfDoc.Add(spacer);
                pdfDoc.Add(headerTablee);
                pdfDoc.Add(spacer);
                pdfDoc.Add(contentTable);
                pdfDoc.Close();
            }
            catch (Exception ex) {
                MessageBox.Show("Generating document error.");
            }
            Close();
        }

        private void StyleCell(PdfPCell cell, bool isOdd) {
            cell.BorderWidth = 1;
            //cell.BorderColor = new iTextSharp.text.BaseColor(1, 69, 70);
            if(isOdd) cell.BackgroundColor = new iTextSharp.text.BaseColor(231, 231, 231);
        }

        private void SetHeaderTable(PdfPTable headerTable, DateTime firstDate, DateTime secondDate) {
            PdfPCell cell1 = new PdfPCell(new Phrase("Document creation"));
            PdfPCell cell2 = new PdfPCell(new Phrase(DateTime.Now.ToString()));
            PdfPCell cell3 = new PdfPCell(new Phrase("Patient"));
            PdfPCell cell4 = new PdfPCell(new Phrase(mainWindow.PatientsUsername));
            PdfPCell cell5 = new PdfPCell(new Phrase("From"));
            PdfPCell cell6 = new PdfPCell(new Phrase(firstDate.ToString()));
            PdfPCell cell7 = new PdfPCell(new Phrase("To"));
            PdfPCell cell8 = new PdfPCell(new Phrase(secondDate.ToString()));
            PdfPCell cell9 = new PdfPCell(new Phrase("Email"));
            PdfPCell cell10 = new PdfPCell(new Phrase("zdravohospital@zdravo.com"));
            PdfPCell cell11 = new PdfPCell(new Phrase("Contact"));
            PdfPCell cell12 = new PdfPCell(new Phrase("+381 06912015664"));
            StyleCell(cell1, false);
            StyleCell(cell2, false);
            StyleCell(cell3, false);
            StyleCell(cell4, false);
            StyleCell(cell5, false);
            StyleCell(cell6, false);
            StyleCell(cell7, false);
            StyleCell(cell8, false);
            StyleCell(cell9, false);
            StyleCell(cell10, false);
            StyleCell(cell11, false);
            StyleCell(cell12, false);
            headerTable.AddCell(cell1);
            headerTable.AddCell(cell2);
            headerTable.AddCell(cell3);
            headerTable.AddCell(cell4);
            headerTable.AddCell(cell5);
            headerTable.AddCell(cell6);
            headerTable.AddCell(cell7);
            headerTable.AddCell(cell8);
            headerTable.AddCell(cell9);
            headerTable.AddCell(cell10);
            headerTable.AddCell(cell11);
            headerTable.AddCell(cell12);
        }

        private void SetContentTable(PdfPTable contentTable, List<Appointment> appointments, iTextSharp.text.Font tableFont) {
            PdfPCell cellId = new PdfPCell(new Phrase("ID"));
            PdfPCell cellDoctor = new PdfPCell(new Phrase("Doctor"));
            PdfPCell cellStartTime = new PdfPCell(new Phrase("Start time"));
            PdfPCell cellRoom = new PdfPCell(new Phrase("Room"));
            StyleCell(cellId, false);
            StyleCell(cellDoctor, false);
            StyleCell(cellStartTime, false);
            StyleCell(cellRoom, false);
            contentTable.AddCell(cellId);
            contentTable.AddCell(cellDoctor);
            contentTable.AddCell(cellStartTime);
            contentTable.AddCell(cellRoom);

            for (int i = 0; i < appointments.Count; i++)
            {
                bool isOdd = i % 2 == 1 ? true : false;
                PdfPCell cell1 = new PdfPCell(new Phrase(appointments[i].ExaminationId, tableFont));
                PdfPCell cell2 = new PdfPCell(new Phrase(appointments[i].DoctorsId, tableFont));
                PdfPCell cell3 = new PdfPCell(new Phrase(appointments[i].ExaminationStart.ToString(), tableFont));
                PdfPCell cell4 = new PdfPCell(new Phrase(appointments[i].RoomId, tableFont));
                StyleCell(cell1, isOdd);
                StyleCell(cell2, isOdd);
                StyleCell(cell3, isOdd);
                StyleCell(cell4, isOdd);
                contentTable.AddCell(cell1);
                contentTable.AddCell(cell2);
                contentTable.AddCell(cell3);
                contentTable.AddCell(cell4);
            }
        }

        private bool Validate()
        {
            bool firstDateValidation = true;
            bool secondDateValidation = true;
            validationService.SetValidateDatePickerStrategy(new DpNotEmpty());
            if (!validationService.ValidateDatePicker(FirstDate)) firstDateValidation = false;
            if (!validationService.ValidateDatePicker(SecondDate)) secondDateValidation = false;
            isFirstDateValid = firstDateValidation;
            isSecondDateValid = secondDateValidation;
            return (isFirstDateValid && isSecondDateValid);
        }
    }
}