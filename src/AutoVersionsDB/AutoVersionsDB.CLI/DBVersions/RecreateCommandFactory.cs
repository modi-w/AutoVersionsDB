using AutoVersionsDB.Core.DBVersions;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;

namespace AutoVersionsDB.CLI.DBVersions
{
    public class RecreateCommandFactory : CLICommandFactory
    {
        private readonly DBVersionsAPI _dbVersionsAPI;
        private readonly IConsoleProcessMessages _consoleProcessMessages;
        private readonly IdCLIOption _idOption;

        public RecreateCommandFactory(DBVersionsAPI dbVersionsAPI,
                                        IConsoleProcessMessages consoleProcessMessages,
                                        IdCLIOption idOption)
        {
            _dbVersionsAPI = dbVersionsAPI;
            _consoleProcessMessages = consoleProcessMessages;
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
                _consoleProcessMessages.StartProcessMessage("recreate", id);

                _consoleProcessMessages.StartSpiiner();
                ProcessResults processResults = _dbVersionsAPI.RecreateDBFromScratch(id, null, _consoleProcessMessages.OnNotificationStateChanged);
                _consoleProcessMessages.StopSpinner();

                _consoleProcessMessages.ProcessComplete(processResults);
            });

            return command;
        }
    }
}
