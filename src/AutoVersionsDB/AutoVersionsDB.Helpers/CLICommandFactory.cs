using System.CommandLine;

namespace AutoVersionsDB.Helpers
{
    public abstract class CLICommandFactory
    {

        public abstract Command Create();
    }
}
