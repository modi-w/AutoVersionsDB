using AutoVersionsDB.Core.Common;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Helpers;
using System;
using System.Collections.Generic;

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


        public override bool CanRollback => !string.IsNullOrWhiteSpace(DBBackupFileFullPath);

      


        public List<RuntimeScriptFileBase> ExecutedFiles { get; private set; }

        public DBVersionsProcessContext()
        {
            ExecutedFiles = new List<RuntimeScriptFileBase>();
        }


        public void AppendExecutedFile(RuntimeScriptFileBase runtimeScriptFile)
        {
            runtimeScriptFile.ThrowIfNull(nameof(runtimeScriptFile));

            runtimeScriptFile.ExecutedDateTime = DateTime.Now;

            ExecutedFiles.Add(runtimeScriptFile);
        }
    }
}
