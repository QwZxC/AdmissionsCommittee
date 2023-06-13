using AdmissionsCommittee.Models.Base;
using System.Collections.Generic;
namespace AdmissionsCommittee.Models
{
    public class Speciality : BaseModel
    {
        private string name;
        private string? description;
        private List<Enrollee> enrollees;

        public Speciality() { }

        public string Name 
        { 
            get { return name; }
            set { Set(ref name, value); }
        }

        public string? Description
        {
            get { return description; }
            set { Set(ref description, value); }
        }

        public List<Enrollee> Enrollees
        {
            get { return enrollees; }
            set { Set(ref enrollees, value); }
        }
    }
}
