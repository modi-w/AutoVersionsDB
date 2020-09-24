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
    public class CreateNewRepeatableScriptFileCommandFactory : CLICommandFactory
    {
        private readonly DBVersionsAPI _dbVersionsAPI;
        private readonly IConsoleHandler _consoleHandler;
        private readonly CodeCLIOption _codeOption;
        private readonly ScriptNameCLIOption _scriptNameOption;

        public CreateNewRepeatableScriptFileCommandFactory(DBVersionsAPI dbVersionsAPI,
                                                            IConsoleHandler consoleHandler,
                                                            CodeCLIOption codeOption,
                                                            ScriptNameCLIOption scriptNameOption)
        {
            _dbVersionsAPI = dbVersionsAPI;
            _consoleHandler = consoleHandler;
            _codeOption = codeOption;
            _scriptNameOption = scriptNameOption;
        }

        public override Command Create()
        {
            Command command = new Command("repeatable")
            {
                _codeOption,
                _scriptNameOption,
            };

            command.Description = "Create new repeatable script file";

            command.Handler = CommandHandler.Create<string, string>((code, scriptName) =>
            {
                _consoleHandler.StartProcessMessage("new repeatable", code);
 
                _consoleHandler.StartSpiiner();
                ProcessResults processResults = _dbVersionsAPI.CreateNewRepeatableScriptFile(code, scriptName, _consoleHandler.OnNotificationStateChanged);
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
