using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.DB.Contract;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;

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
            Command command = new Command("dbtypes")
            {
                Description = CLITextResources.DBTypesCommandDescription,

                Handler = CommandHandler
                .Create(() =>
                {
                    _consoleProcessMessages.StartProcessMessage("dbtypes");

                    _consoleProcessMessages.SetInfoMessage("");

                    string captionsMessage = $"{"  Code",-12} |  Name";
                    _consoleProcessMessages.SetInfoMessage(captionsMessage);

                    string captionsLineMessage = "-".PadRight(20, '-');
                    _consoleProcessMessages.SetInfoMessage(captionsLineMessage);


                    IEnumerable<DBType> dbTypesList = _projectConfigsAPI.DBTypes;

                    foreach (DBType dbType in dbTypesList)
                    {
                        string projectLineMessage = $"+ {dbType.Code,-10} | {dbType.Name}";
                        _consoleProcessMessages.SetInfoMessage(projectLineMessage);
                    }


                })
            };

            return command;
        }


    }
}
