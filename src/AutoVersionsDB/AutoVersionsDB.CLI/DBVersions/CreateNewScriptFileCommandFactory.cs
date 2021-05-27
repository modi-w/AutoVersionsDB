using AutoVersionsDB.Core.DBVersions;
using System.CommandLine;

namespace AutoVersionsDB.CLI.DBVersions
{
    public class CreateNewScriptFileCommandFactory : CLICommandFactory
    {
        private readonly CreateNewIncrementalScriptFileCommandFactory _createNewIncrementalScriptFileCommandFactory;
        private readonly CreateNewRepeatableScriptFileCommandFactory _createNewRepeatableScriptFileCommandFactory;
        private readonly CreateNewDevDummyDataScriptFileCommandFactory _createNewDevDummyDataScriptFileCommandFactory;


        public CreateNewScriptFileCommandFactory(CreateNewIncrementalScriptFileCommandFactory createNewIncrementalScriptFileCommandFactory,
                                                CreateNewRepeatableScriptFileCommandFactory createNewRepeatableScriptFileCommandFactory,
                                                CreateNewDevDummyDataScriptFileCommandFactory createNewDevDummyDataScriptFileCommandFactory)
        {
            _createNewIncrementalScriptFileCommandFactory = createNewIncrementalScriptFileCommandFactory;
            _createNewRepeatableScriptFileCommandFactory = createNewRepeatableScriptFileCommandFactory;
            _createNewDevDummyDataScriptFileCommandFactory = createNewDevDummyDataScriptFileCommandFactory;
        }

        public override Command Create()
        {
            Command command = new Command("new")
            {
                Description = CLITextResources.CreateNewScriptFileCommandDescription
            };

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
