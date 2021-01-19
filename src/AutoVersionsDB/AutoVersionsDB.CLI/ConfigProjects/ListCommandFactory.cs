using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Helpers;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;

namespace AutoVersionsDB.CLI.ConfigProjects
{
    public class ListCommandFactory : CLICommandFactory
    {
        private readonly ProjectConfigsAPI _projectConfigsAPI;
        private readonly IConsoleProcessMessages _consoleProcessMessages;




        public ListCommandFactory(ProjectConfigsAPI projectConfigsAPI,
                                        ConsoleProcessMessages consoleProcessMessages)
        {
            _projectConfigsAPI = projectConfigsAPI;
            _consoleProcessMessages = consoleProcessMessages;
        }

        public override Command Create()
        {
            Command command = new Command("list")
            {
                Description = CLITextResources.ListCommandDescription,

                Handler = CommandHandler
                .Create(() =>
                {
                    _consoleProcessMessages.StartProcessMessage("list");

                    _consoleProcessMessages.SetInfoMessage("");

                    IEnumerable<ProjectConfigItem> projectsList = _projectConfigsAPI.GetProjectsList();

                    if (!projectsList.Any())
                    {
                        _consoleProcessMessages.SetInfoMessage(CLITextResources.ProjectNotFoundInfoMessage);
                    }
                    else
                    {
                        string captionsMessage = $"{"  Id",-31} |  Description";
                        _consoleProcessMessages.SetInfoMessage(captionsMessage);

                        string captionsLineMessage = "-".PadRight(55, '-');
                        _consoleProcessMessages.SetInfoMessage(captionsLineMessage);

                        foreach (ProjectConfigItem projectConfig in projectsList)
                        {
                            string id = projectConfig.Id.Ellipsis(30);

                            string projectLineMessage = $" {id,-30} | {projectConfig.Description}";
                            _consoleProcessMessages.SetInfoMessage(projectLineMessage);
                        }

                    }

                })
            };

            return command;
        }


    }
}
