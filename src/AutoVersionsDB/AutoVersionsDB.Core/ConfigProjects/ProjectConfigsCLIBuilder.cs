using System;
using System.Collections.Generic;
using System.Text;
using System.CommandLine;
using AutoVersionsDB.Core.DBVersions.CLICommands;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.ConfigProjects.CLICommands;

namespace AutoVersionsDB.Core.ConfigProjects
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
