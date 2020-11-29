using AutoVersionsDB.Helpers;
using System.Collections.Generic;
using System.CommandLine;

namespace AutoVersionsDB.CLI.ConfigProjects
{
    public class ProjectConfigsCLIBuilder
    {
        private readonly List<CLICommandFactory> _cliCommandFactories;


        public ProjectConfigsCLIBuilder(DBTypesCommandFactory dbTypesCommandFactory,
                                        ListCommandFactory listCommandFactory,
                                        InfoCommandFactory infoCommandFactory,
                                        InitCommandFactory initCommandFactory,
                                        ConfigCommandFactory configCommandFactory,
                                        RemoveCommandFactory removeCommandFactory)
        {
            _cliCommandFactories = new List<CLICommandFactory>()
            {
                dbTypesCommandFactory,
                listCommandFactory,
                infoCommandFactory,
                initCommandFactory,
                configCommandFactory,
                removeCommandFactory,
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
