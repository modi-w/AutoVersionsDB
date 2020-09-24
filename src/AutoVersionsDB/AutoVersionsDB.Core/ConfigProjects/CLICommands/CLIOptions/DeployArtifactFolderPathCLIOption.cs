using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.Core.ConfigProjects.CLICommands.CLIOptions
{
    public class DeployArtifactFolderPathCLIOption : Option<string>
    {
        public DeployArtifactFolderPathCLIOption()
            : base(new string[] { "--deploy-artifact-folder-path", "-def" }, "Deploy folder path for exporting the artifact file")
        {
        }
    }
}
