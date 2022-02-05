using HospitalApplication.Model;
using HospitalApplication.Service;
using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalApplication.Controller
{
    public class ManagerController
    {
        private RoomService RoomManagment = new RoomService();
        private RelocationResourceService RelocationResource = new RelocationResourceService();
        private RenovationsService RenovationService = new RenovationsService();

        public void CreateRoom(Room newRoom)
        {
            RoomManagment.CreateRoom(newRoom);
        }

        public void AddItem(Resource newResource)
        {
            RoomManagment.AddResource(newResource);
        }

        public void TransferDynamicItem(Resource oldResource, int quantity)
        {
            RoomManagment.TransferDynamicResource(oldResource, quantity);
        }

        public void RemoveRoom(Room oldRoom)
        {
            RoomManagment.RemoveRoom(oldRoom);
        }

        public void RemoveItem(Resource oldResource)
        {
            RoomManagment.RemoveResource(oldResource);
        }

        public void RemoveQuantity(Resource resourceForReduce, int quantity)
        {
            RoomManagment.RemoveQuantity(resourceForReduce, quantity);
        }

        public void DeleteTransfer(Transfer oldTransfer)
        {
            RelocationResource.DeleteTransfer(oldTransfer);
        }

        public void MakeScheduled(Transfer forTransfer)
        {
            RelocationResource.AddStaticTransfer(forTransfer);
        }

        public void CheckTransfers()
        {
            RelocationResource.DeleteTransferIfPass();
        }

        public void RemoveRenovation(Renovation oldRenovation)
        {
            RenovationService.RemoveRenovation(oldRenovation);
        }

        public void AddRenovation(Renovation newRenovation)
        {
            RenovationService.AddRenovation(newRenovation);
        }

        public void CheckRenovation(Renovation renovationForCheck)
        {
            RenovationService.CheckRenovation(renovationForCheck);
        }

        public void IsFinishRenovation()
        {
            RenovationService.DeleteOldRenovations();
        }
    }
}
