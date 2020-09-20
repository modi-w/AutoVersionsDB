using AutoVersionsDB.Core.DBVersions.ArtifactFile;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable;
using AutoVersionsDB.NotificationableEngine;
using System.IO;

namespace AutoVersionsDB.Core.ConfigProjects
{
    public class ProjectConfigItem
    {
        public string Code { get; set; }


        public string Description { get; set; }

        public string ProjectFullName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Description))
                {
                    return $"{Code} - {Description}";
                }
                else
                {
                    return Code;
                }

            }
        }

        public string DBType { get; set; }
        public string ConnectionString { get; set; }

        public string ConnectionStringToMasterDB { get; set; }

        public string BackupFolderPath { get; set; }

        public bool DevEnvironment { get; set; }

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
                if (DevEnvironment)
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
            DevEnvironment = true;
            DBCommandsTimeout = 300;
        }




    }
}
