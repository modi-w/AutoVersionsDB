using AutoVersionsDB.Core.Common;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Helpers;
using System;
using System.Collections.Generic;
using System.IO;

namespace AutoVersionsDB.Core.DBVersions.Processes
{
    public class DBVersionsProcessContext : CommonProcessContext
    {
        public ScriptFilesState ScriptFilesState { get; set; }
        public string DBBackupFileFullPath { get; set; }

        private ProjectConfigItem _projectConfig;
        public override ProjectConfigItem ProjectConfig => _projectConfig;
        public void SetProjectConfig(ProjectConfigItem projectConfig)
        {
            _projectConfig = projectConfig;
        }



        public bool IsVirtualExecution => (ProcessDefinition as DBVersionsProcessDefinition).IsVirtualExecution;


        public override bool CanRollback => !string.IsNullOrWhiteSpace(DBBackupFileFullPath) && File.Exists(DBBackupFileFullPath);




        public List<RuntimeScriptFile> ExecutedFiles { get; private set; }

        public DBVersionsProcessContext()
        {
            ExecutedFiles = new List<RuntimeScriptFile>();
        }


        public void AppendExecutedFile(RuntimeScriptFile runtimeScriptFile)
        {
            runtimeScriptFile.ThrowIfNull(nameof(runtimeScriptFile));

            runtimeScriptFile.ExecutedDateTime = DateTime.Now;

            ExecutedFiles.Add(runtimeScriptFile);
        }
    }
}
