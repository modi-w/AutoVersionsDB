using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_NewScrtiptFile;
using AutoVersionsDB.Core.IntegrationTests.Process;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests
{
    [TestFixture]
    public class DevEnv_NewScrtiptFile_Tests
    {
        [SetUp]
        public void Init()
        {
            NinjectUtils_IntegrationTests.CreateKernel();
        }




        [Test]
        public void DeliveryEnv_New_Incremental()
        {
            TestsRunner.RunTest<DevEnv_NewScrtiptFile_Incremental_API, DevEnv_NewScrtiptFile_Incremental_CLI>(true, DBBackupFileType.FinalState_DevEnv, ScriptFilesStateType.ValidScripts);
        }

        [Test]
        public void DeliveryEnv_New_Repeatable()
        {
            TestsRunner.RunTest<DevEnv_NewScrtiptFile_Repeatable_API, DevEnv_NewScrtiptFile_Repeatable_CLI>(true, DBBackupFileType.FinalState_DevEnv, ScriptFilesStateType.ValidScripts);
        }

        [Test]
        public void DeliveryEnv_New_DevDummyData()
        {
            TestsRunner.RunTest<DevEnv_NewScrtiptFile_DevDummyData_API, DevEnv_NewScrtiptFile_DevDummyData_CLI>(true, DBBackupFileType.FinalState_DevEnv, ScriptFilesStateType.ValidScripts);
        }


    }
}
