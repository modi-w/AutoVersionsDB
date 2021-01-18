using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using System.CommandLine;

namespace AutoVersionsDB.CLI.DBVersions
{
    public class RptTargetCLIOption : Option<string>
    {
        public RptTargetCLIOption()
            : base(new string[] { "--rpt-target", "-rt" }, $"The Repeatable target file script name that set the db in the desired state. Set {NoneRuntimeScriptFile.TargetNoneScriptFileName} if you want to that system not run any repeatable script; Set {LastRuntimeScriptFile.TargetLastScriptFileName} if you want to that system not run all repeatable scripts; Otherwise, set the target script name.")
        {
        }
    }
}
