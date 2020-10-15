using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.DbCommands.Contract;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI
{
    public class AssertTextByLines
    {
        private readonly string _testName;
        private readonly string _textType;
        private readonly List<string> _finalConsoleOutLines;
        private readonly int? _maxNumOfLines;

        private int _lineIndex;

        public AssertTextByLines(string testName, string textType, string text, int? maxNumOfLines)
        {
            _testName = testName;
            _textType = textType;
            _maxNumOfLines = maxNumOfLines;

            _finalConsoleOutLines = text.Trim(Environment.NewLine.ToCharArray()).Split(Environment.NewLine).ToList();


            _lineIndex = 0;
        }

        public void AssertLineMessage(string expectedMessage, bool isExact, int? forceLineIndex = null)
        {
            int lineIndex = _lineIndex;

            if (forceLineIndex.HasValue)
            {
                lineIndex = forceLineIndex.Value;
            }

            Assert.That(lineIndex < _finalConsoleOutLines.Count, $"{_testName}-> {_textType} -> Number of lines ({_finalConsoleOutLines.Count}) too small. should be at least: '{lineIndex + 1}'");

            if (isExact)
            {
                Assert.That(_finalConsoleOutLines[lineIndex] == expectedMessage, $"{_testName}-> {_textType} -> Final console message on line {lineIndex + 1} should be: '{expectedMessage}'. but was '{_finalConsoleOutLines[lineIndex]}'.");
            }
            else
            {
                Assert.That(_finalConsoleOutLines[lineIndex].Contains(expectedMessage), $"{_testName}-> {_textType} -> Final console message on line {lineIndex + 1} should be: '{expectedMessage}'. but was {_finalConsoleOutLines[lineIndex]}.");
            }



            if (!forceLineIndex.HasValue)
            {
                if (_maxNumOfLines.HasValue 
                    && _maxNumOfLines <= _lineIndex+1)
                {
                    Assert.That(_finalConsoleOutLines.Count == _maxNumOfLines, $"{_testName}-> {_textType} -> Invalid number of lines, should be: {_maxNumOfLines}, but was: {_finalConsoleOutLines.Count}");
                }


                _lineIndex++;
            }

        }



        public static void AssertEmpty(string testName, string textType, string text)
        {
            Assert.That(string.IsNullOrWhiteSpace(text), $"{testName}-> {textType} -> Should be empty, but was: {text}");
        }
    }
}
