using System.CommandLine;

namespace AutoVersionsDB.CLI.DBVersions
{
    public class ScriptNameCLIOption : Option<string>
    {
        public ScriptNameCLIOption()
            : base(new string[] { "--script-name", "-sn" }, CLITextResources.ScriptNameCLIOptionDescription)
        {
            IsRequired = true;
        }
    }
}
