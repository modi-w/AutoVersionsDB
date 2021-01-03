﻿using System.CommandLine;

namespace AutoVersionsDB.CLI.ConfigProjects.CLIOptions
{
    public class ScriptsBaseFolderPathCLIOption : Option<string>
    {
        public ScriptsBaseFolderPathCLIOption()
            : base(new string[] { "--dev-scripts-base-folder-path", "-dsf" }, "For dev environment only - the scripts base folder path. Where all the project scripts files located. Usually, it is a folder the the source control can follow.")
        {
        }
    }
}