using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.TestsUtils.Process;
using AutoVersionsDB.NotificationableEngine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.TestsUtils.Process
{
    public class ProcessAsserts
    {
        public void AssertProccessValid(string testName, ProcessTrace processTrace)
        {
            Assert.IsFalse(processTrace.HasError, $"{testName} >>> {processTrace.GetOnlyErrorsStatesLogAsString()}");
        }

        public void AssertProccessHasErrors(string testName, ProcessTrace processTrace)
        {
            Assert.That(processTrace.HasError, $"{testName} >>> Process is valid, should be an error");
        }

        public void AssertContainError(string testName, ProcessTrace processTrace, string errorCode)
        {
            Assert.That(processTrace.ContainErrorCode(errorCode), $"{testName} >>> The process trace results not contain the error code: '{errorCode}'");
        }
    }
}
