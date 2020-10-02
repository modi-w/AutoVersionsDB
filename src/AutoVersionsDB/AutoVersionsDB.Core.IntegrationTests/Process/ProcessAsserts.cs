﻿using AutoVersionsDB.NotificationableEngine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.Process
{
    public class ProcessAsserts
    {
        public void AssertProccessValid(string testName, ProcessTrace processTrace)
        {
            Assert.IsFalse(processTrace.HasError, $"{testName} -> {processTrace.GetOnlyErrorsHistoryAsString()}");
        }

        public void AssertProccessHasErrors(string testName, ProcessTrace processTrace)
        {
            Assert.That(processTrace.HasError, $"{testName} -> Process is valid, should be an error");
        }

        public void AssertContainError(string testName, ProcessTrace processTrace, string errorCode)
        {
            Assert.That(processTrace.ContainErrorCode(errorCode), $"{testName} -> The process trace results not contain the error code: '{errorCode}'");
        }
    }
}
