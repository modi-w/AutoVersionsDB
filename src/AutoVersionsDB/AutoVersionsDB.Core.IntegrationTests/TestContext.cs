using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;

using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests
{

    public class TestContext
    {
        private StringBuilder _sbAllConsoleOut;
        private StringBuilder _sbCurrentConsoleOut;

        private StringBuilder _sbConsoleError;

        private int _lastLengthForCurrentConsoleOnLineAppended;

        public string AllConsoleOut => _sbAllConsoleOut.ToString();
        public string FinalConsoleOut => _sbCurrentConsoleOut.ToString();

        public string ConsoleError => _sbConsoleError.ToString();


        public object Result { get; set; }
        public ProcessResults ProcessResults { get; set; }


        public TestArgs TestArgs { get; }


        public TestContext(TestArgs testArgs)
        {
            TestArgs = testArgs;

            _sbAllConsoleOut = new StringBuilder();
            _sbCurrentConsoleOut = new StringBuilder();
            _sbConsoleError = new StringBuilder();
        }

        public void AppendToConsoleOut(string str)
        {
            _sbAllConsoleOut.Append(str);
            _sbCurrentConsoleOut.Append(str);

            if (str == Environment.NewLine)
            {
                _lastLengthForCurrentConsoleOnLineAppended = _sbCurrentConsoleOut.Length;
            }
        }

        public void ClearCurrentMessage(int left)
        {
            if ((_sbCurrentConsoleOut.Length - (_lastLengthForCurrentConsoleOnLineAppended + left)) > 0)
            {
                _sbCurrentConsoleOut.Remove((_lastLengthForCurrentConsoleOnLineAppended + left), _sbCurrentConsoleOut.Length - (_lastLengthForCurrentConsoleOnLineAppended + left));
            }
        }


        public void AppendToConsoleError(string str)
        {
            _sbConsoleError.Append(str);
        }


        public virtual void ClearProcessData()
        {
            _sbAllConsoleOut.Clear();
            _sbConsoleError.Clear();
            _sbCurrentConsoleOut.Clear();

            _lastLengthForCurrentConsoleOnLineAppended = 0;

            this.Result = null;
            this.ProcessResults = null;

        }

    }

    public class TestContext<TArgs> : TestContext
        where TArgs : TestArgs
    {

        public TArgs TestArgs => base.TestArgs as TArgs;

        public TestContext(TArgs testArgs)
            : base(testArgs)
        {

        }
    }

}
