using AutoVersionsDB.Core.Common.Validators;
using AutoVersionsDB.Core.ConfigProjects.Processes.Validators;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps.ValidationFactories
{
    public class IdNotExistValidationsFactory : ValidationsFactory
    {
        private readonly ProjectConfigsStorage _projectConfigsStorage;

        public override string ValidationName => "IdNotExist";


        public IdNotExistValidationsFactory(ProjectConfigsStorage projectConfigsStorage)
        {
            _projectConfigsStorage = projectConfigsStorage;
        }


        public override ValidationsGroup Create(ProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            ValidationsGroup validationsGroup = new ValidationsGroup(true);

            string id = (processContext.ProcessParams as ProjectConfigProcessParams).Id;

            IdMandatory idNotEmpty = new IdMandatory(id);
            validationsGroup.Add(idNotEmpty);

            IdNotExistValidator projectNameValidator = new IdNotExistValidator(id, _projectConfigsStorage);
            validationsGroup.Add(projectNameValidator);

            return validationsGroup;
        }
    }
}
