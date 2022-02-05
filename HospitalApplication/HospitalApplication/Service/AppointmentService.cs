using System;
using System.Collections.Generic;
using System.Windows;
using HospitalApplication.Model;
using HospitalApplication.Repository;
using HospitalApplication.Service;
using HospitalApplication.WorkWithFiles;
using Model;
using WorkWithFiles;

namespace Logic
{
    class AppointmentService
    {
        private IFileAppointments fileAppointments = FileAppointments.Instance;
        //private FileAppointments fileAppointments = FileAppointments.Instance;
        private FilePatients filePatients = FilePatients.Instance;
        public List<Appointment> appointments;
        private FileRooms fileRooms = FileRooms.Instance;
        private IFileDoctors fileDoctors = FileDoctors.Instance;
        //private FileDoctors fileDoctors = FileDoctors.Instance;

        public AppointmentService()
        {
            appointments = fileAppointments.GetAppointments();
        }

        public void ScheduleAppointment(Appointment appointment)
        {
            Tuple<bool, int> roomIsFree = fileRooms.IsRoomFree(appointment.ExaminationStart);
            appointment.RoomId = FileRooms.GetRoomId(roomIsFree.Item2);
            Patient patient = filePatients.GetPatientByUsername(appointment.PatientsId);
            if (PenaltyIsGreaterThanAllowed(patient)) {
                MessageBox.Show("You can not schedule examinations anymore. For more information contact us at zdravo@hospital.rs or call 095-5155-622.", "Info");
                return;
            }
            if (fileDoctors.IsDoctorFree(appointment.DoctorsId, appointment.ExaminationStart) == false || roomIsFree.Item1 == false){
                MessageBox.Show("Choosen term is not free. Choose another one.", "Info", MessageBoxButton.OK);
                return;
            }
            filePatients.SetPatientsPenalty(patient, Constants.PENALTY_SCHEDULE);
            fileDoctors.AddAppointmentToDoctor(appointment.DoctorsId, appointment.ExaminationStart);
            fileRooms.AddAppointmentToRoom(roomIsFree.Item2, appointment.ExaminationStart);
            appointments.Add(appointment);
            fileAppointments.Write();
        }

        public void CancelAppointment(Appointment appointment)
        {
            Patient patient = filePatients.GetPatientByUsername(appointment.PatientsId);
            if (PenaltyIsGreaterThanAllowed(patient)){
                MessageBox.Show("You can not cancel examinations anymore. For more information contact us at zdravo@hospital.rs or call 095-5155-622.", "Info");
                return;
            }
            filePatients.SetPatientsPenalty(patient, Constants.PENALTY_CANCEL);
            fileDoctors.RemoveAppointmentFromDoctor(appointment.DoctorsId, appointment.ExaminationStart);
            fileRooms.RemoveAppointmentFromRoom(appointment.RoomId, appointment.ExaminationStart);
            appointments.RemoveAt(fileAppointments.GetAppointmentsIndex(appointment));
            fileAppointments.Write();
        }

        public void EditAppointment(Appointment appointment, string doctorsId)
        {
            Patient patient = filePatients.GetPatientByUsername(appointment.PatientsId);
            if (PenaltyIsGreaterThanAllowed(patient)){
                MessageBox.Show("You can not edit examinations anymore. For more information contact us at zdravo@hospital.rs or call 095-5155-622.", "Info");
                return;
            }
            if (fileDoctors.IsDoctorFree(doctorsId, appointment.ExaminationStart) == false){
                MessageBox.Show("There is no free term. Choose another time.");
                return;
            }
            filePatients.SetPatientsPenalty(patient, Constants.PENALTY_EDIT);
            fileDoctors.RemoveAppointmentFromDoctor(appointment.DoctorsId, appointment.ExaminationStart);
            fileDoctors.AddAppointmentToDoctor(doctorsId, appointment.ExaminationStart);
            appointment.DoctorsId = doctorsId;
            fileAppointments.Write();
        }

        public void MoveAppointment(Appointment appointment, DateTime newDate)
        {
            Tuple<bool, int> roomIsFree = fileRooms.IsRoomFree(appointment.ExaminationStart);
            Patient patient = filePatients.GetPatientByUsername(appointment.PatientsId); 
            if (PenaltyIsGreaterThanAllowed(patient)){
                MessageBox.Show("You can not move examinations anymore. For more information contact us at zdravo@hospital.rs or call 095-5155-622.", "Info");
                return;
            }
            if (fileDoctors.IsDoctorFree(appointment.DoctorsId, newDate) == false || roomIsFree.Item1 == false){
                MessageBox.Show("Choosen term is not free. Choose another one.", "Info", MessageBoxButton.OK);
                return;
            }
            filePatients.SetPatientsPenalty(patient, Constants.PENALTY_MOVE);
            fileDoctors.RemoveAppointmentFromDoctor(appointment.DoctorsId, appointment.ExaminationStart);
            fileDoctors.AddAppointmentToDoctor(appointment.DoctorsId, newDate);
            fileRooms.RemoveAppointmentFromRoom(appointment.RoomId, appointment.ExaminationStart);
            fileRooms.AddAppointmentToRoom(roomIsFree.Item2, newDate);
            appointment.ExaminationStart = newDate;
            appointment.RoomId = FileRooms.GetRoomId(roomIsFree.Item2);
            fileAppointments.Write();
        }

        private bool PenaltyIsGreaterThanAllowed(Patient patient)
        {
            return patient.Penalty.Item3; 
        }
    }
}