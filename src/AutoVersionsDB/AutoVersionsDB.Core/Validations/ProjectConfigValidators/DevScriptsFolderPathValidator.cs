using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessDefinitions;
using System.IO;

namespace AutoVersionsDB.Core.Validations.ProjectConfigValidators
{
    public class DevScriptsBaseFolderPathValidator : ValidatorBase
    {
        private readonly ProjectConfigItem _projectConfigItem;

        public override string ValidatorName => "DevScriptsBaseFolder";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";


        public DevScriptsBaseFolderPathValidator(ProjectConfigItem projectConfig)
        {
            _projectConfigItem = projectConfig;
        }

        public override string Validate(AutoVersionsDbProcessParams executionParam)
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
