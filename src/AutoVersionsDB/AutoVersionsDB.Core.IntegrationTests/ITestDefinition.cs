using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;

namespace AutoVersionsDB.Core.IntegrationTests
{
    public abstract class TestDefinition
    {
        public abstract TestContext Arrange(TestArgs testArgs);
        public abstract void Act(TestContext testContext);
        public abstract void Asserts(TestContext testContext);

        public abstract void Release(TestContext testContext);
    }

    public abstract class TestDefinition<TTestContext> : TestDefinition
        where TTestContext : TestContext
    {



        public override void Act(TestContext testContext)
        {
            Act(testContext as TTestContext);
        }
        public abstract void Act(TTestContext testContext);


        public override void Asserts(TestContext testContext)
        {
            Asserts(testContext as TTestContext);
        }
        public abstract void Asserts(TTestContext testContext);



        public override void Release(TestContext testContext)
        {
            Release(testContext as TTestContext);
        }
        public abstract void Release(TTestContext testContext);


    }

}