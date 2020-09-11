using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.Validations.ProjectConfigValidators
{
    public class ProjectCodeNotExistValidator : ValidatorBase
    {
        private readonly ProjectConfigs _projectConfigs;
        private readonly string _projectCode;

        internal override string ValidatorName => "ProjectCodeNotExist";

        internal override string ErrorInstructionsMessage => "Project Config Validation Error";


        internal ProjectCodeNotExistValidator(string projectCode,
                                                ProjectConfigs projectConfigs)
        {
            _projectCode = projectCode;
            _projectConfigs = projectConfigs;
        }

        internal override string Validate()
        {
            if (!string.IsNullOrWhiteSpace(_projectCode))
            {
                if (_projectConfigs.IsProjectCodeExsit(_projectCode))
                {
                    string errorMsg = $"Project Code: '{_projectCode}' is aready exist.";
                    return errorMsg;
                }
            }


            return "";
        }
    }
}
