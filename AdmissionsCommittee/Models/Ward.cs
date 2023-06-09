using AdmissionsCommittee.Models.Base;
using System.Collections.Generic;

namespace AdmissionsCommittee.Models
{
    public class Ward : BaseModel
    {
        private byte[] document;

        public Ward() { }

        public byte[] Document
        {
            get { return document; }
            set { Set(ref document, value); }
        }
    }
}
