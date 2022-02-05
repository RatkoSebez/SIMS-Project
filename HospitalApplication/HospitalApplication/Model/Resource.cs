using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalApplication.Model
{
    public class Resource
    {
        public int idItem { get; set; }
        public String name { get; set; }
        public int quantity { get; set; }
        public String manufacturer { get; set; }
        public bool isStatic { get; set; }
        public int roomId { get; set; }
    }
}
