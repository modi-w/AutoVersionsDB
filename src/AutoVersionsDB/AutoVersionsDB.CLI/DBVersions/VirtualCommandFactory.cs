using AutoVersionsDB.Core.DBVersions;
using AutoVersionsDB.Core.DBVersions.Processes;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
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
        private readonly IncTargetCLIOption _incTargetCLIOption;
        private readonly RptTargetCLIOption _rptTargetCLIOption;
        private readonly DDDTargetCLIOption _dddTargetCLIOption;

        public VirtualCommandFactory(DBVersionsAPI dbVersionsAPI,
                                        IConsoleProcessMessages consoleProcessMessages,
                                        IdCLIOption idOption,
                                        IncTargetCLIOption incTargetCLIOption,
                                        RptTargetCLIOption rptTargetCLIOption,
                                        DDDTargetCLIOption dddTargetCLIOption)
        {
            _dbVersionsAPI = dbVersionsAPI;
            _consoleProcessMessages = consoleProcessMessages;
            _idOption = idOption;
            _incTargetCLIOption = incTargetCLIOption;
            _rptTargetCLIOption = rptTargetCLIOption;
            _dddTargetCLIOption = dddTargetCLIOption;
        }

        public override Command Create()
        {
            Command command = new Command("virtual")
            {
                _idOption,
                _incTargetCLIOption,
                _rptTargetCLIOption,
                _dddTargetCLIOption,
         };

            command.Description = CLITextResources.VirtualCommandDescription;

            command.Handler = CommandHandler.Create<string, string, string, string>((id, incTarget, rptTarget, dddTarget) =>
            {
                _consoleProcessMessages.StartProcessMessage("virtual", id);

                _consoleProcessMessages.StartSpiiner();

                TargetScripts targetScripts = new TargetScripts(incTarget, rptTarget, dddTarget);

                ProcessResults processResults = _dbVersionsAPI.SetDBStateByVirtualExecution(id, targetScripts, _consoleProcessMessages.OnNotificationStateChanged);
                _consoleProcessMessages.StopSpinner();

                _consoleProcessMessages.ProcessComplete(processResults);
            });


            return command;
        }
    }
}
