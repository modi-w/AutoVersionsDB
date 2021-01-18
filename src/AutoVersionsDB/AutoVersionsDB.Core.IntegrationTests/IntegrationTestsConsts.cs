using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.Processes;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Helpers;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace AutoVersionsDB.Core.IntegrationTests
{
    public static class IntegrationTestsConsts
    {
        public const string TestProjectId = "IntegrationTestProject";

        public const string SqlServerDBType = "SqlServer";
        public const string TestDBName = "AutoVersionsDB.Tests";


        public static TargetScripts MiddleStateTargetScripts =
            new TargetScripts("incScript_2020-02-25.102_CreateLookupTable2.sql",
                                "rptScript_001_DataForLookupTable1.sql",
                                NoneRuntimeScriptFile.TargetNoneScriptFileName);

        public static TargetScripts FinalStateTargetScripts = TargetScripts.CreateLastState();



        public const string DBBackupBaseFolder = @"[CommonApplicationData]\AutoVersionsDB.Core.IntegrationTests\Backups";

        //public const string  SQLServer_ConnStr  = @"Data Source=(localdb)\localtestdb; Database=AutoVersionsDB.Tests;";
        //public const string  SQLServer_ConnStrToMaster  = @"Data Source=(localdb)\localtestdb; Database=Master;";

        public const string DevScriptsBaseFolderPath_Normal = @"[AppPath]\[DBType]\Scripts\DevScripts";
        public const string DevScriptsBaseFolderPath_ChangedHistoryFiles_Incremental = @"[AppPath]\[DBType]\Scripts\DevScripts_ChangedHistoryFiles_Incremental";
        public const string DevScriptsBaseFolderPath_ChangedHistoryFiles_Repeatable = @"[AppPath]\[DBType]\Scripts\DevScripts_ChangedHistoryFiles_Repeatable";
        public const string DevScriptsBaseFolderPath_MissingFile = @"[AppPath]\[DBType]\Scripts\DevScripts_MissingFile";
        public const string DevScriptsBaseFolderPath_ScriptError = @"[AppPath]\[DBType]\Scripts\DevScripts_ScriptError";
        public const string DevScriptsBaseFolderPath_NoScriptFiles = @"[AppPath]\[DBType]\Scripts\DevScripts_NoScriptFiles";

        public const string DeployArtifact_FolderPath = @"[AppPath]\[DBType]\Scripts\Deploy";


        public const string DeliveryArtifactFolderPath_Normal = @"[AppPath]\[DBType]\Scripts\Delivery";
        public const string DeliveryArtifactFolderPath_ChangedHistoryFiles_Incremental = @"[AppPath]\[DBType]\Scripts\Delivery_ChangedHistoryFiles_Incremental";
        public const string DeliveryArtifactFolderPath_ChangedHistoryFiles_Repeatable = @"[AppPath]\[DBType]\Scripts\Delivery_ChangedHistoryFiles_Repeatable";
        public const string DeliveryArtifactFolderPath_MissingFileh = @"[AppPath]\[DBType]\Scripts\Delivery_MissingFile";
        public const string DeliveryArtifactFolderPath_ScriptError = @"[AppPath]\[DBType]\Scripts\Delivery_ScriptError";
        public const string DeliveryArtifactFolderPath_WithDevDummyDataFiles = @"[AppPath]\[DBType]\Scripts\Delivery_WithDevDummyDataFiles";
        public const string DeliveryArtifactFolderPath_NoScriptFiles = @"[AppPath]\[DBType]\Scripts\Delivery_NoScriptFiles";



        public const string DBBackup_EmptyDB = @"[AppPath]\[DBType]\DBBackups\AutoVersionsDB_EmptyDB.bak";
        public const string DBBackup_ExceptSystemTables = @"[AppPath]\[DBType]\DBBackups\AutoVersionsDB_EmptyDB_ExceptSystemTables.bak";
        public const string DBBackup_AfterRunInitStateScript = @"[AppPath]\[DBType]\DBBackups\AutoVersionsDB_AfterRunInitStateScript.bak";
        public const string DBBackup_MiddleState = @"[AppPath]\[DBType]\DBBackups\AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak";
        public const string DBBackup_FinalState_DevEnv = @"[AppPath]\[DBType]\DBBackups\AutoVersionsDB_FinalState_DevEnv.bak";
        public const string DBBackup_FinalState_DeliveryEnv = @"[AppPath]\[DBType]\DBBackups\AutoVersionsDB_FinalState_DeliveryEnv.bak";
        public const string DBBackup_FinalState_MissingSystemTables = @"[AppPath]\[DBType]\DBBackups\AutoVersionsDB_FinalState_MissingSystemTables.bak";
        public const string DBBackup_AddDataAfterFinalState = @"[AppPath]\[DBType]\DBBackups\AutoVersionsDB_AddDataAfterFinalState.bak";



        public static ProjectConfigItem DummyProjectConfigValid = new ProjectConfigItem()
        {
            Id = TestProjectId,
            Description = "DummyDescription",
            DBType = SqlServerDBType,
            Server = SqlServerLocalDBConnection.ConnectionStringBuilder.DataSource,
            DBName = TestDBName,
            Username = SqlServerLocalDBConnection.ConnectionStringBuilder.UserID,
            Password = SqlServerLocalDBConnection.ConnectionStringBuilder.Password,
            BackupFolderPath = FileSystemPathUtils.ParsePathVaribles($"{IntegrationTestsConsts.DBBackupBaseFolder}_Temp"),
            DevEnvironment = true,
            DevScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles($"{IntegrationTestsConsts.DevScriptsBaseFolderPath_Normal}_Temp").Replace("[DBType]", IntegrationTestsConsts.SqlServerDBType),
            DeployArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles($"{IntegrationTestsConsts.DeployArtifact_FolderPath}_Temp").Replace("[DBType]", IntegrationTestsConsts.SqlServerDBType),
            DeliveryArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(IntegrationTestsConsts.DeliveryArtifactFolderPath_Normal).Replace("[DBType]", IntegrationTestsConsts.SqlServerDBType),
        };

        public static ProjectConfigItem GetNewInstanceForDummyProjectConfigValid()
        {
            return new ProjectConfigItem()
            {
                Id = DummyProjectConfigValid.Id,
                Description = DummyProjectConfigValid.Description,
                DBType = DummyProjectConfigValid.DBType,
                Server = DummyProjectConfigValid.Server,
                DBName = DummyProjectConfigValid.DBName,
                Username = DummyProjectConfigValid.Username,
                Password = DummyProjectConfigValid.Password,
                BackupFolderPath = DummyProjectConfigValid.BackupFolderPath,
                DevEnvironment = DummyProjectConfigValid.DevEnvironment,
                DevScriptsBaseFolderPath = DummyProjectConfigValid.DevScriptsBaseFolderPath,
                DeployArtifactFolderPath = DummyProjectConfigValid.DeployArtifactFolderPath,
                DeliveryArtifactFolderPath = DummyProjectConfigValid.DeliveryArtifactFolderPath,
            };
        }


        public static ProjectConfigItem DummyProjectConfigDBNotValid = new ProjectConfigItem()
        {
            Id = TestProjectId,
            Description = "DummyDescription",
            DBType = IntegrationTestsConsts.SqlServerDBType,
            Server = "DummyServer",
            DBName = "DummyDBName",
            Username = "DummyUsername",
            Password = "DummyPassword",
            BackupFolderPath = FileSystemPathUtils.ParsePathVaribles(@"[CommonApplicationData]\AutoVersionsDB.Core.IntegrationTests\DummyBackupFolderPath"),
            DevEnvironment = true,
            DevScriptsBaseFolderPath = FileSystemPathUtils.ParsePathVaribles(@"[CommonApplicationData]\AutoVersionsDB.Core.IntegrationTests\DummyDevScriptsBaseFolderPath"),
            DeployArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(@"[CommonApplicationData]\AutoVersionsDB.Core.IntegrationTests\DummyDeployArtifactFolderPath"),
            DeliveryArtifactFolderPath = FileSystemPathUtils.ParsePathVaribles(@"[CommonApplicationData]\AutoVersionsDB.Core.IntegrationTests\DummyDeliveryArtifactFolderPath"),
        };
    }
}
