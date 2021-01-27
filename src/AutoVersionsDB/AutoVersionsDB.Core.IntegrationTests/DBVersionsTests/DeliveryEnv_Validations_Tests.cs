using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Validations;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ProjectConfigsUtils;
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
    public class DeliveryEnv_Validations_Tests
    {

        [SetUp]
        public void Init()
        {
            DIConfig.CreateKernel();
        }






        [Test]
        public void DeliveryEnv_Validate_Valid()
        {
            TestsRunner.RunTestsForeachDBType<DeliveryEnv_Validate_Valid_API, DeliveryEnv_Validate_Valid_CLI, DeliveryEnv_Validate_Valid_UI>();
        }


        [Test]
        public void DeliveryEnv_Validate_HistoryExecutedFilesChanged()
        {
            TestsRunner.RunTestsForeachDBType<DeliveryEnv_Validate_HistoryExecutedFilesChanged_API, DeliveryEnv_Validate_HistoryExecutedFilesChanged_CLI, DeliveryEnv_Validate_HistoryExecutedFilesChanged_UI>();
        }

        [Test]
        public void DeliveryEnv_Validate_HistoryExecutedFileMissing()
        {
            TestsRunner.RunTestsForeachDBType<DeliveryEnv_Validate_HistoryExecutedFilesMissing_API, DeliveryEnv_Validate_HistoryExecutedFilesMissing_CLI, DeliveryEnv_Validate_HistoryExecutedFilesMissing_UI>();
        }

        [Test]
        public void DeliveryEnv_Validate_NewProject()
        {
            TestsRunner.RunTestsForeachDBType<DeliveryEnv_Validate_NewProject_API, DeliveryEnv_Validate_NewProject_CLI, DeliveryEnv_Validate_NewProject_UI>();
        }

        [Test]
        public void DeliveryEnv_Validate_MissingSystemTables()
        {
            TestsRunner.RunTestsForeachDBType<DeliveryEnv_Validate_MissingSystemTables_API, DeliveryEnv_Validate_MissingSystemTables_CLI, DeliveryEnv_Validate_MissingSystemTables_UI>();
        }


        [Test]
        public void DeliveryEnv_Validate_ArtifactFile()
        {
            TestsRunner.RunTestsForeachDBType<DeliveryEnv_Validate_ArtifactFile_API, DeliveryEnv_Validate_ArtifactFile_CLI, DeliveryEnv_Validate_ArtifactFile_UI>();
        }

    }
}
