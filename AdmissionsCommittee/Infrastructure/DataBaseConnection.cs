using AdmissionsCommittee.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Infrastructure
{
    public static class DataBaseConnection
    {
        public static ApplicationContext ApplicationContext { get; set; }

        static DataBaseConnection()
        {
            ApplicationContext = new ApplicationContext();
        }
    }
}
