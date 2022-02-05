using HospitalApplication.Model;
using HospitalApplication.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace HospitalApplication.Service
{
    public class MedicinesService
    {
        private List<Drugs> allMedicines;
        private FileDrugs fileDrugs = FileDrugs.Instance;

        public MedicinesService()
        {
            allMedicines = fileDrugs.AllDrugs();
        }

        public void CreateDrug (Drugs newDrug)
        {
            int ok = 0;
            for(int i=0; i < allMedicines.Count; i++)
            {
                if (newDrug.Name == allMedicines[i].Name && newDrug.Manufacturer == allMedicines[i].Manufacturer)
                {
                    MessageBox.Show("That Medicines already exist", "Error");
                    ok++;
                }
            }
            if(ok == 0 )
                allMedicines.Add(newDrug);
            fileDrugs.Write();
        }
        public void DeleteDrug (Drugs oldDrug)
        {
            for(int i=0; i<allMedicines.Count; i++)
            {
                if (oldDrug.ItemId == allMedicines[i].ItemId)
                    allMedicines.RemoveAt(i);
            }
            fileDrugs.Write();
        }
    }
}
