using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Files;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_NewScrtiptFile;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using AutoVersionsDB.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Files
{
    public class DevEnv_Files_RepeatableChanged_API : TestDefinition
    {
        private readonly ProjectConfigWithDBArrangeAndAssert _projectConfigWithDBArrangeAndAssert;


        public DevEnv_Files_RepeatableChanged_API(ProjectConfigWithDBArrangeAndAssert projectConfigWithDBArrangeAndAssert)
        {
            _projectConfigWithDBArrangeAndAssert = projectConfigWithDBArrangeAndAssert;
        }


        public override ITestContext Arrange(TestArgs testArgs)
        {
            ITestContext testContext = _projectConfigWithDBArrangeAndAssert.Arrange(testArgs, true, DBBackupFileType.FinalState_DevEnv, ScriptFilesStateType.RepeatableChanged);


            return testContext;
        }

        public override void Act(ITestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.GetScriptFilesState(testContext.ProjectConfig.Id, null);
        }


        public override void Asserts(ITestContext testContext)
        {
            _projectConfigWithDBArrangeAndAssert.Asserts(GetType().Name, testContext, true);

            ScriptFilesState scriptFilesState = testContext.ProcessResults.Results as ScriptFilesState;

            FileStateListAssert incfileStateListAssert = new FileStateListAssert(this.GetType().Name, scriptFilesState.IncrementalScriptFilesComparer);
            incfileStateListAssert.AssertNumOfFiles(5);
            incfileStateListAssert.AssertFileState(0, "incScript_0001_initState.sql", HashDiffType.Equal);
            incfileStateListAssert.AssertFileState(1, "incScript_0002_CreateLookupTable1.sql", HashDiffType.Equal);
            incfileStateListAssert.AssertFileState(2, "incScript_0003_CreateLookupTable2.sql", HashDiffType.Equal);
            incfileStateListAssert.AssertFileState(3, "incScript_0004_CreateTransTable1.sql", HashDiffType.Equal);
            incfileStateListAssert.AssertFileState(4, "incScript_0005_CreateInvoiceTable1.sql", HashDiffType.Equal);

            FileStateListAssert rptfileStateListAssert = new FileStateListAssert(this.GetType().Name, scriptFilesState.RepeatableScriptFilesComparer);
            rptfileStateListAssert.AssertNumOfFiles(2);
            rptfileStateListAssert.AssertFileState(0, "rptScript_0001_DataForLookupTable1.sql", HashDiffType.Different);
            rptfileStateListAssert.AssertFileState(1, "rptScript_0002_DataForLookupTable2.sql", HashDiffType.Equal);

            FileStateListAssert dddfileStateListAssert = new FileStateListAssert(this.GetType().Name, scriptFilesState.DevDummyDataScriptFilesComparer);
            dddfileStateListAssert.AssertNumOfFiles(2);
            dddfileStateListAssert.AssertFileState(0, "dddScript_0001_DataForInvoiceTable1.sql", HashDiffType.Equal);
            dddfileStateListAssert.AssertFileState(1, "dddScript_0002_DataForTransTable1.sql", HashDiffType.Equal);
        }

        public override void Release(ITestContext testContext)
        {
            _projectConfigWithDBArrangeAndAssert.Release(testContext);
        }




    }
}
