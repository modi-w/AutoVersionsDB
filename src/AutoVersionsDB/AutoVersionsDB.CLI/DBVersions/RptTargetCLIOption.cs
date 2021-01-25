using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using System.CommandLine;

namespace AutoVersionsDB.CLI.DBVersions
{
    public class RptTargetCLIOption : Option<string>
    {
        public RptTargetCLIOption()
            : base(new string[] { "--rpt-target", "-rt" },
                    CLITextResources.TargetCLIOptionDescription
                                    .Replace("[ScriptFileType]", "Repeatable")
                                    .Replace("[TargetNoneScriptFileName]", RuntimeScriptFile.TargetNoneScriptFileName)
                                    .Replace("[TargetLastScriptFileName]", RuntimeScriptFile.TargetLastScriptFileName))
        {
        }
    }
}
