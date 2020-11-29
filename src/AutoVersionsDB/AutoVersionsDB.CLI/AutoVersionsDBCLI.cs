using AutoVersionsDB.CLI.ConfigProjects;
using AutoVersionsDB.CLI.DBVersions;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.CLI
{
    public class AutoVersionsDBCLI
    {
        private readonly ProjectConfigsCLIBuilder _projectConfigsCLIBuilder;
        private readonly DBVersionsCLIBuilder _dbVersionsCLIBuilder;
        private readonly IConsoleExtended _console;



        public AutoVersionsDBCLI(ProjectConfigsCLIBuilder projectConfigsCLIBuilder,
                                    DBVersionsCLIBuilder dbVersionsCLIBuilder,
                                    IConsoleExtended console)
        {
            _projectConfigsCLIBuilder = projectConfigsCLIBuilder;
            _dbVersionsCLIBuilder = dbVersionsCLIBuilder;
            _console = console;
        }


        public int Run(string[] args)
        {
            RootCommand rootCommand = createCli();

            return rootCommand.InvokeAsync(args, _console).Result;
        }

        public int Run(string args)
        {
            RootCommand rootCommand = createCli();

            return rootCommand.InvokeAsync(args, _console).Result;
        }

        private RootCommand createCli()
        {
            RootCommand rootCommand = new RootCommand();

            _projectConfigsCLIBuilder.Build(rootCommand);
            _dbVersionsCLIBuilder.Build(rootCommand);
            return rootCommand;
        }

    }
}
