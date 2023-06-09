using AdmissionsCommittee.Models.Context;

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
