using AdmissionsCommittee.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdmissionsCommittee.Models.Base
{
    public class BaseModel : NotifyPropertyChangedObject
    {
        private int id;
        private bool isSelected;

        public int Id
        {
            get { return id; }
            set { Set(ref id, value); }
        }

        [NotMapped]
        public bool IsSelected
        {
            get { return isSelected; }
            set { Set(ref isSelected, value); }
        }
        
        [NotMapped]
        public bool IsValid { get; set; }
    }
}
