using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.ChangeProjectId;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetDBTypes;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectsList;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests
{
    [TestFixture]
    public class ChangeProjectId_Tests
    {
        [SetUp]
        public void Init()
        {
            NinjectUtils_IntegrationTests.CreateKernel();
        }



        [Test]
        public void ChangeProjectId()
        {
            TestsRunner.RunTests<ChangeProjectId_API, ChangeProjectId_CLI>();
        }
    }
}
