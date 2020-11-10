using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Init;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetDBTypes;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectsList;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Config;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Validations;

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
            TestsRunner.RunTests<DevEnv_ProjectConfigValidation_NotValid_API, DeliveryEnv_ProjectConfigValidation_NotValid_API>();
        }


        [Test]
        public void ProjectConfigValidation_Valid()
        {
            TestsRunner.RunTestsForeachDBType<DevEnv_ProjectConfigValidation_Valid_API, DeliveryEnv_ProjectConfigValidation_Valid_API>();
        }





    }
}
