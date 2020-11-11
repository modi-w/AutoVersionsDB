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

        public void AssertProperty(string testName, string propertyName, string expectedValue, string actualValue)
        {
            Assert.That(actualValue == expectedValue, $"{testName} -> {propertyName} should be: '{expectedValue}', but was: '{actualValue}'.");
        }

        public void AssertProperty(string testName, string propertyName, bool expectedValue, bool actualValue)
        {
            Assert.That(actualValue == expectedValue, $"{testName} -> {propertyName} should be: '{expectedValue}', but was: '{actualValue}'.");
        }

        public void AssertProperty(string testName, string propertyName, int expectedValue, int actualValue)
        {
            Assert.That(actualValue == expectedValue, $"{testName} -> {propertyName} should be: '{expectedValue}', but was: '{actualValue}'.");
        }


    }
}
