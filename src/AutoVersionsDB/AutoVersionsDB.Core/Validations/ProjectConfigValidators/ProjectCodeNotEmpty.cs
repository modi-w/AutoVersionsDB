using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.Validations.ProjectConfigValidators
{
    public class ProjectCodeNotEmpty : ValidatorBase
    {
        private readonly string _projectCode;

        internal override string ValidatorName => "ProjectCodeNotEmpty";

        internal override string ErrorInstructionsMessage => "Project Config Validation Error";


        internal ProjectCodeNotEmpty(string projectCode)
        {
            _projectCode = projectCode;
        }

        internal override string Validate()
        {
            if (string.IsNullOrWhiteSpace(_projectCode))
            {
                string errorMsg = "Project Code is empty";
                return errorMsg;
            }

            return "";
        }
    }
}
