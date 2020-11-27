using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.DbCommands.Contract
{
    public class ScriptsExecutionFile
    {
        public int ID { get; set; }
        public int DBScriptsExecutionHistoryID { get; set; }
        public DateTime ExecutedDateTime { get; set; }
        public bool IsVirtualExecution { get; set; }
        public string Filename { get; set; }
        public string FileFullPath { get; set; }
        public string ScriptFileType { get; set; }
        public string ComputedFileHash { get; set; }
        public DateTime ComputedFileHashDateTime { get; set; }

    }
}
