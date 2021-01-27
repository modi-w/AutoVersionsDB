using NUnit.Framework;

namespace AutoVersionsDB.Core.IntegrationTests.TestsUtils
{
    public class PropertiesAsserts
    {

        public virtual void AssertProperty(string testName, string propertyName, string actualValue, string expectedValue)
        {
            Assert.That(actualValue == expectedValue, $"{testName} >>> {propertyName} should be: '{expectedValue}', but was: '{actualValue}'.");
        }

        public virtual void AssertProperty(string testName, string propertyName, bool actualValue, bool expectedValue)
        {
            Assert.That(actualValue == expectedValue, $"{testName} >>> {propertyName} should be: '{expectedValue}', but was: '{actualValue}'.");
        }

        public virtual void AssertProperty(string testName, string propertyName, int actualValue, int expectedValue)
        {
            Assert.That(actualValue == expectedValue, $"{testName} >>> {propertyName} should be: '{expectedValue}', but was: '{actualValue}'.");
        }


    }
}
