using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Deploy;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_NewScrtiptFile;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Recreate;
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
    public class DevEnv_Deploy_Tests
    {
        [SetUp]
        public void Init()
        {
            NinjectUtils_IntegrationTests.CreateKernel();
        }




        [Test]
        public void DevEnv_Deploy()
        {
            TestsRunner.RunTest<DevEnv_Deploy_API, DevEnv_Deploy_CLI>(true, DBBackupFileType.MiddleState, ScriptFilesStateType.ValidScripts);
        }


      



    }
}
