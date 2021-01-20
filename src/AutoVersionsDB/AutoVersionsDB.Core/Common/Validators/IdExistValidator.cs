using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.Common.Validators
{
    public class IdExistValidator : ValidatorBase
    {
        private readonly ProjectConfigsStorage _projectConfigsStorage;
        private readonly string _id;

        public const string Name = "IdExist";
        public override string ValidatorName => Name;

        public override string ErrorInstructionsMessage => CoreTextResources.ProjectConfigValidation;

        public override NotificationErrorType NotificationErrorType => NotificationErrorType.Error;

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
                    return CoreTextResources.ProjectIdIsNotExistErrorMessage.Replace("[Id]", _id);
                }
            }

            return "";
        }
    }
}
