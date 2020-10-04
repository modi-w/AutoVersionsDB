﻿using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.Common.CLI;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.Process;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests
{
    public class DBVersionsTest : ITestDefinition
    {
        private readonly DBHandler _dbHandler;
        private readonly FoldersUtils _foldersUtils;
        private readonly DBAsserts _dbAsserts;

        public DBVersionsTest(DBHandler dbHandler,
                                        FoldersUtils foldersUtils,
                                        ProcessAsserts processAsserts,
                                        DBAsserts dbAsserts)
        {
            _dbHandler = dbHandler;
            _foldersUtils = foldersUtils;
            _dbAsserts = dbAsserts;
        }

        public TestContext Arrange(ProjectConfigItem projectConfig, DBBackupFileType dbBackupFileType, ScriptFilesStateType scriptFilesStateType)
        {
            TestContext testContext = new TestContext(dbBackupFileType, scriptFilesStateType, projectConfig);

            MockObjectsProvider.MockProjectConfigsStorage.Setup(m => m.GetProjectConfigById(It.IsAny<string>())).Returns(testContext.ProjectConfig);
            _dbHandler.RestoreDB(testContext.ProjectConfig.DBConnectionInfo, dbBackupFileType);
            testContext.NumOfConnectionsBefore = _dbHandler.GetNumOfOpenConnection(testContext.ProjectConfig.DBConnectionInfo);
            _foldersUtils.RemoveArtifactTempFolder(testContext.ProjectConfig);

            return testContext;
        }

        public void Act(TestContext testContext)
        {

        }


        public void Asserts(TestContext testContext)
        {
            _dbAsserts.AssertNumOfOpenDbConnection(GetType().Name, testContext.ProjectConfig.DBConnectionInfo, testContext.NumOfConnectionsBefore);
            _dbAsserts.AssertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(GetType().Name, testContext.ProjectConfig.DBConnectionInfo, DBBackupFileType.MiddleState);
        }




    }
}