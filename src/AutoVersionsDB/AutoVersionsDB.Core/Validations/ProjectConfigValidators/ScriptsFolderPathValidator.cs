using AutoVersionsDB.Core.Engines;
using System.IO;

namespace AutoVersionsDB.Core.Validations.ProjectConfigValidators
{
    public class ScriptsFolderPathValidator : ValidatorBase
    {
        public override string ValidatorName => "ScriptsFolderPath";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";

        private string _scriptFolderPath;


        public ScriptsFolderPathValidator(string scriptFolderPath)
        {
            _scriptFolderPath = scriptFolderPath;
        }

        public override string Validate(AutoVersionsDBExecutionParams executionParam)
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
