using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
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
    public class DeliveryEnv_SyncDB_Tests
    {
        [SetUp]
        public void Init()
        {
            NinjectUtils_IntegrationTests.CreateKernel();
        }




        [Test]
        public void DeliveryEnv_SyncDB_DBInMiddleState_WithDevDummyDataFiles()
        {
            TestsRunner.RunTest<DeliveryEnv_SyncDB_API, DeliveryEnv_SyncDB_CLI>(false, DBBackupFileType.MiddleState, ScriptFilesStateType.WithDevDummyDataFiles);
        }

        [Test]
        public void DeliveryEnv_SyncDB_DBInMiddleState_ValidScripts()
        {
            TestsRunner.RunTest<DeliveryEnv_SyncDB_API, DeliveryEnv_SyncDB_CLI>(false, DBBackupFileType.MiddleState, ScriptFilesStateType.ValidScripts);
        }

        [Test]
        public void DeliveryEnv_SyncDB_DBInFinalState_RepeatableChanged()
        {
            TestsRunner.RunTest<DeliveryEnv_SyncDB_API, DeliveryEnv_SyncDB_CLI>(false, DBBackupFileType.FinalState_DeliveryEnv, ScriptFilesStateType.RepeatableChanged);
        }


    }
}
