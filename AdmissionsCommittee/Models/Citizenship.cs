using AdmissionsCommittee.Models.Base;
using System.Collections.Generic;

namespace AdmissionsCommittee.Models
{
    public class Citizenship : BaseModel
    {
        private string country;
        private List<Enrollee> enrollees;

        public Citizenship() 
        {
            Enrollees = new List<Enrollee>();
        }

        public string Country 
        { 
            get { return country; }
            set { Set(ref country, value); }
        }

        public List<Enrollee> Enrollees
        {
            get { return enrollees; }
            set { Set(ref enrollees, value); }
        }
    }
}
