using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.IntegrationTests.TestContexts
{
    public interface ITestContext
    {
        TestArgs TestArgs { get; }
        ProjectConfigItem ProjectConfig { get; }

        object Result { get; set; }
        ProcessResults ProcessResults { get; set; }


        DBBackupFileType DBBackupFileType { get; }
        NumOfDBConnections NumOfConnectionsBefore { get; set; }
        ScriptFilesStateType ScriptFilesStateType { get; }


        void ClearProcessData();
    }
}