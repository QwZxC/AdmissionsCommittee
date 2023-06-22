using AdmissionsCommittee.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdmissionsCommittee.Models.DTO
{
    public class EducationDTO : BaseModel
    {
        private bool after11School;
        private bool after9School;
        private string additionalEducation = "";

        public EducationDTO() { }

        public EducationDTO(int id,bool after11School, bool after9School, string additionalEducation) 
        {
            Id = id;
            After11School = after11School;
            After9School = after9School;
            AdditionalEducation = additionalEducation;
        }

        public bool After11School
        {
            get { return after11School; }
            set { Set(ref after11School, value); }
        }

        public bool After9School
        {
            get { return after9School; }
            set { Set(ref after9School, value); }
        }

        public string AdditionalEducation
        {
            get { return additionalEducation; }
            set { Set(ref additionalEducation, value); }
        }

        [NotMapped]
        public string DisplayName
        {
            get
            {
                if (After9School)
                {
                    return "После 9-го класса";
                }
                else if (After11School)
                {
                    return "После 11-го класса";
                }
                return AdditionalEducation;
            }
        }
    }
}
