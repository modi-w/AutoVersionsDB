using AutoVersionsDB.Core.Common.CLI;
using AutoVersionsDB.Core.ConfigProjects.CLICommands.CLIOptions;
using AutoVersionsDB.Core.DBVersions;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;

namespace AutoVersionsDB.Core.ConfigProjects.CLICommands
{
    public class EnvironmentCommandFactory : CLICommandFactory
    {
        private readonly ProjectConfigsAPI _projectConfigsAPI;
        private readonly IConsoleHandler _consoleHandler;

        private readonly IdCLIOption _idOption;
        private readonly DevEnvironmentCLIOption _devEnvironmentOption;




        public EnvironmentCommandFactory(ProjectConfigsAPI projectConfigsAPI,
                                    IConsoleHandler consoleHandler,
                                    IdCLIOption idOption,
                                    DevEnvironmentCLIOption devEnvironmentOption)
        {
            _projectConfigsAPI = projectConfigsAPI;
            _consoleHandler = consoleHandler;
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

            command.Description = "Change project environment configuration. Is run on dev environment (use scripts files) or on a delivery environment (use artifact file).";

            command.Handler = CommandHandler
                .Create<string, bool>((id, dev) =>
                {
                    _consoleHandler.StartProcessMessage("environment", id);

                    ProjectConfigItem existProjectConfig = _projectConfigsAPI.GetProjectConfigById(id);

                 if (existProjectConfig == null)
                 {
                     _consoleHandler.SetErrorMessage($"Id: '{id}' is not exist. You can use the 'init' command to define new project.");
                 }
                 else
                 {
                     existProjectConfig.DevEnvironment = dev;

                     _consoleHandler.StartSpiiner();
                     ProcessResults processResults = _projectConfigsAPI.UpdateProjectConfig(existProjectConfig, _consoleHandler.OnNotificationStateChanged);
                     _consoleHandler.StopSpinner();

                     _consoleHandler.ProcessComplete(processResults.Trace);
                 }
             });

            return command;
        }

    }
}
