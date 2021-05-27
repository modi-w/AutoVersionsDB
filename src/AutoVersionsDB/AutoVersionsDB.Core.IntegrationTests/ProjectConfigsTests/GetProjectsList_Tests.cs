using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetDBTypes;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectsList;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests
{
    [TestFixture]
    public class GetProjectsList_Tests
    {
        [SetUp]
        public void Init()
        {
            DIConfig.CreateKernel();
        }



        [Test]
        public void GetProjectsList()
        {
            TestsRunner.RunTests<GetProjectsList_API, GetProjectsList_CLI>();
        }
    }
}
