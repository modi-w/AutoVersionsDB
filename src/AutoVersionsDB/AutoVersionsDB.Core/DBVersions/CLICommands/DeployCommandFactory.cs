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
        private readonly CodeCLIOption _codeOption;

        public DeployCommandFactory(DBVersionsAPI dbVersionsAPI,
                                    IConsoleHandler consoleHandler,
                                    CodeCLIOption codeOption)
        {
            _dbVersionsAPI = dbVersionsAPI;
            _consoleHandler = consoleHandler;
            _codeOption = codeOption;
        }

        public override Command Create()
        {
            Command command = new Command("deploy")
            {
                _codeOption,
            };

            command.Description = "Deploy the project. Create an artifact file ready to use on delivery enviornment.";

            command.Handler = CommandHandler.Create<string>((code) =>
            {
                _consoleHandler.StartSpiiner();
                ProcessTrace processReults = _dbVersionsAPI.Deploy(code, _consoleHandler.OnNotificationStateChanged);
                _consoleHandler.StopSpinner();

                _consoleHandler.ProcessComplete(processReults);
            });

            return command;
        }
    }
}
