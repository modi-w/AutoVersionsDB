using AutoVersionsDB.Core.Common.CLI;
using AutoVersionsDB.Core.ConfigProjects.CLICommands.CLIOptions;
using AutoVersionsDB.Core.DBVersions;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;

namespace AutoVersionsDB.Core.ConfigProjects.CLICommands
{
    public class ChangeCodeCommandFactory : CLICommandFactory
    {
        private readonly ProjectConfigsAPI _projectConfigsAPI;
        private readonly IConsoleHandler _consoleHandler;

        private readonly CodeCLIOption _codeOption;
        private readonly NewCodeCLIOption _newCodeCLIOption;




        public ChangeCodeCommandFactory(ProjectConfigsAPI projectConfigsAPI,
                                        IConsoleHandler consoleHandler,
                                        CodeCLIOption codeOption,
                                        NewCodeCLIOption newCodeCLIOption)
        {
            _projectConfigsAPI = projectConfigsAPI;
            _consoleHandler = consoleHandler;
            _codeOption = codeOption;
            _newCodeCLIOption = newCodeCLIOption;
        }

        public override Command Create()
        {
            Command command = new Command("change-code")
            {
                _codeOption,
                _newCodeCLIOption
            };

            command.Description = "Change project project code. Change the identifier of the project.";

            command.Handler = CommandHandler
                .Create<string, string>((code, newCode) =>
             {
                 _consoleHandler.StartSpiiner();
                 ProcessResults processResults = _projectConfigsAPI.ChangeProjectCode(code, newCode, _consoleHandler.OnNotificationStateChanged);
                 _consoleHandler.StopSpinner();

                 _consoleHandler.ProcessComplete(processResults.Trace);
             });

            return command;
        }

    }
}
