using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.Common.Validators
{
    public class ProjectCodeExistValidator : ValidatorBase
    {
        private readonly ProjectConfigsStorage _projectConfigsStorage;
        private readonly string _projectCode;

        public override string ValidatorName => "ProjectCodeExist";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";


        public ProjectCodeExistValidator(string projectCode,
                                            ProjectConfigsStorage projectConfigsStorage)
        {
            _projectCode = projectCode;
            _projectConfigsStorage = projectConfigsStorage;
        }

        public override string Validate()
        {
            if (!string.IsNullOrWhiteSpace(_projectCode))
            {
                if (!_projectConfigsStorage.IsProjectCodeExsit(_projectCode))
                {
                    string errorMsg = $"Project Code: '{_projectCode}' is not exist.";
                    return errorMsg;
                }
            }

            return "";
        }
    }
}
