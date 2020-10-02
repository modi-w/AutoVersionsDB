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
            NinjectUtils_IntegrationTests.CreateKernel();
        }



        [Test]
        public void DeliveryEnv_ProjectConfigValidate_NotValid()
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




    }
}
