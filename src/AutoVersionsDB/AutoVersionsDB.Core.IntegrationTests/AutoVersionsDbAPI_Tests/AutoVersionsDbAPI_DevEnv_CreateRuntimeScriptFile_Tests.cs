using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests;
using AutoVersionsDB.Core.IntegrationTests.Helpers;
using AutoVersionsDB.NotificationableEngine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests
{
    public class AutoVersionsDbAPI_DevEnv_CreateRuntimeScriptFile_Tests : AutoVersionsDbAPI_TestsBase
    {
        private string c_RelFolder_Incremental = "Incremental";

        private string c_Incremental_scriptName1 = "TestIncScript1";
        private string _scriptFullPath_Incremental_scriptName1
        {
            get
            {
                string devScriptsBaseFolderPath = FileSystemHelpers.ParsePathVaribles(IntegrationTestsSetting.DevScriptsBaseFolderPath_Normal);

                string scriptFilename = $"incScript_{DateTime.Today:yyyy-MM-dd}.001_{c_Incremental_scriptName1}.sql";
                string script1FullPath = Path.Combine(devScriptsBaseFolderPath, c_RelFolder_Incremental, scriptFilename);

                return script1FullPath;
            }
        }
        private string c_Incremental_scriptName2 = "TestIncScript2";
        private string _scriptFullPath_Incremental__scriptName2
        {
            get
            {
                string devScriptsBaseFolderPath = FileSystemHelpers.ParsePathVaribles(IntegrationTestsSetting.DevScriptsBaseFolderPath_Normal);

                string scriptFilename = $"incScript_{DateTime.Today:yyyy-MM-dd}.002_{c_Incremental_scriptName2}.sql";
                string script1FullPath = Path.Combine(devScriptsBaseFolderPath, c_RelFolder_Incremental, scriptFilename);

                return script1FullPath;
            }
        }


        private string c_RelFolder_Repeatable = "Repeatable";
        private string c_Repeatable_scriptName1 = "TestRptScript1";
        private string _scriptFullPath_Repeatable_scriptName1
        {
            get
            {
                string devScriptsBaseFolderPath = FileSystemHelpers.ParsePathVaribles(IntegrationTestsSetting.DevScriptsBaseFolderPath_Normal);

                string scriptFilename = $"rptScript_{c_Repeatable_scriptName1}.sql";
                string script1FullPath = Path.Combine(devScriptsBaseFolderPath, c_RelFolder_Repeatable, scriptFilename);

                return script1FullPath;
            }
        }


        private string c_RelFolder_DevDummyData = "DevDummyData";
        private string c_DevDummyData_scriptName1 = "TestDddScript1";
        private string _scriptFullPath_DevDummyData_scriptName1
        {
            get
            {
                string devScriptsBaseFolderPath = FileSystemHelpers.ParsePathVaribles(IntegrationTestsSetting.DevScriptsBaseFolderPath_Normal);

                string scriptFilename = $"dddScript_{c_DevDummyData_scriptName1}.sql";
                string script1FullPath = Path.Combine(devScriptsBaseFolderPath, c_RelFolder_DevDummyData, scriptFilename);

                return script1FullPath;
            }
        }


        [Test]
        public void DevEnv_CreateRuntimeScriptFile_Incremental_IsCreated([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            if (File.Exists(_scriptFullPath_Incremental_scriptName1))
            {
                File.Delete(_scriptFullPath_Incremental_scriptName1);
            }


            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DevEnv.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            string fileToDelete = "";

            try
            {
                //Act
                fileToDelete = AutoVersionsDbAPI.CreateNewIncrementalScriptFile(projectConfig, c_Incremental_scriptName1);

                //Assert
                Assert.That(File.Exists(_scriptFullPath_Incremental_scriptName1));
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(fileToDelete)
                    && File.Exists(fileToDelete))
                {
                    File.Delete(fileToDelete);
                }
            }
        }

        [Test]
        public void DevEnv_CreateRuntimeScriptFile_Incremental_CreateNextVersionSameDay([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            if (File.Exists(_scriptFullPath_Incremental_scriptName1))
            {
                File.Delete(_scriptFullPath_Incremental_scriptName1);
            }


            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DevEnv.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            string fileToDelete1 = "";
            string fileToDelete2 = "";

            try
            {
                //Act
                fileToDelete1 = AutoVersionsDbAPI.CreateNewIncrementalScriptFile(projectConfig, c_Incremental_scriptName1);
                fileToDelete2 = AutoVersionsDbAPI.CreateNewIncrementalScriptFile(projectConfig, c_Incremental_scriptName2);

                //Assert
                Assert.That(File.Exists(_scriptFullPath_Incremental_scriptName1));
                Assert.That(File.Exists(_scriptFullPath_Incremental__scriptName2));
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(fileToDelete1)
                    && File.Exists(fileToDelete1))
                {
                    File.Delete(fileToDelete1);
                }
                if (!string.IsNullOrWhiteSpace(fileToDelete2)
                    && File.Exists(fileToDelete2))
                {
                    File.Delete(fileToDelete2);
                }

            }
        }





        [Test]
        public void DevEnv_CreateRuntimeScriptFile_Repeatable_IsCreated([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            if (File.Exists(_scriptFullPath_Repeatable_scriptName1))
            {
                File.Delete(_scriptFullPath_Repeatable_scriptName1);
            }


            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DevEnv.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            string fileToDelete = "";

            try
            {
                //Act
                fileToDelete = AutoVersionsDbAPI.CreateNewRepeatableScriptFile(projectConfig, c_Repeatable_scriptName1);

                //Assert
                Assert.That(File.Exists(_scriptFullPath_Repeatable_scriptName1));
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(fileToDelete)
                    && File.Exists(fileToDelete))
                {
                    File.Delete(fileToDelete);
                }
            }
        }


        [Test]
        public void DevEnv_CreateRuntimeScriptFile_DevDummyData_IsCreated([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            if (File.Exists(_scriptFullPath_DevDummyData_scriptName1))
            {
                File.Delete(_scriptFullPath_DevDummyData_scriptName1);
            }


            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DevEnv.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            string fileToDelete = "";

            try
            {
                //Act
                fileToDelete = AutoVersionsDbAPI.CreateNewDevDummyDataScriptFile(projectConfig, c_DevDummyData_scriptName1);

                //Assert
                Assert.That(File.Exists(_scriptFullPath_DevDummyData_scriptName1));
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(fileToDelete)
                    && File.Exists(fileToDelete))
                {
                    File.Delete(fileToDelete);
                }
            }
        }
    }
}
