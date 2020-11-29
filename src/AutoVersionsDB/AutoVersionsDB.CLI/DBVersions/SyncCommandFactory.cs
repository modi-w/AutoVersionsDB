using AutoVersionsDB.Core.DBVersions;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Runtime.InteropServices;
using System.Text;

namespace AutoVersionsDB.CLI.DBVersions
{
    public class SyncCommandFactory : CLICommandFactory
    {
        private readonly DBVersionsAPI _dbVersionsAPI;
        private readonly IConsoleProcessMessages _consoleProcessMessages;
        private readonly IdCLIOption _idOption;

        public SyncCommandFactory(DBVersionsAPI dbVersionsAPI,
                                    IConsoleProcessMessages consoleProcessMessages,
                                    IdCLIOption idOption)
        {
            _dbVersionsAPI = dbVersionsAPI;
            _consoleProcessMessages = consoleProcessMessages;
            _idOption = idOption;
        }

        public override Command Create()
        {
            Command command = new Command("sync")
            {
                _idOption,
            };

            command.Description = "Sync the database to the last state by the scripts files";

            command.Handler = CommandHandler.Create<string>((id) =>
            {
                _consoleProcessMessages.StartProcessMessage("sync", id);

                _consoleProcessMessages.StartSpiiner();
                ProcessResults processResults = _dbVersionsAPI.SyncDB(id, _consoleProcessMessages.OnNotificationStateChanged);
                _consoleProcessMessages.StopSpinner();

                _consoleProcessMessages.ProcessComplete(processResults);
            });

            return command;
        }
    }
}
