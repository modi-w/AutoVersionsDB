using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using System.CommandLine;

namespace AutoVersionsDB.CLI.DBVersions
{
    public class IncTargetCLIOption : Option<string>
    {
        public IncTargetCLIOption()
            : base(new string[] { "--inc-target", "-it" }, $"The Incremental target file script name that set the db in the desired state. Set {NoneRuntimeScriptFile.TargetNoneScriptFileName} if you want to that system not run any incremental script; Set {LastRuntimeScriptFile.TargetLastScriptFileName} if you want to that system not run all incremental scripts; Otherwise, set the target script name.")
        {
            
        }
    }
}
