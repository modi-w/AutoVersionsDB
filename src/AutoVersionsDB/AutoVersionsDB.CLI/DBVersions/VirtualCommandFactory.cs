using AutoVersionsDB.Core.DBVersions;
using AutoVersionsDB.NotificationableEngine;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace AutoVersionsDB.CLI.DBVersions
{
    public class VirtualCommandFactory : CLICommandFactory
    {
        private readonly DBVersionsAPI _dbVersionsAPI;
        private readonly IConsoleProcessMessages _consoleProcessMessages;
        private readonly IdCLIOption _idOption;
        private readonly TargetCLIOption _targetCLIOption;

        public VirtualCommandFactory(DBVersionsAPI dbVersionsAPI,
                                        IConsoleProcessMessages consoleProcessMessages,
                                        IdCLIOption idOption,
                                        TargetCLIOption targetCLIOption)
        {
            _dbVersionsAPI = dbVersionsAPI;
            _consoleProcessMessages = consoleProcessMessages;
            _idOption = idOption;
            _targetCLIOption = targetCLIOption;
        }

        public override Command Create()
        {
            Command command = new Command("virtual")
            {
                _idOption,
                _targetCLIOption,
            };

            command.Description = "Set the Database to specific state by virtually executions the scripts file. This command is useful when production database didnt use this tool yet. Insert into the 'Target' option the target script file name that you want to set the db state.";

            command.Handler = CommandHandler.Create<string, string>((id, target) =>
            {
                _consoleProcessMessages.StartProcessMessage("virtual", id);

                _consoleProcessMessages.StartSpiiner();
                ProcessResults processResults = _dbVersionsAPI.SetDBStateByVirtualExecution(id, target, _consoleProcessMessages.OnNotificationStateChanged);
                _consoleProcessMessages.StopSpinner();

                _consoleProcessMessages.ProcessComplete(processResults);
            });


            return command;
        }
    }
}
