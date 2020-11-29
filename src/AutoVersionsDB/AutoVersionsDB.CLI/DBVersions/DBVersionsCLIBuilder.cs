using AutoVersionsDB.Helpers;
using System.Collections.Generic;
using System.CommandLine;


namespace AutoVersionsDB.CLI.DBVersions
{
    public class DBVersionsCLIBuilder
    {
        private readonly List<CLICommandFactory> _cliCommandFactories;


        public DBVersionsCLIBuilder(ValidateCommandFactory validateCommandFactory,
                                    FilesCommandFactory filesCommandFactory,
                                    SyncCommandFactory syncCommandFactory,
                                    RecreateCommandFactory recreateCommandFactory,
                                    VirtualCommandFactory virtualCommandFactory,
                                    DeployCommandFactory deployCommandFactory,
                                    CreateNewScriptFileCommandFactory createNewScriptFileCommandFactory)
        {
            _cliCommandFactories = new List<CLICommandFactory>()
            {
                validateCommandFactory,
                filesCommandFactory,
                syncCommandFactory,
                recreateCommandFactory,
                virtualCommandFactory,
                deployCommandFactory,
                createNewScriptFileCommandFactory
            };
        }

        public void Build(RootCommand rootCommand)
        {
            foreach (var commandFactory in _cliCommandFactories)
            {
                Command command = commandFactory.Create();
                rootCommand.Add(command);
            }
        }
    }
}
