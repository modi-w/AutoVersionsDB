using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;


using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using Moq;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests
{
    [TestFixture]
    public class DeliveryEnv_NotAollowMethods_Tests
    {
        [SetUp]
        public void Init()
        {
            DIConfig.CreateKernel();
        }




        [Test]
        public void DeliveryEnv_NotAollowMethods_Deploy()
        {
            TestsRunner.RunTestsForeachDBType<DeliveryEnv_NotAllowMethods_Deploy_API, DeliveryEnv_NotAllowMethods_Deploy_CLI, DeliveryEnv_NotAllowMethods_Deploy_UI>();
        }


        [Test]
        public void DeliveryEnv_NotAollowMethods_Recreate()
        {
            TestsRunner.RunTestsForeachDBType<DeliveryEnv_NotAllowMethods_Recreate_API, DeliveryEnv_NotAllowMethods_Recreate_CLI, DeliveryEnv_NotAllowMethods_Recreate_UI>();
        }


        [Test]
        public void DeliveryEnv_NotAollowMethods_SetDBToSpecificState()
        {
            TestsRunner.RunTestsForeachDBType<DeliveryEnv_NotAllowMethods_SetDBToSpecificState_API, DeliveryEnv_NotAllowMethods_SetDBToSpecificState_UI>();
        }

        [Test]
        public void DeliveryEnv_NotAollowMethods_VirtualDDD()
        {
            TestsRunner.RunTestsForeachDBType<DeliveryEnv_NotAllowMethods_VirtualDDD_API, DeliveryEnv_NotAllowMethods_VirtualDDD_CLI, DeliveryEnv_NotAllowMethods_VirtualDDD_UI>();
        }

        [Test]
        public void DeliveryEnv_NotAollowMethods_InitDB()
        {
            TestsRunner.RunTestsForeachDBType<DeliveryEnv_NotAllowMethods_InitDB_API, DeliveryEnv_NotAllowMethods_InitDB_CLI, DeliveryEnv_NotAllowMethods_InitDB_UI>();
        }

        [Test]
        public void DeliveryEnv_NotAollowMethods_New_Incremental()
        {
            TestsRunner.RunTestsForeachDBType<DeliveryEnv_NotAllowMethods_New_Incremental_API, DeliveryEnv_NotAllowMethods_New_Incremental_CLI, DeliveryEnv_NotAllowMethods_New_Incremental_UI>();
        }
        [Test]
        public void DeliveryEnv_NotAollowMethods_New_Repeatable()
        {
            TestsRunner.RunTestsForeachDBType<DeliveryEnv_NotAllowMethods_New_Repeatable_API, DeliveryEnv_NotAllowMethods_New_Repeatable_CLI, DeliveryEnv_NotAllowMethods_New_Repeatable_UI>();
        }
        [Test]
        public void DeliveryEnv_NotAollowMethods_New_DevDummyData()
        {
            TestsRunner.RunTestsForeachDBType<DeliveryEnv_NotAllowMethods_New_DevDummyData_API, DeliveryEnv_NotAllowMethods_New_DevDummyData_CLI, DeliveryEnv_NotAllowMethods_New_DevDummyData_UI>();
        }




    }
}
