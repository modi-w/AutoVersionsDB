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
    public class Init_Tests
    {
        [SetUp]
        public void Init()
        {
            NinjectUtils_IntegrationTests.CreateKernel();
        }



        [Test]
        public void Init_DevEnv_WithDefaults()
        {
            TestsRunner.RunTests<Init_DevEnv_WithDefaults_API, Init_DevEnv_WithDefaults_CLI>();
        }

        [Test]
        public void Init_DevEnv_AllProperties()
        {
            TestsRunner.RunTests<Init_DevEnv_AllProperties_API, Init_DevEnv_AllProperties_CLI, Init_DevEnv_AllProperties_UI>();
        }


        [Test]
        public void Init_DeliveryEnv_WithDefaults()
        {
            TestsRunner.RunTests<Init_DeliveryEnv_WithDefaults_API, Init_DeliveryEnv_WithDefaults_CLI>();
        }

        [Test]
        public void Init_DeliveryEnv_AllProperties()
        {
            TestsRunner.RunTests<Init_DeliveryEnv_AllProperties_API, Init_DeliveryEnv_AllProperties_CLI, Init_DeliveryEnv_AllProperties_UI>();
        }


    }
}
