﻿using AutoVersionsDB.CLI.ConfigProjects.CLIOptions;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace AutoVersionsDB.CLI.ConfigProjects
{
    public class EnvironmentCommandFactory : CLICommandFactory
    {
        private readonly ProjectConfigsAPI _projectConfigsAPI;
        private readonly IConsoleProcessMessages _consoleProcessMessages;

        private readonly IdCLIOption _idOption;
        private readonly DevEnvironmentCLIOption _devEnvironmentOption;




        public EnvironmentCommandFactory(ProjectConfigsAPI projectConfigsAPI,
                                    IConsoleProcessMessages consoleProcessMessages,
                                    IdCLIOption idOption,
                                    DevEnvironmentCLIOption devEnvironmentOption)
        {
            _projectConfigsAPI = projectConfigsAPI;
            _consoleProcessMessages = consoleProcessMessages;
            _idOption = idOption;
            _devEnvironmentOption = devEnvironmentOption;
        }

        public override Command Create()
        {
            Command command = new Command("environment")
            {
                _idOption,
                _devEnvironmentOption,
            };

            command.Description = CLITextResources.EnvironmentCommandDescription;

            command.Handler = CommandHandler
                .Create<string, bool>((id, dev) =>
                {
                    _consoleProcessMessages.StartProcessMessage("environment", id);

                    ProjectConfigItem existProjectConfig = _projectConfigsAPI.GetProjectConfigById(id);

                    if (existProjectConfig == null)
                    {
                        _consoleProcessMessages.SetErrorInstruction(CLITextResources.IdNotExistCommandError.Replace("[Id]", id), NotificationErrorType.Error);
                    }
                    else
                    {
                        existProjectConfig.DevEnvironment = dev;

                        _consoleProcessMessages.StartSpiiner();
                        ProcessResults processResults = _projectConfigsAPI.UpdateProjectConfig(existProjectConfig, _consoleProcessMessages.OnNotificationStateChanged);
                        _consoleProcessMessages.StopSpinner();

                        _consoleProcessMessages.ProcessComplete(processResults);
                    }
                });

            return command;
        }

    }
}
