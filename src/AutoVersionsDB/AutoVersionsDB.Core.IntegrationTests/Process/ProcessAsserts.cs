using AutoVersionsDB.NotificationableEngine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.Process
{
    public class ProcessAsserts
    {
        public void AssertProccessErrors(string testName, ProcessTrace processResults)
        {
            Assert.IsFalse(processResults.HasError, $"{testName} -> {processResults.GetOnlyErrorsHistoryAsString()}");
        }

    }
}
