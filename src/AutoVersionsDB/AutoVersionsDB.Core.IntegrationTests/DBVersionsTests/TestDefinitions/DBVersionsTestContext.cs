using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions
{
    public class DBVersionsTestContext : TestContext<ProjectConfigTestArgs>
    {
        public DBBackupFileType DBBackupFileType { get; }
        public ScriptFilesStateType ScriptFilesStateType { get; }
        public ProjectConfigItem ProjectConfig => (TestArgs as ProjectConfigTestArgs).ProjectConfig;
        public NumOfDBConnections NumOfConnectionsBefore { get; set; }


        public DBVersionsTestContext(ProjectConfigTestArgs projectConfigTestArgs, DBBackupFileType dbBackupFileType, ScriptFilesStateType scriptFilesStateType)
            : base(projectConfigTestArgs)
        {
            DBBackupFileType = dbBackupFileType;
            ScriptFilesStateType = scriptFilesStateType;
        }
    }
}
