using AdmissionsCommittee.Models.Base;

namespace AdmissionsCommittee.Models
{
    public class District : BaseModel
    {
        private string name;

        public District() { }

        public string Name
        {
            get { return name; }
            set { Set(ref name, value); }
        }

        public int PlaceOfResidenceId { get; set; }
    }
}
