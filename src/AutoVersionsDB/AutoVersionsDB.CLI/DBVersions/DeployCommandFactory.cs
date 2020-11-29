using AutoVersionsDB.Core.DBVersions;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace AutoVersionsDB.CLI.DBVersions
{
    public class DeployCommandFactory : CLICommandFactory
    {
        private readonly DBVersionsAPI _dbVersionsAPI;
        private readonly IConsoleProcessMessages _consoleProcessMessages;
        private readonly IdCLIOption _idOption;

        public DeployCommandFactory(DBVersionsAPI dbVersionsAPI,
                                    IConsoleProcessMessages consoleProcessMessages,
                                    IdCLIOption idOption)
        {
            _dbVersionsAPI = dbVersionsAPI;
            _consoleProcessMessages = consoleProcessMessages;
            _idOption = idOption;
        }

        public override Command Create()
        {
            Command command = new Command("deploy")
            {
                _idOption,
            };

            command.Description = "Deploy the project. Create an artifact file ready to use on delivery enviornment.";

            command.Handler = CommandHandler.Create<string>((id) =>
            {
                _consoleProcessMessages.StartProcessMessage("deploy", id);

                _consoleProcessMessages.StartSpiiner();
                ProcessResults processResults = _dbVersionsAPI.Deploy(id, _consoleProcessMessages.OnNotificationStateChanged);
                _consoleProcessMessages.StopSpinner();

                _consoleProcessMessages.ProcessComplete(processResults);

                if (!processResults.Trace.HasError)
                {
                    string deployFilePath = (string)processResults.Results;
                    _consoleProcessMessages.SetInfoMessage($"Artifact file created: '{deployFilePath}'");
                }
            });

            return command;
        }
    }
}
