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
    public class ChangeIdCommandFactory : CLICommandFactory
    {
        private readonly ProjectConfigsAPI _projectConfigsAPI;
        private readonly IConsoleHandler _consoleHandler;

        private readonly IdCLIOption _IdOption;
        private readonly NewIdCLIOption _newIdCLIOption;




        public ChangeIdCommandFactory(ProjectConfigsAPI projectConfigsAPI,
                                        IConsoleHandler consoleHandler,
                                        IdCLIOption IdOption,
                                        NewIdCLIOption newIdCLIOption)
        {
            _projectConfigsAPI = projectConfigsAPI;
            _consoleHandler = consoleHandler;
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

            command.Description = "Change project project id. Change the identifier of the project.";

            command.Handler = CommandHandler
                .Create<string, string>((id, newId) =>
                {
                    _consoleHandler.StartProcessMessage("change-id", id);

                    _consoleHandler.StartSpiiner();
                    ProcessResults processResults = _projectConfigsAPI.ChangeProjectId(id, newId, _consoleHandler.OnNotificationStateChanged);
                    _consoleHandler.StopSpinner();

                    _consoleHandler.ProcessComplete(processResults.Trace);
                });

            return command;
        }

    }
}
