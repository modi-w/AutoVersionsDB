using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.Validators
{
    public class IdNotExistValidator : ValidatorBase
    {
        private readonly ProjectConfigsStorage _projectConfigsStorage;
        private readonly string _id;

        public const string Name = "ValidateProjectIdNotExist";
        public override string ValidatorName => Name;


        public override string ErrorInstructionsMessage => CoreTextResources.ProjectIdIsAlreadyExistErrorMessage;

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
                    return CoreTextResources.ProjectIdIsAlreadyExistWithIdErrorMessage.Replace("[Id]", _id);
                }
            }


            return "";
        }
    }
}
