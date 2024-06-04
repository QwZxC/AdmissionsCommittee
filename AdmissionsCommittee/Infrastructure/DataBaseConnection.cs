using AdmissionsCommittee.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace AdmissionsCommittee.Infrastructure
{
    public static class DataBaseConnection
    {
        public static ApplicationContext ApplicationContext { get; set; }

        static DataBaseConnection()
        {
            ApplicationContext = new ApplicationContext();
            ApplicationContext.Database.Migrate();
        }
    }
}
