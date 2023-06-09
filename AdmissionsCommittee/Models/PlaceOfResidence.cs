using AdmissionsCommittee.Models.Base;
using System.Collections.Generic;

namespace AdmissionsCommittee.Models
{
    public class PlaceOfResidence : BaseModel
    {
        public string name;
        public List<District> districts;

        public PlaceOfResidence() { }

        public string Name
        {
            get { return name; }
            set { Set(ref name, value); }
        }

        public List<District> Districts
        {
            get { return districts; }
            set { Set(ref districts, value); } 
        }
    }
}
