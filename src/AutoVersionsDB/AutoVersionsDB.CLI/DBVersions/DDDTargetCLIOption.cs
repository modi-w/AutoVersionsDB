using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using System.CommandLine;

namespace AutoVersionsDB.CLI.DBVersions
{
    public class DDDTargetCLIOption : Option<string>
    {
        public DDDTargetCLIOption()
            : base(new string[] { "--ddd-target", "-dt" }, 
                    CLITextResources.TargetCLIOptionDescription
                                    .Replace("[ScriptFileType]", "DevDummyData")
                                    .Replace("[TargetNoneScriptFileName]", RuntimeScriptFileBase.TargetNoneScriptFileName)
                                    .Replace("[TargetLastScriptFileName]", RuntimeScriptFileBase.TargetLastScriptFileName))
                  
        {
        }
    }
}
