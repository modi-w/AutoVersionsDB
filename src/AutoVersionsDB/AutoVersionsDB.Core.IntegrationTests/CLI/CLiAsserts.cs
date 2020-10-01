using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests;
using AutoVersionsDB.Core.IntegrationTests.CLI;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.DbCommands.Contract;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.CLI
{
    public class CLiAsserts
    {

        public CLiAsserts()
        {
        }

        public void AssertConsoleFinalLineMessage(List<string> finalConsoleOutLines, int lineIndex, string expectedMessage)
        {
            Assert.That(finalConsoleOutLines[lineIndex] == expectedMessage, $"Final console message on line {lineIndex + 1} should be: '{expectedMessage}'. but was finalConsoleOutLines[0].");
        }


    }
}
