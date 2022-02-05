using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalApplication.Model
{
    abstract class NewsFactory
    {
        public abstract News GetNews();
    }
}
