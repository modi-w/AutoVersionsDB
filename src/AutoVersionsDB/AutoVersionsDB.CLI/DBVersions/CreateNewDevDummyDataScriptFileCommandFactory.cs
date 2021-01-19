using AutoVersionsDB.Core.DBVersions;
using AutoVersionsDB.NotificationableEngine;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace AutoVersionsDB.CLI.DBVersions
{
    public class CreateNewDevDummyDataScriptFileCommandFactory : CLICommandFactory
    {
        private readonly DBVersionsAPI _dbVersionsAPI;
        private readonly IConsoleProcessMessages _consoleProcessMessages;
        private readonly IdCLIOption _idOption;
        private readonly ScriptNameCLIOption _scriptNameOption;

        public CreateNewDevDummyDataScriptFileCommandFactory(DBVersionsAPI dbVersionsAPI,
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
            Command command = new Command("ddd")
            {
                _idOption,
                _scriptNameOption,
            };

            command.Description = CLITextResources.CreateNewDevDummyDataScriptFileCommandDescription;

            command.Handler = CommandHandler.Create<string, string>((id, scriptName) =>
            {
                _consoleProcessMessages.StartProcessMessage("new ddd", id);

                _consoleProcessMessages.StartSpiiner();
                ProcessResults processResults = _dbVersionsAPI.CreateNewDevDummyDataScriptFile(id, scriptName, _consoleProcessMessages.OnNotificationStateChanged);
                _consoleProcessMessages.StopSpinner();

                _consoleProcessMessages.ProcessComplete(processResults);

                if (!processResults.Trace.HasError)
                {
                    string newFilePath = (string)processResults.Results;
                    _consoleProcessMessages.SetInfoMessage(CLITextResources.TheFileIsCreatedInfoMessage.Replace("[newFilePath]", newFilePath));
                }
            });

            return command;
        }
    }
}
