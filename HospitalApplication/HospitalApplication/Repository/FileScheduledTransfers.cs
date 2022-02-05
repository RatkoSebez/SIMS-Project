using HospitalApplication.Model;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalApplication.Repository
{
    public class FileScheduledTransfers : IFile
    {
        private static string path = "../../../Data/transfers.json";
        private static List<Transfer> transfers;
        private static FileScheduledTransfers instance;

        public static FileScheduledTransfers Instance
        {
            get
            {
                if (null == instance)
                    instance = new FileScheduledTransfers();
                return instance;
            }
        }

        private FileScheduledTransfers()
        {
            Read();
        }

        public void Read()
        {
            string json = File.ReadAllText(path);
            transfers = new JavaScriptSerializer().Deserialize<List<Transfer>>(json);
        }

        public void Write()
        {
            string json = new JavaScriptSerializer().Serialize(transfers);
            File.WriteAllText(path, json);
        }
        public List<Transfer> ShowAllTransfers()
        {
            return transfers;
        }
    }
}
