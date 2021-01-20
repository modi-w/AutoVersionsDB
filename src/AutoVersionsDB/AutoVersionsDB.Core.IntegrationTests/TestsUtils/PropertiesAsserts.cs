using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ProjectConfigsUtils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.TestsUtils
{
    public class PropertiesAsserts
    {

        public void AssertProperty(string testName, string propertyName, string actualValue, string expectedValue)
        {
            Assert.That(actualValue == expectedValue, $"{testName} >>> {propertyName} should be: '{expectedValue}', but was: '{actualValue}'.");
        }

        public void AssertProperty(string testName, string propertyName, bool actualValue, bool expectedValue)
        {
            Assert.That(actualValue == expectedValue, $"{testName} >>> {propertyName} should be: '{expectedValue}', but was: '{actualValue}'.");
        }

        public void AssertProperty(string testName, string propertyName, int actualValue, int expectedValue)
        {
            Assert.That(actualValue == expectedValue, $"{testName} >>> {propertyName} should be: '{expectedValue}', but was: '{actualValue}'.");
        }


    }
}
