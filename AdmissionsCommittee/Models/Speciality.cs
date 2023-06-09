using AdmissionsCommittee.Models.Base;

namespace AdmissionsCommittee.Models
{
    public class Speciality : BaseModel
    {
        private string name;
        private string description;

        public Speciality() { }

        public string Name 
        { 
            get { return name; }
            set { Set(ref name, value); }
        }

        public string Description
        {
            get { return description; }
            set { Set(ref description, value); }
        }
    }
}
