using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.Process;
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
        [SetUp]
        public void Init()
        {
            NinjectUtils_IntegrationTests.CreateKernel();
        }



        [Test]
        //Comment: For this test, we dont have value in DBType, so we dont need to use ProjectConfigsFactory (that create project config for each DBType);
        //          And we dont have CLI Command for ProjectConfigValidation,
        //          So we dont need to use TestsRunner.
        public void DevEnv_ProjectConfigValidation_NotValid()
        {
            //Arrange
            ProjectConfigItem projectConfig = new ProjectConfigItem()
            {
                Id = IntegrationTestsConsts.TestProjectId,
                DevEnvironment = false
            };

            MockObjectsProvider.MockProjectConfigsStorage.Setup(m => m.GetProjectConfigById(It.IsAny<string>())).Returns(projectConfig);



            //Act
            ProcessResults processResults = AutoVersionsDbAPI.ValidateProjectConfig(projectConfig.Id, null);


            //Assert
            ProcessAsserts processAsserts = NinjectUtils_IntegrationTests.NinjectKernelContainer.Get<ProcessAsserts>();

            processAsserts.AssertContainError(this.GetType().Name, processResults.Trace, "DBType");
            processAsserts.AssertContainError(this.GetType().Name, processResults.Trace, "DBName");
            processAsserts.AssertContainError(this.GetType().Name, processResults.Trace, "DBBackupFolderPath");
            processAsserts.AssertContainError(this.GetType().Name, processResults.Trace, "DeliveryArtifactFolderPath");
        }


        [Test]
        public void DevEnv_ProjectConfigValidation_Valid()
        {
            TestsRunner.RunTest<DevEnv_ProjectConfigValidation_API>(true, DBBackupFileType.EmptyDB, ScriptFilesStateType.ValidScripts);
        }


        [Test]
        public void DevEnv_Validate_Valid()
        {
            TestsRunner.RunTest<DevEnv_Validate_Valid_API, DevEnv_Validate_Valid_CLI>(true, DBBackupFileType.MiddleState, ScriptFilesStateType.ValidScripts);
        }


        [Test]
        public void DevEnv_Validate_HistoryExecutedFilesChanged()
        {
            TestsRunner.RunTest<DevEnv_Validate_HistoryExecutedFilesChanged_API, DevEnv_Validate_HistoryExecutedFilesChanged_CLI>(true, DBBackupFileType.FinalState_DevEnv, ScriptFilesStateType.IncrementalChanged);
        }

        [Test]
        public void DevEnv_Validate_HistoryExecutedFileMissing()
        {
            TestsRunner.RunTest<DevEnv_Validate_HistoryExecutedFilesChanged_API, DevEnv_Validate_HistoryExecutedFilesChanged_CLI>(true, DBBackupFileType.FinalState_DevEnv, ScriptFilesStateType.MissingFile);
        }

        [Test]
        public void DevEnv_Validate_MissingSystemTables()
        {
            TestsRunner.RunTest<DevEnv_Validate_MissingSystemTables_API, DevEnv_Validate_MissingSystemTables_CLI>(true, DBBackupFileType.FinalState_MissingSystemTables, ScriptFilesStateType.ValidScripts);
        }

        [Test]
        public void DevEnv_Validate_TargetStateAlreadyExecuted_NotValid()
        {
            TestsRunner.RunTest<DevEnv_Validate_TargetStateAlreadyExecuted_NotValid_API>(true, DBBackupFileType.FinalState_DevEnv, ScriptFilesStateType.ValidScripts);
        }

        [Test]
        public void DevEnv_Validate_TargetStateAlreadyExecuted_Valid()
        {
            TestsRunner.RunTest<DevEnv_Validate_TargetStateAlreadyExecuted_Valid_API>(true, DBBackupFileType.MiddleState, ScriptFilesStateType.ValidScripts);
        }




    }
}
