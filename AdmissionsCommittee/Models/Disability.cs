using AdmissionsCommittee.Models.Base;

namespace AdmissionsCommittee.Models
{
    public class Disability : BaseModel
    {
        private byte[] document;

        public Disability() { }

        public byte[] Document
        {
            get { return document; } 
            set { Set(ref document, value); }
        }
    }
}
