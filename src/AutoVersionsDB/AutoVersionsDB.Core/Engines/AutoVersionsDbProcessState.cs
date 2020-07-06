using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;

namespace AutoVersionsDB.Core.Engines
{
    public class AutoVersionsDbProcessState : ProcessStateBase
    {

        public string DBBackupFileFullPath { get; set; }

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

        public AutoVersionsDbProcessState()
        {
            ExecutedFiles = new List<RuntimeScriptFileBase>();
            EngineMetaData = new Dictionary<string, string>();
        }


        public void AppendExecutedFile(RuntimeScriptFileBase runtimeScriptFile)
        {
            runtimeScriptFile.ThrowIfNull(nameof(runtimeScriptFile));

            runtimeScriptFile.ExecutedDateTime = DateTime.Now;

            ExecutedFiles.Add(runtimeScriptFile);
        }
    }
}
