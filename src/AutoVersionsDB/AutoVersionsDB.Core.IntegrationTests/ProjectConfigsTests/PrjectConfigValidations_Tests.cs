using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Config;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetDBTypes;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectsList;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Init;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Validations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests
{
    [TestFixture]
    public class PrjectConfigValidations_Tests
    {
        [SetUp]
        public void Init()
        {
            NinjectUtils_IntegrationTests.CreateKernel();
        }



        [Test]
        public void ProjectConfigValidation_NotValid()
        {
            TestsRunner.RunTests<ProjectConfigValidation_DevEnv_NotValid_API, ProjectConfigValidation_DevEnv_NotValid_UI, ProjectConfigValidation_DeliveryEnv_NotValid_API, ProjectConfigValidation_DeliveryEnv_NotValid_UI>();
        }


        [Test]
        public void ProjectConfigValidation_Valid()
        {
            TestsRunner.RunTestsForeachDBType<ProjectConfigValidation_DevEnv_Valid_API, ProjectConfigValidation_DevEnv_Valid_UI, ProjectConfigValidation_DeliveryEnv_Valid_API, ProjectConfigValidation_DeliveryEnv_Valid_UI>();
        }





    }
}
