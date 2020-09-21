using AutoVersionsDB.Core.Common.CLI;
using AutoVersionsDB.Core.ConfigProjects.CLICommands.CLIOptions;
using AutoVersionsDB.Core.DBVersions;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.Core.ConfigProjects.CLICommands
{
    public class ListCommandFactory : CLICommandFactory
    {
        private readonly ProjectConfigsAPI _projectConfigsAPI;
        private readonly IConsoleHandler _consoleHandler;




        public ListCommandFactory(ProjectConfigsAPI projectConfigsAPI,
                                        IConsoleHandler consoleHandler)
        {
            _projectConfigsAPI = projectConfigsAPI;
            _consoleHandler = consoleHandler;
        }

        public override Command Create()
        {
            Command command = new Command("list");

            command.Description = "show list of all projects";

            command.Handler = CommandHandler
                .Create<string, string>((code, newCode) =>
             {
                 IEnumerable<ProjectConfigItem> projectsList = _projectConfigsAPI.GetProjectsList();

                 if (!projectsList.Any())
                 {
                     _consoleHandler.WriteLineInfo("----  No projects found on this machine  ----");
                 }
                 else
                 {
                     string captionsMessage = $"{"  Code".PadRight(31)} |  Description";
                     _consoleHandler.WriteLineInfo(captionsMessage);

                     string captionsLineMessage = "-".PadRight(55, '-');
                     _consoleHandler.WriteLineInfo(captionsLineMessage);

                     foreach (ProjectConfigItem projectConfig in projectsList)
                     {
                         string projectCode = projectConfig.Code.Ellipsis(30);

                         string projectLineMessage = $" {projectCode.PadRight(30)} | {projectConfig.Description}";
                         _consoleHandler.WriteLineInfo(projectLineMessage);
                     }

                 }

             });

            return command;
        }

        
    }
}
