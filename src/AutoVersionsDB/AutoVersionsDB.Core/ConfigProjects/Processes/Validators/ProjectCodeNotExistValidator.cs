using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.Validators
{
    public class ProjectCodeNotExistValidator : ValidatorBase
    {
        private readonly ProjectConfigsStorage _projectConfigsStorage;
        private readonly string _projectCode;

        public override string ValidatorName => "ProjectCodeNotExist";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";


        public ProjectCodeNotExistValidator(string projectCode,
                                                ProjectConfigsStorage projectConfigsStorage)
        {
            _projectCode = projectCode;
            _projectConfigsStorage = projectConfigsStorage;
        }

        public override string Validate()
        {
            if (!string.IsNullOrWhiteSpace(_projectCode))
            {
                if (_projectConfigsStorage.IsProjectCodeExsit(_projectCode))
                {
                    string errorMsg = $"Project Code: '{_projectCode}' is aready exist.";
                    return errorMsg;
                }
            }


            return "";
        }
    }
}
