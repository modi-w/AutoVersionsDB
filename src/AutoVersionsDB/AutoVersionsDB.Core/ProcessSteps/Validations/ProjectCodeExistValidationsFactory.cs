using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.Core.Validations.ProjectConfigValidators;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public class ProjectCodeExistValidationsFactory : ValidationsFactory
    {
        private readonly ProjectConfigs _projectConfigs;

        public override string ValidationName => "ProjectCodeExist";


        public ProjectCodeExistValidationsFactory(ProjectConfigs projectConfigs)
        {
            _projectConfigs = projectConfigs;
        }


        public override ValidationsGroup Create(ProcessContext processContext)
        {
            ValidationsGroup validationsGroup = new ValidationsGroup(true);

            string projectCode = (processContext.ProcessParams as IProjectCode).ProjectCode;

            ProjectCodeExistValidator projectNameValidator = new ProjectCodeExistValidator( projectCode, _projectConfigs);
            validationsGroup.Add(projectNameValidator);

            return validationsGroup;
        }
    }
}
