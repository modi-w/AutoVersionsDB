using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.UI.DBVersions;
using AutoVersionsDB.UI.Notifications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.TestsUtils.UI
{
    public class PropertiesAsserts
    {






        public void AssertPropertyState(string testName, string propertyName, bool actualValue, bool expectedValue)
        {
            Assert.That(actualValue == expectedValue, $"{testName} -> {propertyName} should be '{expectedValue}', but was '{actualValue}'");
        }
        public void AssertPropertyState(string testName, string propertyName, string actualValue, string expectedValue)
        {
            Assert.That(actualValue == expectedValue, $"{testName} -> {propertyName} should be '{expectedValue}', but was '{actualValue}'");
        }




        

    }
}
