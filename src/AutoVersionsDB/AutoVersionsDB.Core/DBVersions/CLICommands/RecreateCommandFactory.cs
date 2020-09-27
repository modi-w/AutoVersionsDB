using AutoVersionsDB.Core.Common.CLI;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;

namespace AutoVersionsDB.Core.DBVersions.CLICommands
{
    public class RecreateCommandFactory : CLICommandFactory
    {
        private readonly DBVersionsAPI _dbVersionsAPI;
        private readonly IConsoleHandler _consoleHandler;
        private readonly IdCLIOption _idOption;

        public RecreateCommandFactory(DBVersionsAPI dbVersionsAPI,
                                        IConsoleHandler consoleHandler,
                                        IdCLIOption idOption)
        {
            _dbVersionsAPI = dbVersionsAPI;
            _consoleHandler = consoleHandler;
            _idOption = idOption;
        }

        public override Command Create()
        {
            Command command = new Command("recreate")
            {
                _idOption,
            };

            command.Description = "Recreate the database from scratch to the last state only by the scripts files";

            command.Handler = CommandHandler.Create<string>((id) =>
            {
                _consoleHandler.StartProcessMessage("recreate", id);

                _consoleHandler.StartSpiiner();
                ProcessResults processResults = _dbVersionsAPI.RecreateDBFromScratch(id, null, _consoleHandler.OnNotificationStateChanged);
                _consoleHandler.StopSpinner();

                _consoleHandler.ProcessComplete(processResults.Trace);
            });

            return command;
        }
    }
}
