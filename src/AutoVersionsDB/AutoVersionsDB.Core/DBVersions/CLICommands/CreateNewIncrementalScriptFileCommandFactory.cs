﻿using AutoVersionsDB.Core.Common.CLI;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;

namespace AutoVersionsDB.Core.DBVersions.CLICommands
{
    public class CreateNewIncrementalScriptFileCommandFactory : CLICommandFactory
    {
        private readonly DBVersionsAPI _dbVersionsAPI;
        private readonly IConsoleProcessMessages _consoleProcessMessages;
        private readonly IdCLIOption _idOption;
        private readonly ScriptNameCLIOption _scriptNameOption;

        public CreateNewIncrementalScriptFileCommandFactory(DBVersionsAPI dbVersionsAPI,
                                    IConsoleProcessMessages consoleProcessMessages,
                                    IdCLIOption idOption,
                                    ScriptNameCLIOption scriptNameOption)
        {
            _dbVersionsAPI = dbVersionsAPI;
            _consoleProcessMessages = consoleProcessMessages;
            _idOption = idOption;
            _scriptNameOption = scriptNameOption;
        }

        public override Command Create()
        {
            Command command = new Command("incremental")
            {
                _idOption,
                _scriptNameOption,
            };

            command.Description = "Create new incremental script file";

            command.Handler = CommandHandler.Create<string, string>((id, scriptName) =>
            {
                _consoleProcessMessages.StartProcessMessage("new incremental", id);

                _consoleProcessMessages.StartSpiiner();
                ProcessResults processResults = _dbVersionsAPI.CreateNewIncrementalScriptFile(id, scriptName, _consoleProcessMessages.OnNotificationStateChanged);
                _consoleProcessMessages.StopSpinner();

                _consoleProcessMessages.ProcessComplete(processResults);

                if (!processResults.Trace.HasError)
                {
                    string newFilePath = (string)processResults.Results;
                    _consoleProcessMessages.SetInfoMessage($"The file: '{newFilePath}' is created.");
                }
            });

            return command;
        }
    }
}