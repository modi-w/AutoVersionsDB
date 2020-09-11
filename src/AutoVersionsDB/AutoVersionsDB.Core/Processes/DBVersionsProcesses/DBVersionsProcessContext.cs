using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;

namespace AutoVersionsDB.Core.Processes.DBVersionsProcesses
{
    public class DBVersionsProcessContext : ProcessContext, IProjectConfigable 
    {
        public ScriptFilesState ScriptFilesState { get; set; }
        public string DBBackupFileFullPath { get; set; }

        public ProjectConfigItem ProjectConfig { get; set; }



        public bool IsVirtualExecution
        {
            get
            {
                return (ProcessDefinition as DBVersionsProcessDefinition).IsVirtualExecution;
            }
        }


        public override bool CanRollback
        {
            get
            {
                return !string.IsNullOrWhiteSpace(DBBackupFileFullPath);
            }
        }

        public double ProcessDurationInMs
        {
            get
            {
                double results = 0;

                if (StartProcessDateTime.HasValue)
                {
                    if (EndProcessDateTime.HasValue)
                    {
                        results = (EndProcessDateTime.Value - StartProcessDateTime.Value).TotalMilliseconds;
                    }
                    else
                    {
                        results = (DateTime.Now - StartProcessDateTime.Value).TotalMilliseconds;
                    }
                }

                return results;
            }
        }


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
