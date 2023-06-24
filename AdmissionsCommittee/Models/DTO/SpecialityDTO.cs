using AdmissionsCommittee.Models.Base;
using System.Collections.Generic;

namespace AdmissionsCommittee.Models.DTO
{
    public class SpecialityDTO : BaseModel
    {
        private string name;
        private string? divisionСode;
        private List<Enrollee> enrollees;

        public SpecialityDTO() { }

        public SpecialityDTO(int id,string name, string divisisonCode, List<Enrollee> enrollees = null) 
        {
            Id = id;
            Name = name;
            DivisionСode = divisisonCode;
            Enrollees = enrollees;
        }

        public string Name
        {
            get { return name; }
            set { Set(ref name, value); }
        }

        public string? DivisionСode
        {
            get { return divisionСode; }
            set { Set(ref divisionСode, value); }
        }

        public List<Enrollee> Enrollees
        {
            get { return enrollees; }
            set { Set(ref enrollees, value); }
        }
    }
}
