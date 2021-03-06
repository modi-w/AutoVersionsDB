﻿using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.UI.DBVersions;
using System.Collections.Generic;

namespace AutoVersionsDB.Core.IntegrationTests.TestContexts
{

    public class DBVersionsUITestContext : ITestContext
    {
        private readonly ITestContext _testContext;

        public TestArgs TestArgs => _testContext.TestArgs;
        public ProjectConfigItem ProjectConfig => _testContext.ProjectConfig;


        public ProcessResults ProcessResults
        {
            get => _testContext.ProcessResults;
            set => _testContext.ProcessResults = value;
        }
        public object Result
        {
            get => _testContext.Result;
            set => _testContext.Result = value;
        }


        public DBBackupFileType DBBackupFileType => _testContext.DBBackupFileType;
        public NumOfDBConnections NumOfConnectionsBefore
        {
            get => _testContext.NumOfConnectionsBefore;
            set => _testContext.NumOfConnectionsBefore = value;
        }

        public ScriptFilesStateType ScriptFilesStateType => _testContext.ScriptFilesStateType;



        public List<DBVersionsViewStateType> ViewStatesHistory { get; set; }


        public DBVersionsUITestContext(ITestContext testContext)
        {
            _testContext = testContext;
            ViewStatesHistory = new List<DBVersionsViewStateType>();
        }


        public void ClearProcessData()
        {
            _testContext.ClearProcessData();

            ViewStatesHistory.Clear();
        }
    }
}
