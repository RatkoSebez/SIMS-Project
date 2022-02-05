 using System;
using System.Collections.Generic;
using System.Windows;
using HospitalApplication.Model;
using HospitalApplication.Repository;
using Model;
using WorkWithFiles;

namespace Logic
{
    public class RoomService
    {
        private List<Room> rooms;
        private List<Appointment> appointments;
        private IFileAppointments filesAppointments = FileAppointments.Instance;
        //private FileAppointments filesAppointments = FileAppointments.Instance;
        private FileRooms fileRooms = FileRooms.Instance;

        public RoomService()
        {
            rooms = fileRooms.ShowAllRooms();
            appointments = filesAppointments.GetAppointments();
        }
        public void CreateRoom(Room newRoom)
        {
            rooms.Add(newRoom);
            fileRooms.Write();
        }

        public void DeleteResourceIfZero(Room roomWithResource)
        {
            for(int i=0; i<rooms.Count; i++)
            {
                if(rooms[i].RoomId == roomWithResource.RoomId)
                    DeleteIt(i);
            }
            fileRooms.Write();
        }

        private void DeleteIt(int i)
        {
            for (int j = 0; j < rooms[i].Resource.Count; j++)
            {
                if (rooms[i].Resource[j].quantity == 0)
                    rooms[i].Resource.RemoveAt(j);
            }
        }

        public List<Resource> AddResourceToSatisfiesList(string resourceName, int quantity)
        {
            List<Resource> fulfills = new List<Resource>();
            for (int i=0; i < rooms.Count; i++)
            {
                for(int j=0; j<rooms[i].Resource.Count; j++)
                    MeetsConditions(resourceName, quantity, fulfills, i, j);
            }
            return fulfills;
        }

        private void MeetsConditions(string resourceName, int quantity, List<Resource> fulfills, int i, int j)
        {
            if (rooms[i].Resource[j].name.Equals(resourceName))
            {
                if (rooms[i].Resource[j].quantity >= quantity)
                    fulfills.Add(rooms[i].Resource[j]);
            }
        }

        public void AddResource(Resource newResourse) 
        {
            for (int i = 0; i < rooms.Count; i++) 
            { 
                if(rooms[i].RoomId == newResourse.roomId)
                {
                    if (rooms[i].Resource == null)
                        rooms[i].Resource = new List<Resource>();
                    rooms[i].Resource.Add(newResourse);
                }
            }
            fileRooms.Write();
        }

        public void TransferDynamicResource(Resource resourceForTransfer, int quantity)
        {
            int roomWithThatResource = 0;
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].RoomId == resourceForTransfer.roomId)
                {
                    roomWithThatResource = Transfer(resourceForTransfer, quantity, roomWithThatResource, i);
                    if (roomWithThatResource == 0)
                        rooms[i].Resource.Add(resourceForTransfer);
                }
            }
            fileRooms.Write();
        }

        private int Transfer(Resource resourceForTransfer, int quantity, int roomWithThatResource, int i)
        {
            for (int j = 0; j < rooms[i].Resource.Count; j++)
            {
                if (rooms[i].Resource[j].idItem == resourceForTransfer.idItem)
                {
                    rooms[i].Resource[j].quantity += quantity;
                    roomWithThatResource += 1;
                }
            }
            return roomWithThatResource;
        }

        public void RemoveRoom(Model.Room oldRoom)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].RoomId == oldRoom.RoomId)
                {
                    rooms.RemoveAt(i);
                    break;
                }
            }
            for (int i = 0; i < appointments.Count; i++)
                if (appointments[i].RoomId == oldRoom.RoomId.ToString()) appointments.RemoveAt(i);
            filesAppointments.Write();
            fileRooms.Write();
        }

        public void RemoveResource(Resource oldResource)
        {
            for(int i = 0; i < rooms.Count; i++)
            {
                if(rooms[i].RoomId == oldResource.roomId)
                    ThenDelete(oldResource, i);
            }
            fileRooms.Write();
        }

        private void ThenDelete(Resource oldResource, int i)
        {
            for (int j = 0; j < rooms[i].Resource.Count; j++)
            {
                if (rooms[i].Resource[j].idItem == oldResource.idItem)
                    rooms[i].Resource.RemoveAt(j);
            }
        }

        public void RemoveQuantity(Resource moved, int quantity)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].RoomId == moved.roomId)
                    ThenReduce(moved, quantity, i);
            }
            fileRooms.Write();
        }

        private void ThenReduce(Resource moved, int quantity, int i)
        {
            for (int j = 0; j < rooms[i].Resource.Count; j++)
            {
                if (rooms[i].Resource[j].idItem == moved.idItem)
                    rooms[i].Resource[j].quantity -= quantity;
            }
        }
    }
}