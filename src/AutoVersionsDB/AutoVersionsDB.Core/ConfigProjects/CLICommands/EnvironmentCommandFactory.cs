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

        private readonly CodeCLIOption _codeOption;
        private readonly DevEnvironmentCLIOption _devEnvironmentOption;




        public EnvironmentCommandFactory(ProjectConfigsAPI projectConfigsAPI,
                                    IConsoleHandler consoleHandler,
                                    CodeCLIOption codeOption,
                                    DevEnvironmentCLIOption devEnvironmentOption)
        {
            _projectConfigsAPI = projectConfigsAPI;
            _consoleHandler = consoleHandler;
            _codeOption = codeOption;
            _devEnvironmentOption = devEnvironmentOption;
        }

        public override Command Create()
        {
            Command command = new Command("environment")
            {
                _codeOption,
                _devEnvironmentOption,
            };

            command.Description = "Change project environment configuration. Is run on dev environment (use scripts files) or on a delivery environment (use artifact file).";

            command.Handler = CommandHandler
                .Create<string, bool>((code, dev) =>
             {
                 ProjectConfigItem existProjectConfig = _projectConfigsAPI.GetProjectConfigByProjectCode(code);

                 if (existProjectConfig == null)
                 {
                     _consoleHandler.SetErrorMessage($"Project Code: '{code}' is not exist. You can use the 'init' command to define new project.");
                 }
                 else
                 {
                     existProjectConfig.DevEnvironment = dev;

                     _consoleHandler.StartSpiiner();
                     ProcessTrace processReults = _projectConfigsAPI.UpdateProjectConfig(existProjectConfig, _consoleHandler.OnNotificationStateChanged);
                     _consoleHandler.StopSpinner();

                     _consoleHandler.ProcessComplete(processReults);
                 }
             });

            return command;
        }

    }
}
