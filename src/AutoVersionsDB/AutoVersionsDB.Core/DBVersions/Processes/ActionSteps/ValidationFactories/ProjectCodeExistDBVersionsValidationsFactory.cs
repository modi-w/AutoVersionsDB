using AutoVersionsDB.Common;
using AutoVersionsDB.Core.Common.Validators;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories
{
    public class ProjectCodeExistDBVersionsValidationsFactory : ValidationsFactory
    {
        private readonly ProjectConfigs _projectConfigs;

        public override string ValidationName => "ProjectCodeExist";


        public ProjectCodeExistDBVersionsValidationsFactory(ProjectConfigs projectConfigs)
        {
            _projectConfigs = projectConfigs;
        }


        public override ValidationsGroup Create(ProcessContext processContext)
        {
            ValidationsGroup validationsGroup = new ValidationsGroup(true);

            string projectCode = (processContext.ProcessParams as DBVersionsProcessParams).ProjectCode;

            ProjectCodeExistValidator projectNameValidator = new ProjectCodeExistValidator(projectCode, _projectConfigs);
            validationsGroup.Add(projectNameValidator);

            return validationsGroup;
        }
    }
}
