using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
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
    public class DevEnv_Virtual_Tests
    {
        [SetUp]
        public void Init()
        {
            NinjectUtils_IntegrationTests.CreateKernel();
        }



        [Test]
        public void DevEnv_Virtual_EmptyDB()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_Virtual_EmptyDB_API, DevEnv_Virtual_EmptyDB_CLI>();
        }


        [Test]
        public void DevEnv_Virtual_EmptyDBExceptSystemTables()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_Virtual_EmptyDBWithSystemTables_API, DevEnv_Virtual_EmptyDBWithSystemTables_CLI>();
        }


        [Test]
        public void DevEnv_Virtual_MiddleState()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_Virtual_MiddleState_API, DevEnv_Virtual_MiddleState_API>();
        }


    }
}
