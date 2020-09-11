using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.Core.Validations.ProjectConfigValidators;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public class ProjectCodeNotExistValidationsFactory : ValidationsFactory
    {
        private readonly ProjectConfigs _projectConfigs;

        internal override string ValidationName => "ProjectCodeNotExist";


        public ProjectCodeNotExistValidationsFactory(ProjectConfigs projectConfigs)
        {
            _projectConfigs = projectConfigs;
        }


        internal override ValidationsGroup Create(ProcessContext processContext)
        {
            ProjectConfigItem projectConfig = (processContext as IProjectConfigable).ProjectConfig;


            ValidationsGroup validationsGroup = new ValidationsGroup(true);

            string projectCode = (processContext.ProcessParams as IProjectCode).ProjectCode;

            ProjectCodeNotExistValidator projectNameValidator = new ProjectCodeNotExistValidator(projectCode, _projectConfigs);
            validationsGroup.Add(projectNameValidator);

            return validationsGroup;
        }
    }
}
