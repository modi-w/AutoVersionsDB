using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using System.IO;

namespace AutoVersionsDB.Core.Validations.ProjectConfigValidators
{
    public class DevScriptsBaseFolderPathValidator : ValidatorBase
    {
        public override string ValidatorName => "DevScriptsBaseFolder";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";

        private ProjectConfigItem _projectConfigItem;

        public DevScriptsBaseFolderPathValidator(ProjectConfigItem projectConfig)
        {
            _projectConfigItem = projectConfig;
        }

        public override string Validate(AutoVersionsDBExecutionParams executionParam)
        {
            if (_projectConfigItem.IsDevEnvironment)
            {
                if (string.IsNullOrWhiteSpace(_projectConfigItem.DevScriptsBaseFolderPath))
                {
                    string errorMsg = "Scripts Folder Path is empty";
                    return errorMsg;
                }
                else
                {
                    if (!Directory.Exists(_projectConfigItem.DevScriptsBaseFolderPath))
                    {
                        string errorMsg = "Scripts Folder is not exist";
                        return errorMsg;
                    }
                }
            }

            return "";
        }
    }
}
