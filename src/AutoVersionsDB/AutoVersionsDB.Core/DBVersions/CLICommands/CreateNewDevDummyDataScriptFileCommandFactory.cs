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
    public class CreateNewDevDummyDataScriptFileCommandFactory : CLICommandFactory
    {
        private readonly DBVersionsAPI _dbVersionsAPI;
        private readonly IConsoleHandler _consoleHandler;
        private readonly IdCLIOption _idOption;
        private readonly ScriptNameCLIOption _scriptNameOption;

        public CreateNewDevDummyDataScriptFileCommandFactory(DBVersionsAPI dbVersionsAPI,
                                    IConsoleHandler consoleHandler,
                                    IdCLIOption idOption,
                                    ScriptNameCLIOption scriptNameOption)
        {
            _dbVersionsAPI = dbVersionsAPI;
            _consoleHandler = consoleHandler;
            _idOption = idOption;
            _scriptNameOption = scriptNameOption;
        }

        public override Command Create()
        {
            Command command = new Command("ddd")
            {
                _idOption,
                _scriptNameOption,
            };

            command.Description = "Create new dev dummy data script file";

            command.Handler = CommandHandler.Create<string, string>((id, scriptName) =>
            {
                _consoleHandler.StartProcessMessage("new ddd", id);

                _consoleHandler.StartSpiiner();
                ProcessResults processResults = _dbVersionsAPI.CreateNewDevDummyDataScriptFile(id, scriptName, _consoleHandler.OnNotificationStateChanged);
                _consoleHandler.StopSpinner();

                _consoleHandler.ProcessComplete(processResults.Trace);

                if (!processResults.Trace.HasError)
                {
                    string newFilePath = (string)processResults.Results;
                    _consoleHandler.SetInfoMessage($"The file: '{newFilePath}' is created.");
                }
            });

            return command;
        }
    }
}
