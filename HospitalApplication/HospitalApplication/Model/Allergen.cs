using System;

namespace Model
{
   public class Allergen 
   {
        private string id;
        private string name;
        private string specName;

        public Allergen() { }

        public Allergen(string idd, string namee)
        {
            id = idd;
            name = namee;
        }

        public Allergen(string idd, string namee, string specNamee)
        {
            id = idd;
            name = namee;
            specName = specNamee;
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

    }
}