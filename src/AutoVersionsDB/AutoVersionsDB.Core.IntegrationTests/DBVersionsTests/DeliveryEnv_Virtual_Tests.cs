using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
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
    public class DeliveryEnv_Virtual_Tests
    {
        [SetUp]
        public void Init()
        {
            NinjectUtils_IntegrationTests.CreateKernel();
        }



        [Test]
        public void DeliveryEnv_Virtual_EmptyDB()
        {
            TestsRunner.RunTest<DeliveryEnv_Virtual_API, DeliveryEnv_Virtual_CLI>(false, DBBackupFileType.EmptyDB, ScriptFilesStateType.ValidScripts);
        }


        [Test]
        public void DeliveryEnv_Virtual_EmptyDBExceptSystemTables()
        {
            TestsRunner.RunTest<DeliveryEnv_Virtual_API, DeliveryEnv_Virtual_CLI>(false, DBBackupFileType.EmptyDB_ExceptSystemTables, ScriptFilesStateType.ValidScripts);
        }


        [Test]
        public void DeliveryEnv_Virtual_MiddleState()
        {
            TestsRunner.RunTest<DeliveryEnv_Virtual_API, DeliveryEnv_Virtual_CLI>(false, DBBackupFileType.MiddleState, ScriptFilesStateType.ValidScripts);
        }


    }
}
