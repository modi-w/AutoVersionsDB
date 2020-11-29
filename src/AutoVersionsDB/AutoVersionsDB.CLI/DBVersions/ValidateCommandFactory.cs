using AutoVersionsDB.Core.DBVersions;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace AutoVersionsDB.CLI.DBVersions
{
    public class ValidateCommandFactory : CLICommandFactory
    {
        private readonly DBVersionsAPI _dbVersionsAPI;
        private readonly IConsoleProcessMessages _consoleProcessMessages;
        private readonly IdCLIOption _idOption;

        public ValidateCommandFactory(DBVersionsAPI dbVersionsAPI,
                                        IConsoleProcessMessages consoleProcessMessages,
                                        IdCLIOption idOption)
        {
            _dbVersionsAPI = dbVersionsAPI;
            _consoleProcessMessages = consoleProcessMessages;
            _idOption = idOption;
        }

        public override Command Create()
        {
            Command command = new Command("validate")
            {
                _idOption,
            };

            command.Description = "Validate all relevant relevant properties for run sync on the project.";

            command.Handler = CommandHandler.Create<string>((id) =>
            {
                _consoleProcessMessages.StartProcessMessage("validate", id);

                _consoleProcessMessages.StartSpiiner();
                ProcessResults processResults = _dbVersionsAPI.ValidateDBVersions(id, _consoleProcessMessages.OnNotificationStateChanged);
                _consoleProcessMessages.StopSpinner();

                _consoleProcessMessages.ProcessComplete(processResults);
            });

            return command;
        }
    }
}
