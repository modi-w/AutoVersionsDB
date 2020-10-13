using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;

namespace AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB
{
    public class NumOfDBConnections
    {
        public string DBName { get; set; }
        public int NumOfConnectionsToDB { get; set; }

        public int NumOfConnectionsToAdminDB { get; set; }
    }
}
