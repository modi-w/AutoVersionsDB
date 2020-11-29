using AutoVersionsDB.Core.DBVersions;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace AutoVersionsDB.CLI.DBVersions
{
    public class CreateNewRepeatableScriptFileCommandFactory : CLICommandFactory
    {
        private readonly DBVersionsAPI _dbVersionsAPI;
        private readonly IConsoleProcessMessages _consoleProcessMessages;
        private readonly IdCLIOption _idOption;
        private readonly ScriptNameCLIOption _scriptNameOption;

        public CreateNewRepeatableScriptFileCommandFactory(DBVersionsAPI dbVersionsAPI,
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
            Command command = new Command("repeatable")
            {
                _idOption,
                _scriptNameOption,
            };

            command.Description = "Create new repeatable script file";

            command.Handler = CommandHandler.Create<string, string>((id, scriptName) =>
            {
                _consoleProcessMessages.StartProcessMessage("new repeatable", id);

                _consoleProcessMessages.StartSpiiner();
                ProcessResults processResults = _dbVersionsAPI.CreateNewRepeatableScriptFile(id, scriptName, _consoleProcessMessages.OnNotificationStateChanged);
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
