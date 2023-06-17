using AdmissionsCommittee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Reports
{
    public class EnrolleeReport
    {
        public List<Enrollee> Enrollees { get; set; }

        public EnrolleeReport(List<Enrollee> enrollees)
        {
            Enrollees = enrollees;
        }
    }
}
