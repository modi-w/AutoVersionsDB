using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Files;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ScriptFiles
{
    public class FileStateListAssert
    {
        private string _testName;
        private ScriptFilesComparerBase _scriptFilesComparer;

        public FileStateListAssert(string testName, ScriptFilesComparerBase scriptFilesComparer)
        {
            _testName = testName;
            _scriptFilesComparer = scriptFilesComparer;
        }

        public void AssertNumOfFiles(int numOfFiles)
        {
            Assert.That(_scriptFilesComparer.AllFileSystemScriptFiles.Count == numOfFiles, $"{_testName} -> Num of {_scriptFilesComparer.ScriptFileType.FileTypeCode} should be: '{numOfFiles}' but was: '{_scriptFilesComparer.AllFileSystemScriptFiles.Count}'");
        }


        public void AssertFileState(int fileIndex, string expectedFilename, HashDiffType expectedSyncState)
        {
            RuntimeScriptFileBase scriptFileState = _scriptFilesComparer.AllFileSystemScriptFiles[fileIndex];

            Assert.That(scriptFileState.Filename == expectedFilename, $"{_testName} -> The {fileIndex + 1}st {_scriptFilesComparer.ScriptFileType.FileTypeCode} file should be: '{expectedFilename}' but was: '{scriptFileState.Filename}'");
            Assert.That(scriptFileState.HashDiffType == expectedSyncState, $"{_testName} -> The {fileIndex + 1}st {_scriptFilesComparer.ScriptFileType.FileTypeCode} SyncType should be: '{expectedSyncState}' but was: '{scriptFileState.HashDiffType}'");
        }
    }
}
