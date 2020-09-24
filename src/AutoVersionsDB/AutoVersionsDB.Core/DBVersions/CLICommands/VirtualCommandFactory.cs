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
    public class VirtualCommandFactory : CLICommandFactory
    {
        private readonly DBVersionsAPI _dbVersionsAPI;
        private readonly IConsoleHandler _consoleHandler;
        private readonly IdCLIOption _idOption;
        private readonly TargetCLIOption _targetCLIOption;

        public VirtualCommandFactory(DBVersionsAPI dbVersionsAPI,
                                        IConsoleHandler consoleHandler,
                                        IdCLIOption idOption,
                                        TargetCLIOption targetCLIOption)
        {
            _dbVersionsAPI = dbVersionsAPI;
            _consoleHandler = consoleHandler;
            _idOption = idOption;
            _targetCLIOption = targetCLIOption;
        }

        public override Command Create()
        {
            Command command = new Command("virtual")
            {
                _idOption,
                _targetCLIOption,
            };

            command.Description = "Set the Database to specific state by virtually executions the scripts file. This command is useful when production database didnt use this tool yet. Insert into the 'Target' option the target script file name that you want to set the db state.";

            command.Handler = CommandHandler.Create<string, string>((id, target) =>
            {
                _consoleHandler.StartProcessMessage("virtual", id);

                _consoleHandler.StartSpiiner();
                ProcessResults processResults = _dbVersionsAPI.SetDBStateByVirtualExecution(id, target, _consoleHandler.OnNotificationStateChanged);
                _consoleHandler.StopSpinner();

                _consoleHandler.ProcessComplete(processResults.Trace);
            });


            return command;
        }
    }
}
