using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalApplication.Model
{
    public class ReportedDrugs
    {
        public int IdReportedItem { get; set; }
        public String Name { get; set; }
        public String Manufacturer { get; set; }
        public String Problem { get; set; }
    }
}
