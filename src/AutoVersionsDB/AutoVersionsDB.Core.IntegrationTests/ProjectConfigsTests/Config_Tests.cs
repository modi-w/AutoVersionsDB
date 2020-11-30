using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Config;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetDBTypes;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectsList;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Init;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests
{
    [TestFixture]
    public class Config_Tests
    {
        [SetUp]
        public void Init()
        {
            DIConfig.CreateKernel();
        }



        [Test]
        public void Config_DevEnv()
        {
            TestsRunner.RunTests<Config_DevEnv_API, Config_DevEnv_CLI, Config_DevEnv_UI>();
        }

        [Test]
        public void Config_DeliveryEnv()
        {
            TestsRunner.RunTests<Config_DeliveryEnv_API, Config_DeliveryEnv_CLI, Config_DeliveryEnv_UI>();
        }



    }
}
