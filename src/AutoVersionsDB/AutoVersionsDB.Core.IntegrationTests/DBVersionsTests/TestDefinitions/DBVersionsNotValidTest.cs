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
    public class DBVersionsNotValidTest : ITestDefinition
    {
        private readonly DBVersionsTest _dbVersionsTest;
        private readonly ProcessAsserts _processAsserts;

        public DBVersionsNotValidTest(DBVersionsTest dbVersionsTest,
                                        ProcessAsserts processAsserts)
        {
            _dbVersionsTest = dbVersionsTest;
            _processAsserts = processAsserts;
        }

        public TestContext Arrange(ProjectConfigItem projectConfig, DBBackupFileType dbBackupFileType, ScriptFilesStateType scriptFilesStateType)
        {
            return _dbVersionsTest.Arrange(projectConfig, dbBackupFileType, scriptFilesStateType);
        }

        public void Act(TestContext testContext)
        {
        }


        public void Asserts(TestContext testContext)
        {
            _dbVersionsTest.Asserts(testContext);

            _processAsserts.AssertProccessHasErrors(GetType().Name, testContext.ProcessResults.Trace);
        }




    }
}
