using AdmissionsCommittee.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdmissionsCommittee.Models
{
    public class Speciality : BaseModel
    {
        private string name;
        private string? divisionСode;
        private List<Enrollee> enrollees;

        public Speciality() { }

        public Speciality(string name, string divisisonCode, List<Enrollee> enrollees = null)
        {
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

        [NotMapped]
        public string DisplayName
        {
            get { return $"{Name} - {DivisionСode}"; }
        }

        public List<Enrollee> Enrollees
        {
            get { return enrollees; }
            set { Set(ref enrollees, value); }
        }
    }
}
