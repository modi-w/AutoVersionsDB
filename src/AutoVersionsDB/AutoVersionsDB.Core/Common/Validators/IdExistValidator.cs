using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.Common.Validators
{
    public class IdExistValidator : ValidatorBase
    {
        private readonly ProjectConfigsStorage _projectConfigsStorage;
        private readonly string _id;

        public override string ValidatorName => "IdExist";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";


        public IdExistValidator(string id,
                                ProjectConfigsStorage projectConfigsStorage)
        {
            _id = id;
            _projectConfigsStorage = projectConfigsStorage;
        }

        public override string Validate()
        {
            if (!string.IsNullOrWhiteSpace(_id))
            {
                if (!_projectConfigsStorage.IsIdExsit(_id))
                {
                    string errorMsg = $"Id: '{_id}' is not exist.";
                    return errorMsg;
                }
            }

            return "";
        }
    }
}
