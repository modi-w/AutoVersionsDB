using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;

namespace AutoVersionsDB.Core.Engines
{
    public class AutoVersionsDbEngineContext : EngineContext
    {
        public ScriptFilesState ScriptFilesState { get; set; }
        public string DBBackupFileFullPath { get; set; }

        public ProjectConfigItem ProjectConfig
        {
            get
            {
                return (base.ExecutionParams as AutoVersionsDBExecutionParams).ProjectConfig;
            }
        }

        public bool IsVirtualExecution
        {
            get
            {
                return (base.EngineSettings as AutoVersionsDbEngineSettingBase).IsVirtualExecution;
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

        public AutoVersionsDbEngineContext()
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
