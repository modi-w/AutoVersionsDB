using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace AutoVersionsDB.Core.IntegrationTests
{
    public static class IntegrationTestsConsts
    {
        public const string TestProjectId = "IntegrationTestProject";

        public const string  DBBackupBaseFolder  = @"[CommonApplicationData]\AutoVersionsDB.Core.IntegrationTests\Backups";

        //public const string  SQLServer_ConnStr  = @"Data Source=(localdb)\localtestdb; Database=AutoVersionsDB.Tests;";
        //public const string  SQLServer_ConnStrToMaster  = @"Data Source=(localdb)\localtestdb; Database=Master;";

        public const string  DevScriptsBaseFolderPath_Normal  = @"[AppPath]\FilesForTests\[DBType]\Scripts\DevScripts";
        public const string  DevScriptsBaseFolderPath_ChangedHistoryFiles_Incremental  = @"[AppPath]\FilesForTests\[DBType]\Scripts\DevScripts_ChangedHistoryFiles_Incremental";
        public const string  DevScriptsBaseFolderPath_ChangedHistoryFiles_Repeatable  = @"[AppPath]\FilesForTests\[DBType]\Scripts\DevScripts_ChangedHistoryFiles_Repeatable";
        public const string  DevScriptsBaseFolderPath_MissingFile  = @"[AppPath]\FilesForTests\[DBType]\Scripts\DevScripts_MissingFile";
        public const string  DevScriptsBaseFolderPath_ScriptError  = @"[AppPath]\FilesForTests\[DBType]\Scripts\DevScripts_ScriptError";

        public const string  DeployArtifact_FolderPath  = @"[AppPath]\FilesForTests\[DBType]\Scripts\Deploy";


        public const string  DeliveryArtifactFolderPath_Normal  = @"[AppPath]\FilesForTests\[DBType]\Scripts\Delivery";
        public const string  DeliveryArtifactFolderPath_ChangedHistoryFiles_Incremental  = @"[AppPath]\FilesForTests\[DBType]\Scripts\Delivery_ChangedHistoryFiles_Incremental";
        public const string  DeliveryArtifactFolderPath_ChangedHistoryFiles_Repeatable  = @"[AppPath]\FilesForTests\[DBType]\Scripts\Delivery_ChangedHistoryFiles_Repeatable";
        public const string  DeliveryArtifactFolderPath_MissingFileh  = @"[AppPath]\FilesForTests\[DBType]\Scripts\Delivery_MissingFile";
        public const string  DeliveryArtifactFolderPath_ScriptError  = @"[AppPath]\FilesForTests\[DBType]\Scripts\Delivery_ScriptError";
        public const string  DeliveryArtifactFolderPath_WithDevDummyDataFiles  = @"[AppPath]\FilesForTests\[DBType]\Scripts\Delivery_WithDevDummyDataFiles";



        public const string DBBackup_EmptyDB = @"[AppPath]\FilesForTests\[DBType]\DBBackups\AutoVersionsDB_EmptyDB.bak";
        public const string DBBackup_ExceptSystemTables = @"[AppPath]\FilesForTests\[DBType]\DBBackups\AutoVersionsDB_EmptyDB_ExceptSystemTables.bak";
        public const string DBBackup_AfterRunInitStateScript = @"[AppPath]\FilesForTests\[DBType]\DBBackups\AutoVersionsDB_AfterRunInitStateScript.bak";
        public const string DBBackup_MiddleState = @"[AppPath]\FilesForTests\[DBType]\DBBackups\AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak";
        public const string DBBackup_FinalState_DevEnv = @"[AppPath]\FilesForTests\[DBType]\DBBackups\AutoVersionsDB_FinalState_DevEnv.bak";
        public const string DBBackup_FinalState_DeliveryEnv = @"[AppPath]\FilesForTests\[DBType]\DBBackups\AutoVersionsDB_FinalState_DeliveryEnv.bak";
        public const string DBBackup_FinalState_MissingSystemTables = @"[AppPath]\FilesForTests\[DBType]\DBBackups\AutoVersionsDB_FinalState_MissingSystemTables.bak";
        public const string DBBackup_AddDataAfterFinalState = @"[AppPath]\FilesForTests\[DBType]\DBBackups\AutoVersionsDB_AddDataAfterFinalState.bak";

    }
}
