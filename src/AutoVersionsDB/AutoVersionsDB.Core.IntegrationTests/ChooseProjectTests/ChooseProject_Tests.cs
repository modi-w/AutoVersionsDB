using AutoVersionsDB.Core.IntegrationTests.ChooseProjectTests.TestDefinitions.SerchProjectText;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ChooseProjectTests
{
    [TestFixture]
    public class ChooseProject_Tests
    {
        [SetUp]
        public void Init()
        {
            NinjectUtils_IntegrationTests.CreateKernel();
        }



        [Test]
        public void ChangeProjectId()
        {
            TestsRunner.RunTests<SerchProjectText_UI>();
        }

        [Test]
        public void DeleteProject()
        {
            TestsRunner.RunTests<DeleteProject_UI>();
        }
        
    }
}
