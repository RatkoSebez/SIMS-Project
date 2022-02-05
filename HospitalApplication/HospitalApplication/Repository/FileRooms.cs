using HospitalApplication.Repository;
using Model;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Web;

namespace WorkWithFiles
{
    public class FileRooms : IFile
    {
        private static string path = "../../../Data/rooms.json";
        private static List<Room> rooms;
        private static FileRooms instance;

        public static FileRooms Instance
        {
            get
            {
                if (null == instance)
                    instance = new FileRooms();
                return instance;
            }
        }

        private FileRooms()
        {
            Read();
        }

        public static string GetRoomId(int roomIndex)
        {
            //List<Room> rooms = LoadRoom();
            return rooms[roomIndex].RoomId.ToString();
        }

        public void Read()
        {
            string json = File.ReadAllText(path);
            rooms = JsonConvert.DeserializeObject<List<Room>>(json);
        }

        public void Write()
        {
            string json = JsonConvert.SerializeObject(rooms, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public List<Room> ShowAllRooms()
        {
            return rooms;
        }

        public List<Renovation> ShowAllRenovations()
        {
            List<Renovation> renovations = new List<Renovation>();
            for (int i = 0; i < rooms.Count; i++)
            {
                for (int j = 0; j < rooms[i].Renovation.Count; j++)
                    renovations.Add(rooms[i].Renovation[j]);
            }
            return renovations;
        }

        public Room GetRoom(int roomIdToFind)
        {
            Room showThatRoom = new Room();
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].RoomId == roomIdToFind)
                {
                    showThatRoom = rooms[i];
                    break;
                }
            }
            return showThatRoom;
        }

        public Tuple<bool, int> IsRoomFree(DateTime date)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                bool roomIsFree = true;
                for (int j = 0; j < rooms[i].Scheduled.Count; j++)
                    if (rooms[i].Scheduled[j] == date) roomIsFree = false;
                for (int j = 0; j < rooms[i].Renovation.Count; j++)
                    for (int k = 0; k < rooms[i].Renovation[j].Days.Count; k++)
                        if (rooms[i].Renovation[j].Days[k] == date) roomIsFree = false;
                if (roomIsFree) return new Tuple<bool, int>(true, i);
            }
            return new Tuple<bool, int>(false, -1);
        }

        public void AddAppointmentToRoom(int roomIndex, DateTime date)
        {
            rooms[roomIndex].Scheduled.Add(date);
            Write();
        }

        public void RemoveAppointmentFromRoom(string roomId, DateTime date)
        {
            for (int i = 0; i < rooms.Count; i++){
                if (rooms[i].RoomId.ToString() == roomId){
                    for (int j = 0; j < rooms[i].Scheduled.Count; j++){
                        if (rooms[i].Scheduled[j] == date){
                            rooms[i].Scheduled.RemoveAt(j);
                            Write();
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }
}