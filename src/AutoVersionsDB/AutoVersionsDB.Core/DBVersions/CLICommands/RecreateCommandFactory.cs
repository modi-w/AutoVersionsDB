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
        private readonly CodeCLIOption _codeOption;

        public RecreateCommandFactory(DBVersionsAPI dbVersionsAPI,
                                        IConsoleHandler consoleHandler,
                                        CodeCLIOption codeOption)
        {
            _dbVersionsAPI = dbVersionsAPI;
            _consoleHandler = consoleHandler;
            _codeOption = codeOption;
        }

        public override Command Create()
        {
            Command command = new Command("recreate")
            {
                _codeOption,
            };

            command.Description = "Recreate the database from scratch to the last state only by the scripts files";

            command.Handler = CommandHandler.Create<string>((code) =>
            {
                ProcessTrace processReults;

                _consoleHandler.StartSpiiner();
                processReults = _dbVersionsAPI.RecreateDBFromScratch(code, null, _consoleHandler.OnNotificationStateChanged);
                _consoleHandler.StopSpinner();

                _consoleHandler.ProcessComplete(processReults);
            });

            return command;
        }
    }
}
