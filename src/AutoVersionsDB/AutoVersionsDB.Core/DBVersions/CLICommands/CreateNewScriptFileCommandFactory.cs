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
    public class CreateNewScriptFileCommandFactory : CLICommandFactory
    {
        private readonly DBVersionsAPI _dbVersionsAPI;
        private readonly IConsoleProcessMessages _consoleProcessMessages;

        private readonly CreateNewIncrementalScriptFileCommandFactory _createNewIncrementalScriptFileCommandFactory;
        private readonly CreateNewRepeatableScriptFileCommandFactory _createNewRepeatableScriptFileCommandFactory;
        private readonly CreateNewDevDummyDataScriptFileCommandFactory _createNewDevDummyDataScriptFileCommandFactory;


        public CreateNewScriptFileCommandFactory(DBVersionsAPI dbVersionsAPI,
                                                IConsoleProcessMessages consoleProcessMessages,
                                                CreateNewIncrementalScriptFileCommandFactory createNewIncrementalScriptFileCommandFactory,
                                                CreateNewRepeatableScriptFileCommandFactory createNewRepeatableScriptFileCommandFactory,
                                                CreateNewDevDummyDataScriptFileCommandFactory createNewDevDummyDataScriptFileCommandFactory)
        {
            _dbVersionsAPI = dbVersionsAPI;
            _consoleProcessMessages = consoleProcessMessages;

            _createNewIncrementalScriptFileCommandFactory = createNewIncrementalScriptFileCommandFactory;
            _createNewRepeatableScriptFileCommandFactory = createNewRepeatableScriptFileCommandFactory;
            _createNewDevDummyDataScriptFileCommandFactory = createNewDevDummyDataScriptFileCommandFactory;
        }

        public override Command Create()
        {
            Command command = new Command("new");

            command.Description = "Create new script file. The type of script file is required.";

            Command incrementalScriptFileCommand = _createNewIncrementalScriptFileCommandFactory.Create();
            command.Add(incrementalScriptFileCommand);

            Command repeatableScriptFileCommand = _createNewRepeatableScriptFileCommandFactory.Create();
            command.Add(repeatableScriptFileCommand);

            Command devDummyDataScriptFileCommand = _createNewDevDummyDataScriptFileCommandFactory.Create();
            command.Add(devDummyDataScriptFileCommand);

            return command;
        }
    }
}
