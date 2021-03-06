﻿using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;


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
    public class DevEnv_Virtual_Tests
    {
        [SetUp]
        public void Init()
        {
            DIConfig.CreateKernel();
        }



        [Test]
        public void DevEnv_Virtual_EmptyDB()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_Virtual_EmptyDB_API, DevEnv_Virtual_EmptyDB_CLI, DevEnv_Virtual_EmptyDB_UI>();
        }


        [Test]
        public void DevEnv_Virtual_EmptyDBExceptSystemTables()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_Virtual_EmptyDBWithSystemTables_API, DevEnv_Virtual_EmptyDBWithSystemTables_CLI, DevEnv_Virtual_EmptyDBWithSystemTables_UI>();
        }


        [Test]
        public void DevEnv_Virtual_MiddleState()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_Virtual_MiddleState_API, DevEnv_Virtual_MiddleState_CLI, DevEnv_Virtual_MiddleState_UI>();
        }


    }
}
