using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace AutoVersionsDB.CLI.ConfigProjects
{
    public class RemoveCommandFactory : CLICommandFactory
    {
        private readonly ProjectConfigsAPI _projectConfigsAPI;
        private readonly IConsoleProcessMessages _consoleProcessMessages;

        private readonly IdCLIOption _idOption;




        public RemoveCommandFactory(ProjectConfigsAPI projectConfigsAPI,
                                        IConsoleProcessMessages consoleProcessMessages,
                                        IdCLIOption idOption)
        {
            _projectConfigsAPI = projectConfigsAPI;
            _consoleProcessMessages = consoleProcessMessages;
            _idOption = idOption;
        }

        public override Command Create()
        {
            Command command = new Command("remove")
            {
                _idOption,
            };

            command.Description = "Remove project";

            command.Handler = CommandHandler
                .Create<string>((id) =>
                {
                    _consoleProcessMessages.StartProcessMessage("remove", id);

                    _consoleProcessMessages.StartSpiiner();
                    ProcessResults processResults = _projectConfigsAPI.RemoveProjectConfig(id, _consoleProcessMessages.OnNotificationStateChanged);
                    _consoleProcessMessages.StopSpinner();

                    _consoleProcessMessages.ProcessComplete(processResults);
                });

            return command;
        }

    }
}
