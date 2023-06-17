using AdmissionsCommittee.Infrastructure;
using AdmissionsCommittee.Models.Base;
using System;
using System.Linq;

namespace AdmissionsCommittee.Models
{
    public class Enrollee : BaseModel
    {
        private string name;
        private string surname;
        private string? patronymic;
        private string gender;
        private DateOnly dateOfBirth;
        private string snils;
        private int age;
        private DateOnly yearOfAdmission;
        private Education education;
        private Disability disability;
        private Certificate certificate;
        private Ward ward;
        private PlaceOfResidence placeOfResidence;
        private Speciality speciality;
        private Citizenship citizenship;
        private District district;
        private bool isBudget;
        private bool isEnlisted;

        public Enrollee() { }

        public string Name 
        { 
            get { return name; } 
            set { Set(ref name, value); } 
        }

        public string Surname
        {
            get { return surname; }
            set { Set(ref surname, value); }
        }

        public string? Patronymic
        {
            get { return patronymic; }
            set { Set(ref patronymic, value); }
        }

        public string Gender
        {
            get { return gender; }
            set { Set(ref gender, value); }
        }

        public DateOnly DateOfBirth
        {
            get { return dateOfBirth; }
            set { Set(ref dateOfBirth, value); }
        }

        public string Snils
        {
            get { return snils; }
            set{ Set(ref snils, value); }
        }

        public int Age
        {
            get { return age; }
            set { Set(ref age, value); }
        }

        public DateOnly YearOfAdmission
        {
            get { return yearOfAdmission; }
            set { Set(ref yearOfAdmission, value); }
        }
        
        public Education Education
        {
            get { return education; }
            set { Set(ref education, value); }
        }

        public Disability Disability
        {
            get { return disability; }
            set { Set(ref disability, value); }
        }

        public Certificate Certificate
        {
            get { return certificate; }
            set { Set(ref certificate, value); }
        }

        public Ward Ward
        {
            get { return ward; }
            set { Set(ref ward, value); }
        }

        public Speciality Speciality
        {
            get { return speciality; }
            set { Set(ref speciality, value); }
        }

        public Citizenship Citizenship
        {
            get { return citizenship; }
            set 
            {
                if (value.Country != "Российская Федерация")
                {
                    PlaceOfResidence = DataBaseConnection.ApplicationContext.PlaceOfResidence.ToList().Find(district => district.Name == "Иностранный гражданин");
                }
                Set(ref citizenship, value); 
            }
        }

        public PlaceOfResidence PlaceOfResidence
        {
            get { return placeOfResidence; }
            set
            {
                if (value.Name != "Костромская область") 
                {
                    District = DataBaseConnection.ApplicationContext.District.ToList().Find(district => district.Name == "За пределами Костромской области");
                }
                Set(ref placeOfResidence, value); 
            }
        }

        public District District
        {
            get { return district; }
            set { Set(ref district, value); }
        }

        public bool IsBudget
        {
            get { return isBudget; }
            set { Set(ref isBudget, value); }
        }

        public bool IsEnlisted
        {
            get { return isEnlisted; }
            set { Set(ref isEnlisted, value); }
        }

        public int? EducationId { get; set; }

        public int? DisabilityId { get; set; }

        public int? CertificateId { get; set; }

        public int? SpecialityId { get; set; }

        public int? CitizenshipId { get; set; }

        public int? PlaceOfResidenceId { get; set; }

        public int? WardId { get; set; }

        public int? DistrictId { get; set; }
    }
}
