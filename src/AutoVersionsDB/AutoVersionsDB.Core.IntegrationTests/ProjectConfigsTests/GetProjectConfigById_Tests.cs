using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetDBTypes;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectConfigById;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectsList;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests
{
    [TestFixture]
    public class GetProjectConfigById_Tests
    {
        [SetUp]
        public void Init()
        {
            NinjectUtils_IntegrationTests.CreateKernel();
        }



        [Test]
        public void GetProjectConfigById_DevEnv()
        {
            TestsRunner.RunTests<GetProjectConfigById_DevEnv_API, GetProjectConfigById_DevEnv_CLI>();
        }

        [Test]
        public void GetProjectConfigById_DeliveryEnv()
        {
            TestsRunner.RunTests<GetProjectConfigById_DeliveryEnv_API, GetProjectConfigById_DeliveryEnv_CLI>();
        }
    }
}
