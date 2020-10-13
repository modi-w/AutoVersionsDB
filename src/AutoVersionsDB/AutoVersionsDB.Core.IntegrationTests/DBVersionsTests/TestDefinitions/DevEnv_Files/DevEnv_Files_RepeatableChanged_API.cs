﻿using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Files;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_NewScrtiptFile;
using AutoVersionsDB.Core.IntegrationTests.Process;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Files
{
    public class DevEnv_Files_RepeatableChanged_API : TestDefinition<DBVersionsTestContext>
    {
        private readonly DBVersionsTestHelper _dbVersionsTestHelper;
        private readonly ScriptFilesAsserts _scriptFilesAsserts;


        public DevEnv_Files_RepeatableChanged_API(DBVersionsTestHelper dbVersionsTestHelper,
                                                    ScriptFilesAsserts scriptFilesAsserts)
        {
            _dbVersionsTestHelper = dbVersionsTestHelper;
            _scriptFilesAsserts = scriptFilesAsserts;
        }


        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = _dbVersionsTestHelper.Arrange(testArgs, true, DBBackupFileType.FinalState_DevEnv, ScriptFilesStateType.RepeatableChanged);


            return testContext;
        }

        public override void Act(DBVersionsTestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.GetScriptFilesState(testContext.ProjectConfig.Id, null);
        }


        public override void Asserts(DBVersionsTestContext testContext)
        {
            _dbVersionsTestHelper.Asserts(testContext, true);

            ScriptFilesState scriptFilesState = testContext.ProcessResults.Results as ScriptFilesState;

            //FileStateListAssert incfileStateListAssert = new FileStateListAssert(this.GetType().Name, scriptFilesState.IncrementalScriptFilesComparer);
            //incfileStateListAssert.AssertNumOfFiles(5);
            //incfileStateListAssert.AssertFileState(0, "incScript_2020-02-25.100_initState.sql", HashDiffType.Equal);
            //incfileStateListAssert.AssertFileState(1, "incScript_2020-02-25.101_CreateLookupTable1.sql", HashDiffType.Equal);
            //incfileStateListAssert.AssertFileState(2, "incScript_2020-02-25.102_CreateLookupTable2.sql", HashDiffType.Different);
            //incfileStateListAssert.AssertFileState(3, "incScript_2020-03-02.100_CreateTransTable1.sql", HashDiffType.NotExist);
            //incfileStateListAssert.AssertFileState(4, "incScript_2020-03-02.101_CreateInvoiceTable1.sql", HashDiffType.NotExist);

            //FileStateListAssert rptfileStateListAssert = new FileStateListAssert(this.GetType().Name, scriptFilesState.RepeatableScriptFilesComparer);
            //rptfileStateListAssert.AssertNumOfFiles(2);
            //rptfileStateListAssert.AssertFileState(0, "rptScript_DataForLookupTable1.sql", HashDiffType.NotExist);
            //rptfileStateListAssert.AssertFileState(1, "rptScript_DataForLookupTable2.sql", HashDiffType.NotExist);

            //FileStateListAssert dddfileStateListAssert = new FileStateListAssert(this.GetType().Name, scriptFilesState.DevDummyDataScriptFilesComparer);
            //dddfileStateListAssert.AssertNumOfFiles(2);
            //dddfileStateListAssert.AssertFileState(0, "dddScript_DataForInvoiceTable1.sql", HashDiffType.NotExist);
            //dddfileStateListAssert.AssertFileState(1, "dddScript_DataForTransTable1.sql", HashDiffType.NotExist);
        }

        public override void Release(DBVersionsTestContext testContext)
        {
            _dbVersionsTestHelper.Release(testContext);
        }




    }
}
