using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.Core.ConfigProjects.CLICommands.CLIOptions
{
    public class BackupFolderPathCLIOption : Option<string>
    {
        public BackupFolderPathCLIOption()
            : base(new string[] { "--backup-folder-path", "-buf" }, "Backup up folder path for saving the database before run")
        {
        }
    }
}
