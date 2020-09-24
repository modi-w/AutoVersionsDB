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
    public class DeployCommandFactory : CLICommandFactory
    {
        private readonly DBVersionsAPI _dbVersionsAPI;
        private readonly IConsoleHandler _consoleHandler;
        private readonly IdCLIOption _idOption;

        public DeployCommandFactory(DBVersionsAPI dbVersionsAPI,
                                    IConsoleHandler consoleHandler,
                                    IdCLIOption idOption)
        {
            _dbVersionsAPI = dbVersionsAPI;
            _consoleHandler = consoleHandler;
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
                _consoleHandler.StartProcessMessage("deploy", id);

                _consoleHandler.StartSpiiner();
                ProcessResults processResults = _dbVersionsAPI.Deploy(id, _consoleHandler.OnNotificationStateChanged);
                _consoleHandler.StopSpinner();

                _consoleHandler.ProcessComplete(processResults.Trace);

                if (!processResults.Trace.HasError)
                {
                    string deployFilePath = (string)processResults.Results;
                    _consoleHandler.SetInfoMessage($"Artifact file created: '{deployFilePath}'");
                }
            });

            return command;
        }
    }
}
