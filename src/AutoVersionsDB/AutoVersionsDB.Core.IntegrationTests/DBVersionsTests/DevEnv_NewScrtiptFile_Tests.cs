﻿using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_NewScrtiptFile;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
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
    public class DevEnv_NewScrtiptFile_Tests
    {
        [SetUp]
        public void Init()
        {
            DIConfig.CreateKernel();
        }




        [Test]
        public void DevEnv_New_Incremental()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_NewScrtiptFile_Incremental_API, DevEnv_NewScrtiptFile_Incremental_CLI, DevEnv_NewScrtiptFile_Incremental_UI>();
        }

        [Test]
        public void DevEnv_New_Repeatable()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_NewScrtiptFile_Repeatable_API, DevEnv_NewScrtiptFile_Repeatable_CLI, DevEnv_NewScrtiptFile_Repeatable_UI>();
        }

        [Test]
        public void DevEnv_New_DevDummyData()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_NewScrtiptFile_DevDummyData_API, DevEnv_NewScrtiptFile_DevDummyData_CLI, DevEnv_NewScrtiptFile_DevDummyData_UI>();
        }


    }
}
