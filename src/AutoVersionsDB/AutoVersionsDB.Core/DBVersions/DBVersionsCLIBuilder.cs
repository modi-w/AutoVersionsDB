using System;
using System.Collections.Generic;
using System.Text;
using System.CommandLine;
using AutoVersionsDB.Core.DBVersions.CLICommands;
using AutoVersionsDB.Helpers;

namespace AutoVersionsDB.Core.DBVersions
{
    public class DBVersionsCLIBuilder
    {
        private readonly List<CLICommandFactory> _cliCommandFactories;


        public DBVersionsCLIBuilder(SyncCommandFactory syncCommandFactory,
                                    RecreateCommandFactory recreateCommandFactory,
                                    VirtualCommandFactory virtualCommandFactory,
                                    DeployCommandFactory deployCommandFactory,
                                    CreateNewScriptFileCommandFactory createNewScriptFileCommandFactory)
        {
            _cliCommandFactories = new List<CLICommandFactory>()
            {
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
