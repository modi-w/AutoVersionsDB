using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.Validators
{
    public class IdNotExistValidator : ValidatorBase
    {
        private readonly ProjectConfigsStorage _projectConfigsStorage;
        private readonly string _id;

        public override string ValidatorName => "IdNotExist";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";

        public override NotificationErrorType NotificationErrorType => NotificationErrorType.Error;

        public IdNotExistValidator(string id,
                                                ProjectConfigsStorage projectConfigsStorage)
        {
            _id = id;
            _projectConfigsStorage = projectConfigsStorage;
        }

        public override string Validate()
        {
            if (!string.IsNullOrWhiteSpace(_id))
            {
                if (_projectConfigsStorage.IsIdExsit(_id))
                {
                    string errorMsg = $"Id: '{_id}' is aready exist.";
                    return errorMsg;
                }
            }


            return "";
        }
    }
}
