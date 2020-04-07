using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;

namespace AutoVersionsDB.Core.Engines
{
    public class AutoVersionsDbProcessState : ProcessStateBase
    {
       // public bool IsResetDBFlag { get; set; }

        //  public string TargetStateScriptFileName { get; set; }

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


        public List<RuntimeScriptFileBase> ExecutedFiles { get; set; }

        public AutoVersionsDbProcessState()
        {
            ExecutedFiles = new List<RuntimeScriptFileBase>();
        }


        public void AppendExecutedFile(RuntimeScriptFileBase runtimeScriptFile)
        {
            runtimeScriptFile.ExecutedDateTime = DateTime.Now;

            ExecutedFiles.Add(runtimeScriptFile);
        }
    }
}
