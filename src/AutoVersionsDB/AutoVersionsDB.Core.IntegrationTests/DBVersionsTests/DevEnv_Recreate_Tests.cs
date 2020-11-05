using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_NewScrtiptFile;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Recreate;


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
    public class DevEnv_Recreate_Tests
    {
        [SetUp]
        public void Init()
        {
            NinjectUtils_IntegrationTests.CreateKernel();
        }




        [Test]
        public void DevEnv_Recreate_EmptyDB()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_Recreate_EmptyDB_API, DevEnv_Recreate_EmptyDB_CLI, DevEnv_Recreate_EmptyDB_UI>();
        }

        [Test]
        public void DevEnv_Recreate_DBInMiddleState()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_Recreate_DBInMiddleState_API, DevEnv_Recreate_DBInMiddleState_CLI, DevEnv_Recreate_DBInMiddleState_UI>();
        }



    }
}
