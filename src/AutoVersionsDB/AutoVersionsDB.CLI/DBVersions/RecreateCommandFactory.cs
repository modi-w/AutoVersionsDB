using AutoVersionsDB.Core.DBVersions;
using AutoVersionsDB.Core.DBVersions.Processes;
using AutoVersionsDB.NotificationableEngine;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace AutoVersionsDB.CLI.DBVersions
{
    public class RecreateCommandFactory : CLICommandFactory
    {
        private readonly DBVersionsAPI _dbVersionsAPI;
        private readonly IConsoleProcessMessages _consoleProcessMessages;
        private readonly IdCLIOption _idOption;

        public RecreateCommandFactory(DBVersionsAPI dbVersionsAPI,
                                        IConsoleProcessMessages consoleProcessMessages,
                                        IdCLIOption idOption)
        {
            _dbVersionsAPI = dbVersionsAPI;
            _consoleProcessMessages = consoleProcessMessages;
            _idOption = idOption;
        }

        public override Command Create()
        {
            Command command = new Command("recreate")
            {
                _idOption,
            };

            command.Description = CLITextResources.RecreateCommandDescription;

            command.Handler = CommandHandler.Create<string>((id) =>
            {
                _consoleProcessMessages.StartProcessMessage("recreate", id);

                _consoleProcessMessages.StartSpiiner();
                ProcessResults processResults = _dbVersionsAPI.RecreateDBFromScratch(id, TargetScripts.CreateLastState(), _consoleProcessMessages.OnNotificationStateChanged);
                _consoleProcessMessages.StopSpinner();

                _consoleProcessMessages.ProcessComplete(processResults);
            });

            return command;
        }
    }
}
