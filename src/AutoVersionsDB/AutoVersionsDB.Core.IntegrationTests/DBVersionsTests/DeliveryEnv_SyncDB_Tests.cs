using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;


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
        public void DeliveryEnv_SyncDB_DBInMiddleState()
        {
            TestsRunner.RunTestsForeachDBType<DeliveryEnv_SyncDB_DBInMiddleState_API, DeliveryEnv_SyncDB_DBInMiddleState_CLI, DeliveryEnv_SyncDB_DBInMiddleState_UI>();
        }


        [Test]
        public void DeliveryEnv_SyncDB_DBInMiddleState_WithDevDummyDataFiles()
        {
            TestsRunner.RunTestsForeachDBType<DeliveryEnv_SyncDB_DBInMiddleState_WithDevDummyDataFiles_API, DeliveryEnv_SyncDB_DBInMiddleState_WithDevDummyDataFiles_CLI, DeliveryEnv_SyncDB_DBInMiddleState_WithDevDummyDataFiles_UI>();
        }

    

        [Test]
        public void DeliveryEnv_SyncDB_DBInFinalState_RepeatableChanged()
        {
            TestsRunner.RunTestsForeachDBType<DeliveryEnv_SyncDB_DBInFinalState_RepeatableChanged_API, DeliveryEnv_SyncDB_DBInFinalState_RepeatableChanged_CLI, DeliveryEnv_SyncDB_DBInFinalState_RepeatableChanged_UI>();
        }


    }
}
