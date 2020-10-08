﻿using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.Process;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsUtils;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
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
    public class DevEnv_Validations_Tests
    {
        private ProjectConfigsStorageHelper _projectConfigsStorageHelper;


        [SetUp]
        public void Init()
        {
            NinjectUtils_IntegrationTests.CreateKernel();
            _projectConfigsStorageHelper = NinjectUtils_IntegrationTests.NinjectKernelContainer.Get<ProjectConfigsStorageHelper>();
        }



        [Test]
        public void DevEnv_ProjectConfigValidation_NotValid()
        {
            TestsRunner.RunTests<DevEnv_ProjectConfigValidation_NotValid_API>();
        }


        [Test]
        public void DevEnv_ProjectConfigValidation_Valid()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_ProjectConfigValidation_Valid_API>();
        }


        [Test]
        public void DevEnv_Validate_Valid()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_Validate_Valid_API, DevEnv_Validate_Valid_CLI>();
        }


        [Test]
        public void DevEnv_Validate_HistoryExecutedFilesChanged()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_Validate_HistoryExecutedFilesChanged_API, DevEnv_Validate_HistoryExecutedFilesChanged_CLI>();
        }

        [Test]
        public void DevEnv_Validate_HistoryExecutedFileMissing()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_Validate_HistoryExecutedFilesMissing_API, DevEnv_Validate_HistoryExecutedFilesMissing_CLI>();
        }

        [Test]
        public void DevEnv_Validate_MissingSystemTables()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_Validate_MissingSystemTables_API, DevEnv_Validate_MissingSystemTables_CLI>();
        }

        [Test]
        public void DevEnv_Validate_TargetStateAlreadyExecuted_NotValid()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_Validate_TargetStateAlreadyExecuted_NotValid_API>();
        }

        [Test]
        public void DevEnv_Validate_TargetStateAlreadyExecuted_Valid()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_Validate_TargetStateAlreadyExecuted_Valid_API>();
        }




    }
}
