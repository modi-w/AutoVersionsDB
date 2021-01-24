using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.DevDummyData;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Incremental;
using AutoVersionsDB.Core.DBVersions.ScriptFiles.Repeatable;
using System.Collections.Generic;

namespace AutoVersionsDB.Core.DBVersions.Processes
{
    public class TargetScripts
    {
        public static TargetScripts CreateNoneState() => new TargetScripts(RuntimeScriptFile.TargetNoneScriptFileName, RuntimeScriptFile.TargetNoneScriptFileName, RuntimeScriptFile.TargetNoneScriptFileName);
        public static TargetScripts CreateLastState() => new TargetScripts(RuntimeScriptFile.TargetLastScriptFileName, RuntimeScriptFile.TargetLastScriptFileName, RuntimeScriptFile.TargetLastScriptFileName);

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
                { IncrementalScriptFileType.Code, IncScriptFileName },
                { RepeatableScriptFileType.Code, RptScriptFileName },
                { DevDummyDataScriptFileType.Code, DDDScriptFileName }
            };
        }

        private static string GetTargetScriptNameOrDefault(string targetScriptName)
        {
            if (string.IsNullOrWhiteSpace(targetScriptName))
            {
                return RuntimeScriptFile.TargetNoneScriptFileName;
            }
            else
            {
                return targetScriptName;
            }
        }
    }
}