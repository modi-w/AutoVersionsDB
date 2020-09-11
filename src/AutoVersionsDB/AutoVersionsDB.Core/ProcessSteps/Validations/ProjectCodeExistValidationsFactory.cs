using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.Core.Validations.ProjectConfigValidators;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public class ProjectCodeExistValidationsFactory : ValidationsFactory
    {
        private readonly ProjectConfigs _projectConfigs;

        internal override string ValidationName => "ProjectCodeExist";


        public ProjectCodeExistValidationsFactory(ProjectConfigs projectConfigs)
        {
            _projectConfigs = projectConfigs;
        }


        internal override ValidationsGroup Create(ProcessContext processContext)
        {
            ValidationsGroup validationsGroup = new ValidationsGroup(true);

            string projectCode = (processContext.ProcessParams as IProjectCode).ProjectCode;

            ProjectCodeExistValidator projectNameValidator = new ProjectCodeExistValidator( projectCode, _projectConfigs);
            validationsGroup.Add(projectNameValidator);

            return validationsGroup;
        }
    }
}
