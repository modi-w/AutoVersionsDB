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
        public virtual void  AssertDBVersionsViewModelDataDBFinalState(string testName, DBVersionsViewModelData dbVersionsViewModelData, bool isDevEnv)
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


        public virtual void AssertDBVersionsViewModelDataDBFinalStateVrDDD(string testName, DBVersionsViewModelData dbVersionsViewModelData)
        {
            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.IncrementalScriptFiles), dbVersionsViewModelData.IncrementalScriptFiles, 5);
            AssertFilesListHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles, HashDiffType.Equal);

            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.RepeatableScriptFiles), dbVersionsViewModelData.RepeatableScriptFiles, 2);
            AssertFilesListHashState(testName, dbVersionsViewModelData.RepeatableScriptFiles, HashDiffType.Equal);

            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.DevDummyDataScriptFiles), dbVersionsViewModelData.DevDummyDataScriptFiles, 2);
            AssertFilesListHashState(testName, dbVersionsViewModelData.DevDummyDataScriptFiles, HashDiffType.EqualVirtual);
        }

        public virtual void AssertDBVersionsViewModelDataDBFinalStateVirtual(string testName, DBVersionsViewModelData dbVersionsViewModelData, bool isDevEnv)
        {
            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.IncrementalScriptFiles), dbVersionsViewModelData.IncrementalScriptFiles, 5);
            AssertFilesListHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles, HashDiffType.EqualVirtual);

            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.RepeatableScriptFiles), dbVersionsViewModelData.RepeatableScriptFiles, 2);
            AssertFilesListHashState(testName, dbVersionsViewModelData.RepeatableScriptFiles, HashDiffType.EqualVirtual);

            if (isDevEnv)
            {
                AssertFilesListSize(testName, nameof(dbVersionsViewModelData.DevDummyDataScriptFiles), dbVersionsViewModelData.DevDummyDataScriptFiles, 2);
                AssertFilesListHashState(testName, dbVersionsViewModelData.DevDummyDataScriptFiles, HashDiffType.EqualVirtual);
            }
            else
            {
                AssertFilesListSize(testName, nameof(dbVersionsViewModelData.DevDummyDataScriptFiles), dbVersionsViewModelData.DevDummyDataScriptFiles, 0);
            }
        }

        public virtual void  AssertDBVersionsViewModelDataMiddleState(string testName, DBVersionsViewModelData dbVersionsViewModelData, bool isDevEnv)
        {
            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.IncrementalScriptFiles), dbVersionsViewModelData.IncrementalScriptFiles, 5);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[0], HashDiffType.Equal);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[1], HashDiffType.Equal);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[2], HashDiffType.Equal);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[3], HashDiffType.NotExist);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[4], HashDiffType.NotExist);

            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.RepeatableScriptFiles), dbVersionsViewModelData.RepeatableScriptFiles, 2);
            AssertFileHashState(testName, dbVersionsViewModelData.RepeatableScriptFiles[0], HashDiffType.NotExist);
            AssertFileHashState(testName, dbVersionsViewModelData.RepeatableScriptFiles[1], HashDiffType.NotExist);

            if (isDevEnv)
            {
                AssertFilesListSize(testName, nameof(dbVersionsViewModelData.DevDummyDataScriptFiles), dbVersionsViewModelData.DevDummyDataScriptFiles, 2);
                AssertFilesListHashState(testName, dbVersionsViewModelData.DevDummyDataScriptFiles, HashDiffType.NotExist);
            }
            else
            {
                AssertFilesListSize(testName, nameof(dbVersionsViewModelData.DevDummyDataScriptFiles), dbVersionsViewModelData.DevDummyDataScriptFiles, 0);
            }
        }

        public virtual void  AssertDBVersionsViewModelDataMiddleStateVirtual(string testName, DBVersionsViewModelData dbVersionsViewModelData, bool isDevEnv)
        {
            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.IncrementalScriptFiles), dbVersionsViewModelData.IncrementalScriptFiles, 5);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[0], HashDiffType.EqualVirtual);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[1], HashDiffType.EqualVirtual);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[2], HashDiffType.EqualVirtual);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[3], HashDiffType.NotExist);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[4], HashDiffType.NotExist);

            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.RepeatableScriptFiles), dbVersionsViewModelData.RepeatableScriptFiles, 2);
            AssertFileHashState(testName, dbVersionsViewModelData.RepeatableScriptFiles[0], HashDiffType.NotExist);
            AssertFileHashState(testName, dbVersionsViewModelData.RepeatableScriptFiles[1], HashDiffType.NotExist);

            if (isDevEnv)
            {
                AssertFilesListSize(testName, nameof(dbVersionsViewModelData.DevDummyDataScriptFiles), dbVersionsViewModelData.DevDummyDataScriptFiles, 2);
                AssertFilesListHashState(testName, dbVersionsViewModelData.DevDummyDataScriptFiles, HashDiffType.NotExist);
            }
            else
            {
                AssertFilesListSize(testName, nameof(dbVersionsViewModelData.DevDummyDataScriptFiles), dbVersionsViewModelData.DevDummyDataScriptFiles, 0);
            }
        }





        public virtual void  AssertDBVersionsViewModelDataNewIncScriptsFiles(string testName, DBVersionsViewModelData dbVersionsViewModelData, bool isDevEnv)
        {
            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.IncrementalScriptFiles), dbVersionsViewModelData.IncrementalScriptFiles, 7);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[0], HashDiffType.Equal);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[1], HashDiffType.Equal);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[2], HashDiffType.Equal);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[3], HashDiffType.Equal);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[4], HashDiffType.Equal);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[5], HashDiffType.NotExist);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[6], HashDiffType.NotExist);

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

        public virtual void  AssertDBVersionsViewModelDataNewRptScriptFile(string testName, DBVersionsViewModelData dbVersionsViewModelData, bool isDevEnv)
        {
            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.IncrementalScriptFiles), dbVersionsViewModelData.IncrementalScriptFiles, 5);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[0], HashDiffType.Equal);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[1], HashDiffType.Equal);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[2], HashDiffType.Equal);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[3], HashDiffType.Equal);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[4], HashDiffType.Equal);

            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.RepeatableScriptFiles), dbVersionsViewModelData.RepeatableScriptFiles, 3);
            AssertFileHashState(testName, dbVersionsViewModelData.RepeatableScriptFiles[0], HashDiffType.Equal);
            AssertFileHashState(testName, dbVersionsViewModelData.RepeatableScriptFiles[1], HashDiffType.Equal);
            AssertFileHashState(testName, dbVersionsViewModelData.RepeatableScriptFiles[2], HashDiffType.NotExist);

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
        public virtual void  AssertDBVersionsViewModelDataNewDDDScriptFile(string testName, DBVersionsViewModelData dbVersionsViewModelData, bool isDevEnv)
        {
            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.IncrementalScriptFiles), dbVersionsViewModelData.IncrementalScriptFiles, 5);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[0], HashDiffType.Equal);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[1], HashDiffType.Equal);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[2], HashDiffType.Equal);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[3], HashDiffType.Equal);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[4], HashDiffType.Equal);

            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.RepeatableScriptFiles), dbVersionsViewModelData.RepeatableScriptFiles, 2);
            AssertFilesListHashState(testName, dbVersionsViewModelData.RepeatableScriptFiles, HashDiffType.Equal);

            if (isDevEnv)
            {
                AssertFilesListSize(testName, nameof(dbVersionsViewModelData.DevDummyDataScriptFiles), dbVersionsViewModelData.DevDummyDataScriptFiles, 3);
                AssertFileHashState(testName, dbVersionsViewModelData.DevDummyDataScriptFiles[0], HashDiffType.Equal);
                AssertFileHashState(testName, dbVersionsViewModelData.DevDummyDataScriptFiles[1], HashDiffType.Equal);
                AssertFileHashState(testName, dbVersionsViewModelData.DevDummyDataScriptFiles[2], HashDiffType.NotExist);
            }
            else
            {
                AssertFilesListSize(testName, nameof(dbVersionsViewModelData.DevDummyDataScriptFiles), dbVersionsViewModelData.DevDummyDataScriptFiles, 0);
            }
        }


        public virtual void AssertDBVersionsViewModelDataRepeatableChanged(string testName, DBVersionsViewModelData dbVersionsViewModelData, bool isDevEnv)
        {
            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.IncrementalScriptFiles), dbVersionsViewModelData.IncrementalScriptFiles, 5);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[0], HashDiffType.Equal);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[1], HashDiffType.Equal);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[2], HashDiffType.Equal);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[3], HashDiffType.Equal);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[4], HashDiffType.Equal);

            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.RepeatableScriptFiles), dbVersionsViewModelData.RepeatableScriptFiles, 2);
            AssertFileHashState(testName, dbVersionsViewModelData.RepeatableScriptFiles[0], HashDiffType.Different);
            AssertFileHashState(testName, dbVersionsViewModelData.RepeatableScriptFiles[1], HashDiffType.Equal);

            if (isDevEnv)
            {
                AssertFilesListSize(testName, nameof(dbVersionsViewModelData.DevDummyDataScriptFiles), dbVersionsViewModelData.DevDummyDataScriptFiles, 2);
                AssertFileHashState(testName, dbVersionsViewModelData.DevDummyDataScriptFiles[0], HashDiffType.Equal);
                AssertFileHashState(testName, dbVersionsViewModelData.DevDummyDataScriptFiles[1], HashDiffType.Equal);
            }
            else
            {
                AssertFilesListSize(testName, nameof(dbVersionsViewModelData.DevDummyDataScriptFiles), dbVersionsViewModelData.DevDummyDataScriptFiles, 0);
            }
        }

        public virtual void  AssertDBVersionsViewModelDataIncrementalChanged(string testName, DBVersionsViewModelData dbVersionsViewModelData, bool isDevEnv)
        {
            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.IncrementalScriptFiles), dbVersionsViewModelData.IncrementalScriptFiles, 5);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[0], HashDiffType.Equal);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[1], HashDiffType.Equal);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[2], HashDiffType.Different);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[3], HashDiffType.NotExist);
            AssertFileHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles[4], HashDiffType.NotExist);

            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.RepeatableScriptFiles), dbVersionsViewModelData.RepeatableScriptFiles, 2);
            AssertFileHashState(testName, dbVersionsViewModelData.RepeatableScriptFiles[0], HashDiffType.NotExist);
            AssertFileHashState(testName, dbVersionsViewModelData.RepeatableScriptFiles[1], HashDiffType.NotExist);

            if (isDevEnv)
            {
                AssertFilesListSize(testName, nameof(dbVersionsViewModelData.DevDummyDataScriptFiles), dbVersionsViewModelData.DevDummyDataScriptFiles, 2);
                AssertFilesListHashState(testName, dbVersionsViewModelData.DevDummyDataScriptFiles, HashDiffType.NotExist);
            }
            else
            {
                AssertFilesListSize(testName, nameof(dbVersionsViewModelData.DevDummyDataScriptFiles), dbVersionsViewModelData.DevDummyDataScriptFiles, 0);
            }
        }
        public virtual void  AssertDBVersionsViewModelDataIncrementalMissing(string testName, DBVersionsViewModelData dbVersionsViewModelData, bool isDevEnv)
        {
            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.IncrementalScriptFiles), dbVersionsViewModelData.IncrementalScriptFiles, 4);
            AssertFilesListHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles, HashDiffType.Equal);

            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.RepeatableScriptFiles), dbVersionsViewModelData.RepeatableScriptFiles, 2);
            AssertFilesListHashState(testName, dbVersionsViewModelData.RepeatableScriptFiles, HashDiffType.Equal);

            if (isDevEnv)
            {
                AssertFilesListSize(testName, nameof(dbVersionsViewModelData.DevDummyDataScriptFiles), dbVersionsViewModelData.DevDummyDataScriptFiles, 2);
                AssertFilesListHashState(testName, dbVersionsViewModelData.RepeatableScriptFiles, HashDiffType.Equal);
            }
            else
            {
                AssertFilesListSize(testName, nameof(dbVersionsViewModelData.DevDummyDataScriptFiles), dbVersionsViewModelData.DevDummyDataScriptFiles, 0);
            }
        }

        public virtual void AssertDBVersionsViewModelDataNoFiles(string testName, DBVersionsViewModelData dbVersionsViewModelData)
        {
            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.IncrementalScriptFiles), dbVersionsViewModelData.IncrementalScriptFiles, 0);
            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.RepeatableScriptFiles), dbVersionsViewModelData.RepeatableScriptFiles, 0);
            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.DevDummyDataScriptFiles), dbVersionsViewModelData.DevDummyDataScriptFiles, 0);

        }

        public virtual void  AssertDBVersionsViewModelDataMissingSystemTables(string testName, DBVersionsViewModelData dbVersionsViewModelData, bool isDevEnv)
        {
            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.IncrementalScriptFiles), dbVersionsViewModelData.IncrementalScriptFiles, 5);
            AssertFilesListHashState(testName, dbVersionsViewModelData.IncrementalScriptFiles, HashDiffType.NotExist);

            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.RepeatableScriptFiles), dbVersionsViewModelData.RepeatableScriptFiles, 2);
            AssertFilesListHashState(testName, dbVersionsViewModelData.RepeatableScriptFiles, HashDiffType.NotExist);

            if (isDevEnv)
            {
                AssertFilesListSize(testName, nameof(dbVersionsViewModelData.DevDummyDataScriptFiles), dbVersionsViewModelData.DevDummyDataScriptFiles, 2);
                AssertFilesListHashState(testName, dbVersionsViewModelData.RepeatableScriptFiles, HashDiffType.NotExist);
            }
            else
            {
                AssertFilesListSize(testName, nameof(dbVersionsViewModelData.DevDummyDataScriptFiles), dbVersionsViewModelData.DevDummyDataScriptFiles, 0);
            }
        }

        public virtual void  AssertDBVersionsViewModelDataNewProject(string testName, DBVersionsViewModelData dbVersionsViewModelData)
        {
            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.IncrementalScriptFiles), dbVersionsViewModelData.IncrementalScriptFiles, 0);

            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.RepeatableScriptFiles), dbVersionsViewModelData.RepeatableScriptFiles, 0);

            AssertFilesListSize(testName, nameof(dbVersionsViewModelData.DevDummyDataScriptFiles), dbVersionsViewModelData.DevDummyDataScriptFiles, 0);
        }


        private static void AssertFilesListSize(string testName, string listName, IList<RuntimeScriptFile> runtimeFiles, int targetSize)
        {
            Assert.That(runtimeFiles.Count == targetSize, $"{testName} >>> {listName} size should be {targetSize}, but was '{runtimeFiles.Count}'");

        }

        private static void AssertFilesListHashState(string testName, IEnumerable<RuntimeScriptFile> runtimeFiles, HashDiffType targetHashState)
        {
            foreach (var file in runtimeFiles)
            {
                AssertFileHashState(testName, file, targetHashState);
            }
        }

        private static void AssertFileHashState(string testName, RuntimeScriptFile runtimeFile, HashDiffType targetHashState)
        {
            Assert.That(runtimeFile.HashDiffType == targetHashState, $"{testName} >>> The {runtimeFile.ScriptFileType.FileTypeCode} file: '{runtimeFile.Filename}' should be '{targetHashState}' hash state, but was '{runtimeFile.HashDiffType}'");

        }
    }
}
