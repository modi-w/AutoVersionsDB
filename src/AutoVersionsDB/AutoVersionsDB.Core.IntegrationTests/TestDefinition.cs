using AutoVersionsDB.Core.IntegrationTests.TestContexts;

namespace AutoVersionsDB.Core.IntegrationTests
{
    public abstract class TestDefinition
    {
        public abstract ITestContext Arrange(TestArgs testArgs);
        public abstract void Act(ITestContext testContext);
        public abstract void Asserts(ITestContext testContext);

        public abstract void Release(ITestContext testContext);
    }


    public abstract class TestDefinition<TTestContext> : TestDefinition
        where TTestContext : class, ITestContext
    {

        public override void Act(ITestContext testContext)
        {
            Act(testContext as TTestContext);
        }
        public abstract void Act(TTestContext testContext);


        public override void Asserts(ITestContext testContext)
        {
            Asserts(testContext as TTestContext);
        }
        public abstract void Asserts(TTestContext testContext);



        public override void Release(ITestContext testContext)
        {
            Release(testContext as TTestContext);
        }
        public abstract void Release(TTestContext testContext);


    }

}