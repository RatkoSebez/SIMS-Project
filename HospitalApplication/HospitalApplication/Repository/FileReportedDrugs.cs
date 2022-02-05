using HospitalApplication.Model;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalApplication.Repository
{
    class FileReportedDrugs : IFile
    {
        public static string path = "../../../Data/reportedDrugs.json";
        private static List<ReportedDrugs> reports;
        private static FileReportedDrugs instance;

        public static FileReportedDrugs Instance
        {
            get
            {
                if (null == instance)
                    instance = new FileReportedDrugs();
                return instance;
            }
        }

        private FileReportedDrugs()
        {
            Read();
        }
        public void Read()
        {
            string json = File.ReadAllText(path);
            reports = new JavaScriptSerializer().Deserialize<List<ReportedDrugs>>(json);
        }
        public void Write()
        {
            string json = new JavaScriptSerializer().Serialize(reports);
            File.WriteAllText(path, json);
        }
        public List<ReportedDrugs> GetAllReports()
        {
            return reports;
        }
    }
}
