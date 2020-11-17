using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.Common.CLI;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.Process;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ProjectConfigsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.TestsUtils
{
    public class ProjectConfigWithDBArrangeAndAssert
    {
        private readonly ProjectConfigsStorageHelper _projectConfigsStorageHelper;
        private readonly DBHandler _dbHandler;
        private readonly FoldersUtils _foldersUtils;
        private readonly DBAsserts _dbAsserts;
        private readonly ProcessAsserts _processAsserts;
        private readonly ProjectConfigsFactory _projectConfigsFactory;


        public ProjectConfigWithDBArrangeAndAssert(ProjectConfigsStorageHelper projectConfigsStorageHelper,
                                    DBHandler dbHandler,
                                    FoldersUtils foldersUtils,
                                    ProcessAsserts processAsserts,
                                    DBAsserts dbAsserts,
                                    ProjectConfigsFactory projectConfigsFactory)
        {
            _projectConfigsStorageHelper = projectConfigsStorageHelper;
            _dbHandler = dbHandler;
            _foldersUtils = foldersUtils;
            _processAsserts = processAsserts;
            _dbAsserts = dbAsserts;
            _projectConfigsFactory = projectConfigsFactory;
        }

        public ITestContext Arrange(TestArgs testArgs, bool devEnvironment, DBBackupFileType dbBackupFileType, ScriptFilesStateType scriptFilesStateType)
        {
            ITestContext testContext = new ProcessTestContext(testArgs as ProjectConfigTestArgs, dbBackupFileType, scriptFilesStateType);

            testContext.ProjectConfig.DevEnvironment = devEnvironment;

            ProjectConfigItem projectConfig = testContext.ProjectConfig;
            _projectConfigsFactory.SetFoldersPathByDBType(ref projectConfig, testContext.ScriptFilesStateType);

            _projectConfigsStorageHelper.PrepareTestProject(testContext.ProjectConfig);

            if (testContext.DBBackupFileType != DBBackupFileType.None)
            {
                _dbHandler.RestoreDB(testContext.ProjectConfig.DBConnectionInfo, dbBackupFileType);
                testContext.NumOfConnectionsBefore = _dbHandler.GetNumOfOpenConnection(testContext.ProjectConfig.DBConnectionInfo);
            }


            _foldersUtils.RemoveArtifactTempFolder(testContext.ProjectConfig);

            return testContext;
        }


        public void Asserts(string testName,ITestContext testContext, bool processSucceed)
        {
            if (testContext.DBBackupFileType != DBBackupFileType.None)
            {
                _dbAsserts.AssertNumOfOpenDbConnection(testName, testContext.ProjectConfig.DBConnectionInfo, testContext.NumOfConnectionsBefore);
                _dbAsserts.AssertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(testName, testContext.ProjectConfig.DBConnectionInfo, testContext.DBBackupFileType);
            }

            if (processSucceed)
            {
                _processAsserts.AssertProccessValid(testName, testContext.ProcessResults.Trace);
            }
        }


        public void Release(ITestContext testContext)
        {
            _projectConfigsStorageHelper.ClearAllProjects();

            //TODO: Delete DB
        }




    }
}
