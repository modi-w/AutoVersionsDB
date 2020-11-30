using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;


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
    public class DeliveryEnv_Virtual_Tests
    {
        [SetUp]
        public void Init()
        {
            DIConfig.CreateKernel();
        }



        [Test]
        public void DeliveryEnv_Virtual_EmptyDB()
        {
            TestsRunner.RunTestsForeachDBType<DeliveryEnv_Virtual_EmptyDB_API, DeliveryEnv_Virtual_EmptyDB_CLI, DeliveryEnv_Virtual_EmptyDB_UI>();
        }


        [Test]
        public void DeliveryEnv_Virtual_EmptyDBExceptSystemTables()
        {
            TestsRunner.RunTestsForeachDBType<DeliveryEnv_Virtual_EmptyDBWithSystemTables_API, DeliveryEnv_Virtual_EmptyDBWithSystemTables_CLI, DeliveryEnv_Virtual_EmptyDBWithSystemTables_UI>();
        }


        [Test]
        public void DeliveryEnv_Virtual_MiddleState()
        {
            TestsRunner.RunTestsForeachDBType<DeliveryEnv_Virtual_MiddleState_API, DeliveryEnv_Virtual_MiddleState_CLI, DeliveryEnv_Virtual_MiddleState_UI>();
        }


    }
}
