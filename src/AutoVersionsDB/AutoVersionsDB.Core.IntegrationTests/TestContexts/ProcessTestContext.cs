using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.TestContexts
{

    public class ProcessTestContext : ITestContext
    {
        public object Result { get; set; }
        public ProcessResults ProcessResults { get; set; }

        public TestArgs TestArgs { get; }

        public ProjectConfigItem ProjectConfig => (TestArgs as ProjectConfigTestArgs).ProjectConfig;


        public DBBackupFileType DBBackupFileType { get; }
        public ScriptFilesStateType ScriptFilesStateType { get; }
        public NumOfDBConnections NumOfConnectionsBefore { get; set; }



        public ProcessTestContext(TestArgs testArgs)
        {
            TestArgs = testArgs;
        }
        public ProcessTestContext(ProjectConfigTestArgs projectConfigTestArgs)
          : this(projectConfigTestArgs as TestArgs)
        {
        }

        public ProcessTestContext(ProjectConfigTestArgs projectConfigTestArgs, DBBackupFileType dbBackupFileType, ScriptFilesStateType scriptFilesStateType)
        : this(projectConfigTestArgs)
        {
            DBBackupFileType = dbBackupFileType;
            ScriptFilesStateType = scriptFilesStateType;
        }


        public void ClearProcessData()
        {
            this.Result = null;
            this.ProcessResults = null;
        }
    }

    //public class ITestContext<TArgs> : TestContext
    //    where TArgs : TestArgs
    //{

    //    public TArgs TestArgs => base.TestArgs as TArgs;

    //    public TestContext(TArgs testArgs)
    //        : base(testArgs)
    //    {

    //    }
    //}
}
