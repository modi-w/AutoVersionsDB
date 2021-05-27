using System.CommandLine;

namespace AutoVersionsDB.CLI
{
    public abstract class CLICommandFactory
    {

        public abstract Command Create();
    }
}
