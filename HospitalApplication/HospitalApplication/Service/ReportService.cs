using HospitalApplication.Model;
using HospitalApplication.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalApplication.Service
{
    public class ReportService
    {
        private List<ReportedDrugs> reports;
        private FileReportedDrugs fileReportedDrugs = FileReportedDrugs.Instance;
        public ReportService()
        {
            reports = fileReportedDrugs.GetAllReports();
        }

        public void DeleteReport(ReportedDrugs forDelete)
        {
            for(int i=0; i<reports.Count; i++)
            {
                if (forDelete.IdReportedItem == reports[i].IdReportedItem)
                    reports.RemoveAt(i);
            }
            fileReportedDrugs.Write();
        }
        
        public void AddReport(ReportedDrugs newReport)
        {
            reports.Add(newReport);
            fileReportedDrugs.Write();
        }
    }
}
