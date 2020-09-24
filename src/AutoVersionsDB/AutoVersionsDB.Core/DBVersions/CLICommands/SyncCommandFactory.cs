using AutoVersionsDB.Core.Common.CLI;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Runtime.InteropServices;
using System.Text;

namespace AutoVersionsDB.Core.DBVersions.CLICommands
{
    public class SyncCommandFactory : CLICommandFactory
    {
        private readonly DBVersionsAPI _dbVersionsAPI;
        private readonly IConsoleHandler _consoleHandler;
        private readonly IdCLIOption _idOption;

        public SyncCommandFactory(DBVersionsAPI dbVersionsAPI,
                                    IConsoleHandler consoleHandler,
                                    IdCLIOption idOption)
        {
            _dbVersionsAPI = dbVersionsAPI;
            _consoleHandler = consoleHandler;
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
                _consoleHandler.StartProcessMessage("sync", id);

                _consoleHandler.StartSpiiner();
                ProcessResults processResults = _dbVersionsAPI.SyncDB(id, _consoleHandler.OnNotificationStateChanged);
                _consoleHandler.StopSpinner();

                _consoleHandler.ProcessComplete(processResults.Trace);
            });

            return command;
        }
    }
}
