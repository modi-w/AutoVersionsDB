using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.CLI;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.DbCommands.Contract;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.CLI
{
    public class AssertTextByLines
    {
        private readonly string _testName;
        private readonly string _textType;
        private readonly List<string> _finalConsoleOutLines;

        public AssertTextByLines(string testName, string textType, string text)
        {
            _testName = testName;
            _textType = textType;
            _finalConsoleOutLines = text.Split(Environment.NewLine).ToList();
        }

        public void AssertLineMessage(int lineIndex, string expectedMessage, bool isExact)
        {
            Assert.That(lineIndex < _finalConsoleOutLines.Count, $"{_testName}-> {_textType} -> Number of lines ({_finalConsoleOutLines.Count}) too small. should be at least: '{lineIndex + 1}'");
            if (isExact)
            {
                Assert.That(_finalConsoleOutLines[lineIndex] == expectedMessage, $"{_testName}-> {_textType} -> Final console message on line {lineIndex + 1} should be: '{expectedMessage}'. but was {_finalConsoleOutLines[lineIndex]}.");
            }
            else
            {
                Assert.That(_finalConsoleOutLines[lineIndex].Contains(expectedMessage), $"{_testName}-> {_textType} -> Final console message on line {lineIndex + 1} should be: '{expectedMessage}'. but was {_finalConsoleOutLines[lineIndex]}.");
            }
        }


    }
}
