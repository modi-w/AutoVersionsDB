using AutoVersionsDB.Core.ArtifactFile;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.ScriptFiles.Incremental;
using AutoVersionsDB.Core.ScriptFiles.Repeatable;
using AutoVersionsDB.NotificationableEngine;
using System.IO;

namespace AutoVersionsDB.Core.ConfigProjects
{
    public class ProjectConfigItem : NotificationableEngineConfig
    {
        public string ProjectGuid { get; set; }
        public string ProjectName { get; set; }


        public string DBTypeCode { get; set; }
        public string ConnStr { get; set; }

        public string ConnStrToMasterDB { get; set; }

        public string DBBackupBaseFolder { get; set; }

        public bool IsDevEnvironment { get; set; }

        public string DevScriptsBaseFolderPath { get; set; }


        public string DeployArtifactFolderPath { get; set; }
        public string DeliveryArtifactFolderPath { get; set; }


        public int DBCommandsTimeout { get; set; }


        public string DeliveryExtractedFilesArtifactFolder
        {
            get
            {
                string outStr = "";

                if (!string.IsNullOrWhiteSpace(DeliveryArtifactFolderPath))
                {
                    outStr = Path.Combine(this.DeliveryArtifactFolderPath, ArtifactExtractor.TempExtractArtifactFolderName);
                }

                return outStr;
            }
        }


        public string ScriptsBaseFolderPath
        {
            get
            {
                if (IsDevEnvironment)
                {
                    return DevScriptsBaseFolderPath;
                }
                else
                {
                    return DeliveryExtractedFilesArtifactFolder;
                }
            }
        }


        public string IncrementalScriptsFolderPath
        {
            get
            {
                string outStr = "";

                if (!string.IsNullOrWhiteSpace(ScriptsBaseFolderPath))
                {
                    ScriptFileTypeBase incrementalScriptFileType = ScriptFileTypeBase.Create<IncrementalScriptFileType>();
                    outStr = Path.Combine(ScriptsBaseFolderPath, incrementalScriptFileType.RelativeFolderName);
                }

                return outStr;
            }
        }
        public string RepeatableScriptsFolderPath
        {
            get
            {
                string outStr = "";

                if (!string.IsNullOrWhiteSpace(ScriptsBaseFolderPath))
                {
                    ScriptFileTypeBase repeatableScriptFileType = ScriptFileTypeBase.Create<RepeatableScriptFileType>();
                    outStr = Path.Combine(ScriptsBaseFolderPath, repeatableScriptFileType.RelativeFolderName);
                }

                return outStr;
            }
        }
        public string DevDummyDataScriptsFolderPath
        {
            get
            {
                string outStr = "";

                if (!string.IsNullOrWhiteSpace(ScriptsBaseFolderPath))
                {
                    ScriptFileTypeBase devDummyDataScriptFileType = ScriptFileTypeBase.Create<DevDummyDataScriptFileType>();
                    outStr = Path.Combine(ScriptsBaseFolderPath, devDummyDataScriptFileType.RelativeFolderName);
                }

                return outStr;
            }
        }




        public ProjectConfigItem()
        {
            IsDevEnvironment = true;
            DBCommandsTimeout = 300;
        }



        

    }
}
