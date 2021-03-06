﻿using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using System.CommandLine;

namespace AutoVersionsDB.CLI.DBVersions
{
    public class IncTargetCLIOption : Option<string>
    {
        public IncTargetCLIOption()
            : base(new string[] { "--inc-target", "-it" },
                    CLITextResources.TargetCLIOptionDescription
                                    .Replace("[ScriptFileType]", "Incremental")
                                    .Replace("[TargetNoneScriptFileName]", RuntimeScriptFile.TargetNoneScriptFileName)
                                    .Replace("[TargetLastScriptFileName]", RuntimeScriptFile.TargetLastScriptFileName))
        {
            
        }
    }
}
