using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.Core.ConfigProjects.CLICommands.CLIOptions
{
    public class DeliveryArtifactFolderPathCLIOption : Option<string>
    {
        public DeliveryArtifactFolderPathCLIOption()
            : base(new string[] { "--delivery-artifact-folder-path", "-darf" }, "For delivery environment only - The folder where artifact file is located")
        {
        }
    }
}
