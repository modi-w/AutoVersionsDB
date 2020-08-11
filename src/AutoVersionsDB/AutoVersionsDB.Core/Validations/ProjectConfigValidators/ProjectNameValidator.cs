using AutoVersionsDB.Core.Engines;

namespace AutoVersionsDB.Core.Validations.ProjectConfigValidators
{
    public class ProjectNameValidator : ValidatorBase
    {
        private readonly string _projectName;

        public override string ValidatorName => "ProjectName";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";


        public ProjectNameValidator(string projectName)
        {
            _projectName = projectName;
        }

        public override string Validate(AutoVersionsDBExecutionParams executionParam)
        {
            if (string.IsNullOrWhiteSpace(_projectName))
            {
                string errorMsg = "Project Name is empty";
                return errorMsg;
            }

            return "";
        }
    }
}
