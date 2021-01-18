using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable;
using System.Collections.Generic;

namespace AutoVersionsDB.Core.DBVersions.Processes
{
    public class TargetScripts
    {
        public static TargetScripts CreateNoneState() => new TargetScripts(RuntimeScriptFileBase.TargetNoneScriptFileName, RuntimeScriptFileBase.TargetNoneScriptFileName, RuntimeScriptFileBase.TargetNoneScriptFileName);
        public static TargetScripts CreateLastState() => new TargetScripts(RuntimeScriptFileBase.TargetLastScriptFileName, RuntimeScriptFileBase.TargetLastScriptFileName, RuntimeScriptFileBase.TargetLastScriptFileName);

        public string IncScriptFileName { get; set; }
        public string RptScriptFileName { get; set; }
        public string DDDScriptFileName { get; set; }

        public Dictionary<string, string> TargetScriptsByType { get; }

        public TargetScripts(string incScriptFileName,
                            string rptScriptFileName,
                            string dddScriptFileName)
        {
            IncScriptFileName = GetTargetScriptNameOrDefault(incScriptFileName);
            RptScriptFileName = GetTargetScriptNameOrDefault(rptScriptFileName);
            DDDScriptFileName = GetTargetScriptNameOrDefault(dddScriptFileName);

            TargetScriptsByType = new Dictionary<string, string>
            {
                { IncrementalScriptFileType.IncrementalFileTypeCode, IncScriptFileName },
                { RepeatableScriptFileType.RepeatableFileTypeCode, RptScriptFileName },
                { DevDummyDataScriptFileType.DevDummyDataFileTypeCode, DDDScriptFileName }
            };
        }

        private static string GetTargetScriptNameOrDefault(string targetScriptName)
        {
            if (string.IsNullOrWhiteSpace(targetScriptName))
            {
                return RuntimeScriptFileBase.TargetNoneScriptFileName;
            }
            else
            {
                return targetScriptName;
            }
        }
    }
}