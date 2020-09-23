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
    public class RemoveCommandFactory : CLICommandFactory
    {
        private readonly ProjectConfigsAPI _projectConfigsAPI;
        private readonly IConsoleHandler _consoleHandler;

        private readonly CodeCLIOption _codeOption;




        public RemoveCommandFactory(ProjectConfigsAPI projectConfigsAPI,
                                        IConsoleHandler consoleHandler,
                                        CodeCLIOption codeOption)
        {
            _projectConfigsAPI = projectConfigsAPI;
            _consoleHandler = consoleHandler;
            _codeOption = codeOption;
        }

        public override Command Create()
        {
            Command command = new Command("remove")
            {
                _codeOption,
            };

            command.Description = "Remove project";

            command.Handler = CommandHandler
                .Create<string>((code) =>
             {
                 _consoleHandler.StartSpiiner();
                 ProcessResults processResults = _projectConfigsAPI.RemoveProjectConfig(code, _consoleHandler.OnNotificationStateChanged);
                 _consoleHandler.StopSpinner();

                 _consoleHandler.ProcessComplete(processResults.Trace);
             });

            return command;
        }

    }
}
