using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.UI.DBVersions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts
{
    public class ScriptFilesListsStateAsserts
    {
        public void AssertDBVersionsViewModelDataCompleteSuccessfully(string testName, DBVersionsViewModelData dbVersionsViewModelData, bool isDevEnv)
        {
            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.IncrementalScriptFiles), dbVersionsViewModelData.IncrementalScriptFiles, 5);
            AssertFilesListHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles, HashDiffType.Equal);

            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.RepeatableScriptFiles), dbVersionsViewModelData.RepeatableScriptFiles, 2);
            AssertFilesListHashState(testName, dbVersionsViewModelData.RepeatableScriptFiles, HashDiffType.Equal);

            if (isDevEnv)
            {
                AssertFilesListSize(testName, nameof(dbVersionsViewModelData.DevDummyDataScriptFiles), dbVersionsViewModelData.DevDummyDataScriptFiles, 2);
                AssertFilesListHashState(testName, dbVersionsViewModelData.DevDummyDataScriptFiles, HashDiffType.Equal);
            }
            else
            {
                AssertFilesListSize(testName, nameof(dbVersionsViewModelData.DevDummyDataScriptFiles), dbVersionsViewModelData.DevDummyDataScriptFiles, 0);
            }
        }

        private void AssertFilesListSize(string testName, string listName, IList<RuntimeScriptFileBase> runtimeFiles, int targetSize)
        {
            Assert.That(runtimeFiles.Count == targetSize, $"{testName} -> {listName} size should be {targetSize}, but was '{runtimeFiles.Count}'");

        }

        private void AssertFilesListHashState(string testName, IEnumerable<RuntimeScriptFileBase> runtimeFiles, HashDiffType targetHashState)
        {
            foreach (var file in runtimeFiles)
            {
                AssertFileHashState(testName, file, targetHashState);
            }
        }

        private void AssertFileHashState(string testName, RuntimeScriptFileBase runtimeFile, HashDiffType targetHashState)
        {
            Assert.That(runtimeFile.HashDiffType == targetHashState, $"{testName} -> The {runtimeFile.ScriptFileType.FileTypeCode} file: '{runtimeFile.Filename}' should be '{targetHashState}' hash state, but was '{runtimeFile.HashDiffType}'");

        }
    }
}
