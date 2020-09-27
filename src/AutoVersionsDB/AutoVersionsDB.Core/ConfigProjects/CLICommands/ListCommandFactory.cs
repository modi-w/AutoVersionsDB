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
                .Create(() =>
                {
                    _consoleHandler.StartProcessMessage("list");

                    IEnumerable<ProjectConfigItem> projectsList = _projectConfigsAPI.GetProjectsList();

                 if (!projectsList.Any())
                 {
                     _consoleHandler.SetInfoMessage("----  No projects found on this machine  ----");
                 }
                 else
                 {
                     string captionsMessage = $"{"  Id".PadRight(31)} |  Description";
                     _consoleHandler.SetInfoMessage(captionsMessage);

                     string captionsLineMessage = "-".PadRight(55, '-');
                     _consoleHandler.SetInfoMessage(captionsLineMessage);

                     foreach (ProjectConfigItem projectConfig in projectsList)
                     {
                         string id = projectConfig.Id.Ellipsis(30);

                         string projectLineMessage = $" {id.PadRight(30)} | {projectConfig.Description}";
                         _consoleHandler.SetInfoMessage(projectLineMessage);
                     }

                 }

             });

            return command;
        }

        
    }
}
