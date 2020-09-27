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

        private readonly IdCLIOption _idOption;




        public RemoveCommandFactory(ProjectConfigsAPI projectConfigsAPI,
                                        IConsoleHandler consoleHandler,
                                        IdCLIOption idOption)
        {
            _projectConfigsAPI = projectConfigsAPI;
            _consoleHandler = consoleHandler;
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
                    _consoleHandler.StartProcessMessage("remove", id);

                    _consoleHandler.StartSpiiner();
                    ProcessResults processResults = _projectConfigsAPI.RemoveProjectConfig(id, _consoleHandler.OnNotificationStateChanged);
                    _consoleHandler.StopSpinner();

                    _consoleHandler.ProcessComplete(processResults.Trace);
                });

            return command;
        }

    }
}
