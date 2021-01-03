using AutoVersionsDB.Core.Common.Validators;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories
{
    public class IdExistDBVersionsValidationsFactory : ValidationsFactory
    {
        private readonly ProjectConfigsStorage _projectConfigsStorage;

        public override string ValidationName => "IdExist";


        public IdExistDBVersionsValidationsFactory(ProjectConfigsStorage projectConfigsStorage)
        {
            _projectConfigsStorage = projectConfigsStorage;
        }


        public override ValidationsGroup Create(ProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            ValidationsGroup validationsGroup = new ValidationsGroup(true);

            string id = (processContext.ProcessArgs as DBVersionsProcessArgs).Id;

            IdMandatory idNotEmpty = new IdMandatory(id);
            validationsGroup.Add(idNotEmpty);

            IdExistValidator projectNameValidator = new IdExistValidator(id, _projectConfigsStorage);
            validationsGroup.Add(projectNameValidator);

            return validationsGroup;
        }
    }
}
