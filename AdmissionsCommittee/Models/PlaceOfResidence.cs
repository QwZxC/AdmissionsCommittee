using AdmissionsCommittee.Models.Base;
using System.Collections.Generic;

namespace AdmissionsCommittee.Models
{
    public class PlaceOfResidence : BaseModel
    {
        public string name;

        public PlaceOfResidence() { }

        public string Name
        {
            get { return name; }
            set { Set(ref name, value); }
        }
    }
}
