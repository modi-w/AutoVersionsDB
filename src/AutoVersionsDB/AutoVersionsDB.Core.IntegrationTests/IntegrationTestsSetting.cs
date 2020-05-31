using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace AutoVersionsDB.Core.IntegrationTests
{
    public static class IntegrationTestsSetting
    {

        public const string  DBBackupBaseFolder  = @"[CommonApplicationData]\AutoVersionsDB.Core.IntegrationTests\Backups";

        //public const string  SQLServer_ConnStr  = @"Data Source=(localdb)\localtestdb; Database=AutoVersionsDB.Tests;";
        //public const string  SQLServer_ConnStrToMaster  = @"Data Source=(localdb)\localtestdb; Database=Master;";

        public const string  DevScriptsBaseFolderPath_Normal  = @"[AppPath]\FilesForTests\SqlServer\DevScripts";
        public const string  DevScriptsBaseFolderPath_ChangedHistoryFiles_Incremental  = @"[AppPath]\FilesForTests\SqlServer\DevScripts_ChangedHistoryFiles_Incremental";
        public const string  DevScriptsBaseFolderPath_ChangedHistoryFiles_Repeatable  = @"[AppPath]\FilesForTests\SqlServer\DevScripts_ChangedHistoryFiles_Repeatable";
        public const string  DevScriptsBaseFolderPath_MissingFile  = @"[AppPath]\FilesForTests\SqlServer\DevScripts_MissingFile";
        public const string  DevScriptsBaseFolderPath_ScriptError  = @"[AppPath]\FilesForTests\SqlServer\DevScripts_ScriptError";


        public const string  DeployArtifact_FolderPath  = @"[AppPath]\FilesForTests\SqlServer\Deploy";


        public const string  DeliveryArtifactFolderPath_Normal  = @"[AppPath]\FilesForTests\SqlServer\Delivery";
        public const string  DeliveryArtifactFolderPath_ChangedHistoryFiles_Incremental  = @"[AppPath]\FilesForTests\SqlServer\Delivery_ChangedHistoryFiles_Incremental";
        public const string  DeliveryArtifactFolderPath_ChangedHistoryFiles_Repeatable  = @"[AppPath]\FilesForTests\SqlServer\Delivery_ChangedHistoryFiles_Repeatable";
        public const string  DeliveryArtifactFolderPath_MissingFileh  = @"[AppPath]\FilesForTests\SqlServer\Delivery_MissingFile";
        public const string  DeliveryArtifactFolderPath_ScriptError  = @"[AppPath]\FilesForTests\SqlServer\Delivery_ScriptError";
        public const string  DeliveryArtifactFolderPath_WithDevDummyDataFiles  = @"[AppPath]\FilesForTests\SqlServer\Delivery_WithDevDummyDataFiles";

    }
}
