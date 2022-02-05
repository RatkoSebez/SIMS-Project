using HospitalApplication.Model;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalApplication.Repository
{
    class FileDrugs : IFile
    {
        public static string path = "../../../Data/drugs.json";
        private static List<Drugs> drugs;
        private static FileDrugs instance;
        public static FileDrugs Instance
        {
            get
            {
                if (null == instance)
                    instance = new FileDrugs();
                return instance;
            }
        }

        private FileDrugs()
        {
            Read();
        }
        public void Read()
        {
            string json = File.ReadAllText(path);
            drugs = new JavaScriptSerializer().Deserialize<List<Drugs>>(json);
        }
        public void Write()
        {
            string json = new JavaScriptSerializer().Serialize(drugs);
            File.WriteAllText(path, json);
        }
        public List<Drugs> AllDrugs()
        {
            return drugs;
        }
    }
}
