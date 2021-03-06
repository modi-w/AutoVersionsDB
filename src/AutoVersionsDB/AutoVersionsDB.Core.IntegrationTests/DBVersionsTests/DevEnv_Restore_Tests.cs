﻿using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Restore;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Restore;


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
    public class DevEnv_Restore_Tests
    {
        [SetUp]
        public void Init()
        {
            DIConfig.CreateKernel();
        }



        [Test]
        public void DevEnv_Restore_SyncDB()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_Restore_SyncDB_API, DevEnv_Restore_SyncDB_CLI, DevEnv_Restore_SyncDB_UI>();
        }

        [Test]
        public void DevEnv_Restore_Recreate()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_Restore_Recreate_API, DevEnv_Restore_Recreate_CLI, DevEnv_Restore_Recreate_UI>();
        }


        [Test]
        public void DevEnv_Restore_SetDBToSpecificState()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_Restore_SetDBToSpecificState_API, DevEnv_Restore_SetDBToSpecificState_UI>();
        }


    }
}
