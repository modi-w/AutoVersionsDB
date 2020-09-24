using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.Common.Validators
{
    public class ProjectCodeMandatory : ValidatorBase
    {
        private readonly string _projectCode;

        public override string ValidatorName => "ProjectCodeMandatory";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";


        public ProjectCodeMandatory(string projectCode)
        {
            _projectCode = projectCode;
        }

        public override string Validate()
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
