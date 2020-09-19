using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace AutoVersionsDB.Core
{
    public class AutoVersionsDBCLI
    {
        private readonly ProjectConfigsCLIBuilder _projectConfigsCLIBuilder;
        private readonly DBVersionsCLIBuilder _dbVersionsCLIBuilder;
        


        public AutoVersionsDBCLI(ProjectConfigsCLIBuilder projectConfigsCLIBuilder,
                                    DBVersionsCLIBuilder dbVersionsCLIBuilder)
        {
            _projectConfigsCLIBuilder = projectConfigsCLIBuilder;
            _dbVersionsCLIBuilder = dbVersionsCLIBuilder;
        }


        public int Run(string[] args)
        {
            RootCommand rootCommand = new RootCommand();

            _projectConfigsCLIBuilder.Build(rootCommand);
            _dbVersionsCLIBuilder.Build(rootCommand);

            return rootCommand.InvokeAsync(args).Result;
        }
    }
}
