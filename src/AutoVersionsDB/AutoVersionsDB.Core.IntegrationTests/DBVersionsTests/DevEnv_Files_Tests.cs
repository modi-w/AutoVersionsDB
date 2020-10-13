using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_NewScrtiptFile;


using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Files;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests
{
    [TestFixture]
    public class DevEnv_Files_Tests
    {
        [SetUp]
        public void Init()
        {
            NinjectUtils_IntegrationTests.CreateKernel();
        }




        [Test]
        public void DevEnv_Files_IncrementalChanged()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_Files_IncrementalChanged_API, DevEnv_Files_IncrementalChanged_All_CLI, DevEnv_Files_IncrementalChanged_Incremental_CLI, DevEnv_Files_IncrementalChanged_Repeatable_CLI, DevEnv_Files_IncrementalChanged_DevDummyData_CLI>();
        }


        [Test]
        public void DevEnv_Files_RepeatableChanged()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_Files_RepeatableChanged_API, DevEnv_Files_RepeatableChanged_All_CLI, DevEnv_Files_RepeatableChanged_Incremental_CLI, DevEnv_Files_RepeatableChanged_Repeatable_CLI, DevEnv_Files_RepeatableChanged_DevDummyData_CLI>();
        }


    }
}
