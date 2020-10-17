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
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Files;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests
{
    [TestFixture]
    public class DeliveryEnv_Files_Tests
    {
        [SetUp]
        public void Init()
        {
            NinjectUtils_IntegrationTests.CreateKernel();
        }




        [Test]
        public void DeliveryEnv_Files_IncrementalChanged()
        {
            TestsRunner.RunTestsForeachDBType<DeliveryEnv_Files_IncrementalChanged_API, DeliveryEnv_Files_IncrementalChanged_All_CLI, DeliveryEnv_Files_IncrementalChanged_Incremental_CLI, DeliveryEnv_Files_IncrementalChanged_Repeatable_CLI, DeliveryEnv_Files_IncrementalChanged_DevDummyData_CLI>();
        }


        [Test]
        public void DeliveryEnv_Files_RepeatableChanged()
        {
            TestsRunner.RunTestsForeachDBType<DeliveryEnv_Files_RepeatableChanged_API, DeliveryEnv_Files_RepeatableChanged_All_CLI, DeliveryEnv_Files_RepeatableChanged_Incremental_CLI, DeliveryEnv_Files_RepeatableChanged_Repeatable_CLI, DeliveryEnv_Files_RepeatableChanged_DevDummyData_CLI>();
        }


    }
}
