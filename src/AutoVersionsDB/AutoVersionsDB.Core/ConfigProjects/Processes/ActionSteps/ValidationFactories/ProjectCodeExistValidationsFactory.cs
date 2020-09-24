using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.Common.Validators;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps.ValidationFactories
{
    public class ProjectCodeExistValidationsFactory : ValidationsFactory
    {
        private readonly ProjectConfigsStorage _projectConfigsStorage;

        public override string ValidationName => "ProjectCodeExist";


        public ProjectCodeExistValidationsFactory(ProjectConfigsStorage projectConfigsStorage)
        {
            _projectConfigsStorage = projectConfigsStorage;
        }


        public override ValidationsGroup Create(ProcessContext processContext)
        {
            ValidationsGroup validationsGroup = new ValidationsGroup(true);

            string projectCode = (processContext.ProcessParams as ProjectConfigProcessParams).ProjectCode;

            ProjectCodeMandatory projectCodeNotEmpty = new ProjectCodeMandatory(projectCode);
            validationsGroup.Add(projectCodeNotEmpty);

            ProjectCodeExistValidator projectNameValidator = new ProjectCodeExistValidator(projectCode, _projectConfigsStorage);
            validationsGroup.Add(projectNameValidator);

            return validationsGroup;
        }
    }
}
