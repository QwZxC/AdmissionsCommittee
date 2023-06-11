using AdmissionsCommittee.Models.Base;
using System.Collections.Generic;

namespace AdmissionsCommittee.Models
{
    public class District : BaseModel
    {
        private string name;
        private List<Enrollee> enrolle;

        public District() { }

        public string Name
        {
            get { return name; }
            set { Set(ref name, value); }
        }

        public List<Enrollee> Enrollees
        {
            get { return enrolle; }
            set { Set(ref enrolle, value); }   
        }
    }
}
