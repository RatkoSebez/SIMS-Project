using HospitalApplication.Model;
using HospitalApplication.Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using WorkWithFiles;

namespace HospitalApplication.Service
{
    public class RelocationResourceService
    {
        private List<Transfer> transfers;
        private List<Room> rooms;
        private FileRooms fileRooms = FileRooms.Instance;
        private FileScheduledTransfers fileTransfers = FileScheduledTransfers.Instance;
        private Resource temp = new Resource();

        public RelocationResourceService()
        {
            transfers = fileTransfers.ShowAllTransfers();
            rooms = fileRooms.ShowAllRooms();
        }

        public void DeleteTransfer(Transfer oldTransfer)
        {
            for(int i=0; i < transfers.Count; i++)
            {
                if(transfers[i].idTransfer == oldTransfer.idTransfer)
                    transfers.RemoveAt(i);
            }
            fileTransfers.Write();
        }

        public void AddStaticTransfer(Transfer newTransfer)
        {
            int ok = 0;
            for(int i=0; i < transfers.Count; i++)
            {
                if (transfers[i].idTransfer == newTransfer.idTransfer || transfers[i].date == newTransfer.date && transfers[i].idRoomTo == newTransfer.idRoomTo)
                {
                    MessageBox.Show("That term for that room is already taken!", "Error");
                    ok++;
                    break;
                }
            }
            if (newTransfer.idRoomFrom == newTransfer.idRoomTo)
            {
                MessageBox.Show("That resource is alredy in that room", "Error");
                ok++;
            }
            if (ok == 0)
                transfers.Add(newTransfer);
            fileTransfers.Write();
        }

        public void DeleteTransferIfPass()
        {
            if (transfers == null)
                transfers = new List<Transfer>();
            for (int i=0; i < transfers.Count; i++)
            {
                if(transfers[i].date <= DateTime.Now)
                {
                    PassToResources(i);
                    transfers.RemoveAt(i);
                }
            }
            fileTransfers.Write();
            fileRooms.Write();
        }

        private void PassToResources(int i)
        {
            for (int l = 0; l < transfers[i].resource.Count; l++)
            {
                DeleteResourceIdRoomFrom(i, l);
                AddResourceIdRoomTo(i, l);
            }
        }

        private void AddResourceIdRoomTo(int i, int l)
        {
            for (int z = 0; z < rooms.Count; z++)
            {
                if (transfers[i].idRoomTo == rooms[z].RoomId)
                {
                    if (rooms[z].Resource.Count != 0)
                    {
                        FindThatResourceAndAddOnIt(i, l, z);
                        break;
                    }
                    else
                    {
                        temp = transfers[i].resource[l];
                        temp.roomId = transfers[i].idRoomTo;
                        rooms[z].Resource.Add(temp);
                        transfers[i].quantity++;
                        break;
                    }
                }

            }
        }

        private void FindThatResourceAndAddOnIt(int i, int l, int z)
        {
            int greska = 0;
            for (int k = 0; k < rooms[z].Resource.Count; k++)
            {
                if (rooms[z].Resource[k].idItem == transfers[i].resource[l].idItem)
                {
                    rooms[z].Resource[k].quantity += transfers[i].quantity;
                    greska++;
                    break;
                }
            }
            if(greska == 0)
            {
                temp = transfers[i].resource[l];
                temp.roomId = transfers[i].idRoomTo;
                temp.quantity = transfers[i].quantity;
                rooms[z].Resource.Add(temp);
            }
        }

        private void DeleteResourceIdRoomFrom(int i, int l)
        {
            for (int j = 0; j < rooms.Count; j++)
            {
                if (transfers[i].idRoomFrom == rooms[j].RoomId)
                {
                    FindThatResourceAndDecrease(i, l, j);
                    break;
                }
            }
        }

        private void FindThatResourceAndDecrease(int i, int l, int j)
        {
            for (int k = 0; k < rooms[j].Resource.Count; k++)
            {
                if (transfers[i].resource[l].idItem == rooms[j].Resource[k].idItem)
                {
                    rooms[j].Resource[k].quantity -= transfers[i].quantity;
                    break;
                }
            }
        }

        public bool CheckDoesRoomExist(Transfer newTransfer)
        {
            int ok = 0;
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].RoomId == newTransfer.idRoomTo)
                    ok++;
            }
            if (ok != 0)
                return true;
            return false;
        }
    }
}
