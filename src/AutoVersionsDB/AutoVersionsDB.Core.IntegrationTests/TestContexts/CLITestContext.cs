using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.TestContexts
{
    public class CLITestContext : ITestContext
    {
        private readonly ITestContext _testContext;

        private readonly StringBuilder _sbAllConsoleOut;
        private readonly StringBuilder _sbCurrentConsoleOut;

        private readonly StringBuilder _sbConsoleError;

        private int _lastLengthForCurrentConsoleOnLineAppended;

        public TestArgs TestArgs => _testContext.TestArgs;
        public ProjectConfigItem ProjectConfig => _testContext.ProjectConfig;


        public ProcessResults ProcessResults
        {
            get => _testContext.ProcessResults;
            set => _testContext.ProcessResults = value;
        }
        public object Result
        {
            get => _testContext.Result;
            set => _testContext.Result = value;
        }


        public DBBackupFileType DBBackupFileType => _testContext.DBBackupFileType;
        public NumOfDBConnections NumOfConnectionsBefore
        {
            get => _testContext.NumOfConnectionsBefore;
            set => _testContext.NumOfConnectionsBefore = value;
        }

        public ScriptFilesStateType ScriptFilesStateType => _testContext.ScriptFilesStateType;




        public string AllConsoleOut => _sbAllConsoleOut.ToString();
        public string FinalConsoleOut => _sbCurrentConsoleOut.ToString();

        public string ConsoleError => _sbConsoleError.ToString();


        public CLITestContext(ITestContext testContext)
        {
            _testContext = testContext;

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

        public void ClearProcessData()
        {
            _testContext.ClearProcessData();

            _sbAllConsoleOut.Clear();
            _sbConsoleError.Clear();
            _sbCurrentConsoleOut.Clear();

            _lastLengthForCurrentConsoleOnLineAppended = 0;
        }


    }
}
