using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
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

        private int _lastLengthForCurrentConsoleOnLineAppended;

        public string AllConsoleOut => _sbAllConsoleOut.ToString();
        public string FinalConsoleOut => _sbCurrentConsoleOut.ToString();

        public DBBackupFileType DBBackupFileType { get; }
        public ScriptFilesStateType ScriptFilesStateType { get; }
        public ProjectConfigItem ProjectConfig { get; }
        public NumOfConnections NumOfConnectionsBefore { get; set; }
        public ProcessResults ProcessResults { get; set; }


        public TestContext(DBBackupFileType dbBackupFileType, ScriptFilesStateType scriptFilesStateType, ProjectConfigItem projectConfig)
        {
            DBBackupFileType = dbBackupFileType;
            ScriptFilesStateType = scriptFilesStateType;
            ProjectConfig = projectConfig;

            _sbAllConsoleOut = new StringBuilder();
            _sbCurrentConsoleOut = new StringBuilder();
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
        //public void AppendLineToConsoleOut(string str)
        //{
        //    _sbAllConsoleOut.AppendLine(str);
        //    _sbCurrentConsoleOut.AppendLine(str);

        //}

        public void ClearCurrentMessage(int left)
        {
            if ((_sbCurrentConsoleOut.Length - (_lastLengthForCurrentConsoleOnLineAppended + left)) > 0)
            {
                _sbCurrentConsoleOut.Remove((_lastLengthForCurrentConsoleOnLineAppended + left), _sbCurrentConsoleOut.Length - (_lastLengthForCurrentConsoleOnLineAppended + left));
            }
        }

    }
}
