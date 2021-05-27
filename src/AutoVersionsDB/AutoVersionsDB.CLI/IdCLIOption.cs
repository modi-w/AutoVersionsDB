using System.CommandLine;

namespace AutoVersionsDB.CLI
{
    public class IdCLIOption : Option<string>
    {
        public IdCLIOption()
            : base(new string[] { "--id", "-id" }, CLITextResources.IdCLIOptionDescription)
        {
            IsRequired = true;
        }
    }
}
