using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalApplication.Model
{
     public abstract class News
    {
        public abstract string TypeNews { get; set; }
        public abstract string Id { get; set; }
        public abstract string Title { get; set; }
        public abstract string Description { get; set; }
        public abstract DateTime PublicationDate { get; set; }
        public abstract string DurationNews { get; set; }
    }
}
