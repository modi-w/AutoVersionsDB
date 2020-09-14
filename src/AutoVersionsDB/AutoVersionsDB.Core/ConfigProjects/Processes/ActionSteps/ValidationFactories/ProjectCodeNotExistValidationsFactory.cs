using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ConfigProjects.Processes.Validators;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps.ValidationFactories
{
    public class ProjectCodeNotExistValidationsFactory : ValidationsFactory
    {
        private readonly ProjectConfigs _projectConfigs;

        public override string ValidationName => "ProjectCodeNotExist";


        public ProjectCodeNotExistValidationsFactory(ProjectConfigs projectConfigs)
        {
            _projectConfigs = projectConfigs;
        }


        public override ValidationsGroup Create(ProcessContext processContext)
        {
            ValidationsGroup validationsGroup = new ValidationsGroup(true);

            string projectCode = (processContext.ProcessParams as ProjectConfigProcessParams).ProjectCode;

            ProjectCodeNotExistValidator projectNameValidator = new ProjectCodeNotExistValidator(projectCode, _projectConfigs);
            validationsGroup.Add(projectNameValidator);

            return validationsGroup;
        }
    }
}
