using System.CommandLine;

namespace AutoVersionsDB.CLI.DBVersions
{
    public class TargetCLIOption : Option<string>
    {
        public TargetCLIOption()
            : base(new string[] { "--target", "-t" }, "The target file script name that set the db in the desired state")
        {
        }
    }
}
