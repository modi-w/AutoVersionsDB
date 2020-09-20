using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.Core.ConfigProjects.CLICommands.CLIOptions
{
    public class ScriptsBaseFolderPathCLIOption : Option<string>
    {
        public ScriptsBaseFolderPathCLIOption()
            : base(new string[] { "--dev-scripts-base-folder-path", "-dsf" }, "For dev environment only - the scripts base folder path. Where all the project scripts files located. Usually, it is a folder the the source control can follow.")
        {
        }
    }
}
