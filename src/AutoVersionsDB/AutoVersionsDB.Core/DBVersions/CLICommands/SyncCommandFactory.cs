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
        private readonly CodeCLIOption _codeOption;

        public SyncCommandFactory(DBVersionsAPI dbVersionsAPI,
                                    IConsoleHandler consoleHandler,
                                    CodeCLIOption codeOption)
        {
            _dbVersionsAPI = dbVersionsAPI;
            _consoleHandler = consoleHandler;
            _codeOption = codeOption;
        }

        public override Command Create()
        {
            Command command = new Command("sync")
            {
                _codeOption,
            };

            command.Description = "Sync the database to the last state by the scripts files";

            command.Handler = CommandHandler.Create<string>((code) =>
            {
                _consoleHandler.StartSpiiner();
                ProcessResults processResults = _dbVersionsAPI.SyncDB(code, _consoleHandler.OnNotificationStateChanged);
                _consoleHandler.StopSpinner();

                _consoleHandler.ProcessComplete(processResults.Trace);
            });

            return command;
        }
    }
}
