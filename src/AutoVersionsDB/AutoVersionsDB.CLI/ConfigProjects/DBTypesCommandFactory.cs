using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.CLI.ConfigProjects
{
    public class DBTypesCommandFactory : CLICommandFactory
    {
        private readonly ProjectConfigsAPI _projectConfigsAPI;
        private readonly IConsoleProcessMessages _consoleProcessMessages;




        public DBTypesCommandFactory(ProjectConfigsAPI projectConfigsAPI,
                                        ConsoleProcessMessages consoleProcessMessages)
        {
            _projectConfigsAPI = projectConfigsAPI;
            _consoleProcessMessages = consoleProcessMessages;
        }

        public override Command Create()
        {
            Command command = new Command("dbtypes");

            command.Description = "Show list of the supported DataBase Types.";

            command.Handler = CommandHandler
                .Create(() =>
                {
                    _consoleProcessMessages.StartProcessMessage("dbtypes");

                    _consoleProcessMessages.SetInfoMessage("");

                    string captionsMessage = $"{"  Code".PadRight(12)} |  Name";
                    _consoleProcessMessages.SetInfoMessage(captionsMessage);

                    string captionsLineMessage = "-".PadRight(20, '-');
                    _consoleProcessMessages.SetInfoMessage(captionsLineMessage);


                    IEnumerable<DBType> dbTypesList = _projectConfigsAPI.GetDBTypes();

                    foreach (DBType dbType in dbTypesList)
                    {
                        string projectLineMessage = $"+ {dbType.Code.PadRight(10)} | {dbType.Name}";
                        _consoleProcessMessages.SetInfoMessage(projectLineMessage);
                    }


                });

            return command;
        }


    }
}
