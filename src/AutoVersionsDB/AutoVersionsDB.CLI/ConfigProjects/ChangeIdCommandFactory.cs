﻿using AutoVersionsDB.CLI.ConfigProjects.CLIOptions;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace AutoVersionsDB.CLI.ConfigProjects
{
    public class ChangeIdCommandFactory : CLICommandFactory
    {
        private readonly ProjectConfigsAPI _projectConfigsAPI;
        private readonly IConsoleProcessMessages _consoleProcessMessages;

        private readonly IdCLIOption _IdOption;
        private readonly NewIdCLIOption _newIdCLIOption;




        public ChangeIdCommandFactory(ProjectConfigsAPI projectConfigsAPI,
                                        IConsoleProcessMessages consoleProcessMessages,
                                        IdCLIOption IdOption,
                                        NewIdCLIOption newIdCLIOption)
        {
            _projectConfigsAPI = projectConfigsAPI;
            _consoleProcessMessages = consoleProcessMessages;
            _IdOption = IdOption;
            _newIdCLIOption = newIdCLIOption;
        }

        public override Command Create()
        {
            Command command = new Command("change-id")
            {
                _IdOption,
                _newIdCLIOption
            };

            command.Description = CLITextResources.ChangeIdCommandDescription;

            command.Handler = CommandHandler
                .Create<string, string>((id, newId) =>
                {
                    _consoleProcessMessages.StartProcessMessage("change-id", id);

                    _consoleProcessMessages.StartSpiiner();
                    ProcessResults processResults = _projectConfigsAPI.ChangeProjectId(id, newId, _consoleProcessMessages.OnNotificationStateChanged);
                    _consoleProcessMessages.StopSpinner();

                    _consoleProcessMessages.ProcessComplete(processResults);
                });

            return command;
        }

    }
}
