using HospitalApplication.Model;
using HospitalApplication.Repository;
using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WorkWithFiles;

namespace HospitalApplication.Service
{
   public class RenovationsService
    {
        private List<Room> rooms;
        private List<Transfer> transfers;
        private FileRooms fileRooms = FileRooms.Instance;
        private FileScheduledTransfers fileTransfers = FileScheduledTransfers.Instance;

        public RenovationsService()
        {
            rooms = fileRooms.ShowAllRooms();
            transfers = fileTransfers.ShowAllTransfers();
        }

        public void RemoveRenovation(Renovation renovation)
        {
            for(int i=0; i < rooms.Count; i++)
            {
                if(rooms[i].RoomId == renovation.RoomId)
                    DeleteRenovationInRoom(renovation, i);
            }
            fileRooms.Write();
        }

        private void DeleteRenovationInRoom(Renovation renovation, int i)
        {
            for (int j = 0; j < rooms[i].Renovation.Count; j++)
            {
                if (rooms[i].Renovation[j].IdRenovation == renovation.IdRenovation)
                    rooms[i].Renovation.RemoveAt(j);
            }
        }

        public void AddRenovation(Renovation newRenovation)
        {
            for(int i=0; i<rooms.Count; i++)
            {
                if(rooms[i].RoomId == newRenovation.RoomId)
                    rooms[i].Renovation.Add(newRenovation);
            }
            fileRooms.Write();
        }

        public bool CheckRenovation(Renovation newRenovation)
        {
            return (CheckDoesRoomExist(newRenovation) && CheckDoesRoomHaveTransfers(newRenovation) && CheckIsFree(newRenovation));
        }

        private bool CheckDoesRoomHaveTransfers(Renovation newRenovation)
        {
            for (int i = 0; i < transfers.Count; i++)
            {
                if (newRenovation.RoomId == transfers[i].idRoomFrom || newRenovation.RoomId == transfers[i].idRoomTo)
                    return CheckIfSameDaysReserved(newRenovation, i);
            }
            return true;
        }

        private bool CheckIfSameDaysReserved(Renovation newRenovation, int i)
        {
            for (int j = 0; j < newRenovation.Days.Count; j++)
            {
                if (newRenovation.Days[j].Date == transfers[i].date.Date)
                {
                    MessageBox.Show("That room is already busy, relocation of static equipment is scheduled", "Error");
                    return false;
                }
            }
            return true;
        }

        private bool CheckIsFree(Renovation newRenovation)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if(rooms[i].RoomId == newRenovation.RoomId)
                    return CheckDoesNewRenovationExistForSameRoom(newRenovation, i);
            }
            return true;
        }

        private bool CheckDoesRoomExist(Renovation newRenovation)
        {
            int roomExist = 0;
            int roomTwoExist = 0;
            IterateIfExist(newRenovation, ref roomExist, ref roomTwoExist);
            if (roomExist == 0)
            {
                MessageBox.Show("Room one does not exist", "Error");
                return false;
            }
            if (roomTwoExist == 0 && newRenovation.SecondRoomId != 0)
            {
                MessageBox.Show("Room two does not exist", "Error");
                return false;
            }
            return true;
        }

        private void IterateIfExist(Renovation newRenovation, ref int roomExist, ref int roomTwoExist)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].RoomId == newRenovation.RoomId)
                    roomExist++;
                if (rooms[i].RoomId == newRenovation.SecondRoomId)
                    roomTwoExist++;
            }
        }

        private bool CheckDoesNewRenovationExistForSameRoom(Renovation newRenovation, int i)
        {
            for (int j = 0; j < rooms[i].Renovation.Count; j++)
            {
                if (rooms[i].Renovation[j].RoomId == newRenovation.RoomId)
                    return CheckIsThatRoomBussy(newRenovation, i, j);
            }
            return true;
        }

        private bool CheckIsThatRoomBussy(Renovation newRenovation, int i, int j)
        {
            for (int d = 0; d < rooms[i].Renovation[j].Days.Count; d++)
                return CheckWithDaysOfNewRenovation(newRenovation, i, j, d);
            return true;
        }

        private bool CheckWithDaysOfNewRenovation(Renovation newRenovation, int i, int j, int d)
        {
            for (int nd = 0; nd < newRenovation.Days.Count; nd++)
            {
                if (rooms[i].Renovation[j].Days[d].Date == newRenovation.Days[nd].Date)
                {
                    MessageBox.Show("That room is already busy", "Error");
                    return false;
                }
            }
            return true;
        }

        public void DeleteOldRenovations()
        {
            for(int i=0; i<rooms.Count; i++)
                DeleteIfEndDayPass(i);
        }

        private void DeleteIfEndDayPass(int i)
        {
            for (int j = 0; j < rooms[i].Renovation.Count; j++)
            {
                if (rooms[i].Renovation[j].EndDay <= DateTime.Now)
                {
                    if (rooms[i].Renovation[j].SecondRoomId != 0)
                        EditTwoRoomsRenovation(rooms[i].Renovation[j].RoomId, rooms[i].Renovation[j].SecondRoomId);
                    else if (rooms[i].Renovation[j].OneToTwo == true)
                        EditOneToTwoRenovation(rooms[i].Renovation[j].RoomId);
                    else
                        rooms[i].Renovation.RemoveAt(j);
                }
            }
        }

        private void EditOneToTwoRenovation(int roomId)
        {
            RoomService service = new RoomService();
            Room oldRoom = fileRooms.GetRoom(roomId);
            service.RemoveRoom(oldRoom);
            Room newRoomOne = new Room()
            {
                Capacity = 0, NumberOfFloors = 0, Occupied = false,
                RoomId = 0000,
                RoomNumber = 0,
                RoomType = RoomType.Warehouse,
                Renovation = new List<Renovation>(),
                Resource = new List<Model.Resource>(),
                Scheduled = new List<DateTime>()
            };
            Room newRoomTwo = new Room()
            {
                Capacity = 0,
                NumberOfFloors = 0,
                Occupied = false,
                RoomId = 1111,
                RoomNumber = 0,
                RoomType = RoomType.Warehouse,
                Renovation = new List<Renovation>(),
                Resource = new List<Model.Resource>(),
                Scheduled = new List<DateTime>()
            };
            service.CreateRoom(newRoomOne);
            service.CreateRoom(newRoomTwo);
        }

        private void EditTwoRoomsRenovation(int firstRoomId, int secondRoomId)
        {
            RoomService service = new RoomService();
            Room First = fileRooms.GetRoom(firstRoomId);
            Room Second = fileRooms.GetRoom(secondRoomId);
            Room newRoom = new Room()
            {
                Capacity = 0,
                NumberOfFloors = 0,
                Occupied = false,
                RoomId = 0000,
                RoomNumber = 0,
                RoomType = RoomType.Warehouse,
                Renovation = new List<Renovation>(),
                Resource = new List<Model.Resource>(),
                Scheduled = new List<DateTime>()
            };
            service.RemoveRoom(First);
            service.RemoveRoom(Second);
            service.CreateRoom(newRoom);
        }
    }
}
