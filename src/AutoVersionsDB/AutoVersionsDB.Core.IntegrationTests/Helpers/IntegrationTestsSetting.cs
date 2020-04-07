using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace AutoVersionsDB.Core.IntegrationTests.Helpers
{
    public class IntegrationTestsSetting
    {
        public static IntegrationTestsSetting LoadSetting()
        {
            IntegrationTestsSetting results = null;

            Assembly assemblyInfo = Assembly.GetExecutingAssembly();
            var uriCodeBase = new Uri(assemblyInfo.CodeBase);
            string dllFolder = Path.GetDirectoryName(uriCodeBase.LocalPath);

            string settingFileFullPath = Path.Combine(dllFolder, "AutoVersionsDB_IntegrationTestsSetting.json");

            string settingContentStr = File.ReadAllText(settingFileFullPath);
            results = JsonConvert.DeserializeObject<IntegrationTestsSetting>(settingContentStr);

            return results;
        }


        public string DBBackupBaseFolder { get; set; }

        public string SQLServer_ConnStr { get; set; }
        public string SQLServer_ConnStrToMaster { get; set; }

        public string DevScriptsBaseFolderPath_Normal { get; set; }
        public string DevScriptsBaseFolderPath_ChangedHistoryFiles_Incremental { get; set; }
        public string DevScriptsBaseFolderPath_ChangedHistoryFiles_Repeatable { get; set; }
        public string DevScriptsBaseFolderPath_MissingFile { get; set; }
        public string DevScriptsBaseFolderPath_ScriptError { get; set; }


        public string DeployArtifact_FolderPath { get; set; }


        public string DeliveryArtifactFolderPath_Normal { get; set; }
        public string DeliveryArtifactFolderPath_ChangedHistoryFiles_Incremental { get; set; }
        public string DeliveryArtifactFolderPath_ChangedHistoryFiles_Repeatable { get; set; }
        public string DeliveryArtifactFolderPath_MissingFileh { get; set; }
        public string DeliveryArtifactFolderPath_ScriptError { get; set; }
        public string DeliveryArtifactFolderPath_WithDevDummyDataFiles { get; set; }

    }
}
