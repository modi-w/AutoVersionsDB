using AutoVersionsDB.Core.ProcessDefinitions;
using System.IO;

namespace AutoVersionsDB.Core.Validations.ProjectConfigValidators
{
    public class ScriptsFolderPathValidator : ValidatorBase
    {
        private readonly string _scriptFolderPath;

        public override string ValidatorName => "ScriptsFolderPath";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";



        public ScriptsFolderPathValidator(string scriptFolderPath)
        {
            _scriptFolderPath = scriptFolderPath;
        }

        public override string Validate(AutoVersionsDbProcessParams executionParam)
        {
            if (!Directory.Exists(_scriptFolderPath))
            {
                string errorMsg = $"'{_scriptFolderPath}' Scripts Folder is not exist";
                return errorMsg;
            }

            return "";
        }
    }
}
