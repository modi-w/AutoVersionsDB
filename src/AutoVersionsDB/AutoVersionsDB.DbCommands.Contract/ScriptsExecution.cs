using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.DbCommands.Contract
{
    public class ScriptsExecution
    {
        public int DBScriptsExecutionHistoryID { get; set; }
        public string ExecutionTypeName { get; set; }
        public DateTime StartProcessDateTime { get; set; }
        public DateTime EndProcessDateTime { get; set; }
        public double ProcessDurationInMs { get; set; }
        public bool IsVirtualExecution { get; set; }
        public int NumOfScriptFiles { get; set; }
        public string DBBackupFileFullPath { get; set; }

        public IList<ScriptsExecutionFile> ScriptsExecutionFiles { get; }

        public ScriptsExecution()
        {
            ScriptsExecutionFiles = new List<ScriptsExecutionFile>();
        }

    }
}
