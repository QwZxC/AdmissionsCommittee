using AdmissionsCommittee.Models.Base;

namespace AdmissionsCommittee.Models
{
    public class Certificate : BaseModel
    {
        private bool original;
        private double avarageScore;
        private byte[]? photo;

        public Certificate() { }

        public bool Original
        {
            get { return original; }
            set { Set(ref original, value); }
        }

        public double AvarageScore
        {
            get { return avarageScore; }
            set { Set(ref avarageScore, value); }
        }

        public byte[]? Photo
        {
            get { return photo; }
            set { Set(ref photo, value); }
        }
    }
}
