using AutoVersionsDB.Core.Common.Validators;
using AutoVersionsDB.Core.ConfigProjects.Processes.ProcessDefinitions;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps.ValidationFactories
{
    public class IdExistValidationsFactory : ValidationsFactory
    {
        private readonly ProjectConfigsStorage _projectConfigsStorage;

        public override string ValidationName => "IdExist";


        public IdExistValidationsFactory(ProjectConfigsStorage projectConfigsStorage)
        {
            _projectConfigsStorage = projectConfigsStorage;
        }


        public override ValidationsGroup Create(ProcessContext processContext)
        {
            ValidationsGroup validationsGroup = new ValidationsGroup(true);

            string Id = null;
            if (processContext.ProcessParams is ProjectConfigProcessParams)
            {
                Id = (processContext.ProcessParams as ProjectConfigProcessParams).Id;
            }
            else if (processContext.ProcessParams is ChangeIdProcessParams)
            {
                Id = (processContext.ProcessParams as ChangeIdProcessParams).Id;
            }
            else
            {
                throw new Exception("Invalid 'ProcessParams' type");
            }

            IdMandatory idNotEmpty = new IdMandatory(Id);
            validationsGroup.Add(idNotEmpty);

            IdExistValidator projectNameValidator = new IdExistValidator(Id, _projectConfigsStorage);
            validationsGroup.Add(projectNameValidator);

            return validationsGroup;
        }
    }
}
