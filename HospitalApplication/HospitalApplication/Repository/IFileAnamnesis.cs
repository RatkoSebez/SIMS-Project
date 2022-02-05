using HospitalApplication.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalApplication.Repository
{
    interface IFileAnamnesis : IFile
    {
        public List<Anamnesis> GetAnamnesis(string patientsId);
    }
}
