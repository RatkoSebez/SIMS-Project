using System;
using System.Collections.Generic;
using Model;
using WorkWithFiles;

namespace Logic
{
   public class AllergensService
   {
        private FileAllergens fileAllergens = FileAllergens.Instance;
        private FilePatients filePatients = FilePatients.Instance;
        private List<Allergen> allergens;
        private List<Patient> patients;

        public AllergensService()
        {
            allergens = fileAllergens.GetAllergens();
            patients = filePatients.GetPatients();
        }

        public void CreateAllergen(Allergen newAllergen)
        {
            allergens.Add(newAllergen);
            fileAllergens.Write();
        }

        public void UpdateAllergen(Patient p)
        {
            Patient selectedPatient = filePatients.GetPatient(p.Id);
            selectedPatient.ListAllergens = p.ListAllergens;
            filePatients.Write();
        }
    }
}