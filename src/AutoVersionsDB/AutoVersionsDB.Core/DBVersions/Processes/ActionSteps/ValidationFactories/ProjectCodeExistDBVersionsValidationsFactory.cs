using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.Common.Validators;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories
{
    public class ProjectCodeExistDBVersionsValidationsFactory : ValidationsFactory
    {
        private readonly ProjectConfigsStorage _projectConfigsStorage;

        public override string ValidationName => "ProjectCodeExist";


        public ProjectCodeExistDBVersionsValidationsFactory(ProjectConfigsStorage projectConfigsStorage)
        {
            _projectConfigsStorage = projectConfigsStorage;
        }


        public override ValidationsGroup Create(ProcessContext processContext)
        {
            ValidationsGroup validationsGroup = new ValidationsGroup(true);

            string projectCode = (processContext.ProcessParams as DBVersionsProcessParams).ProjectCode;

            ProjectCodeExistValidator projectNameValidator = new ProjectCodeExistValidator(projectCode, _projectConfigsStorage);
            validationsGroup.Add(projectNameValidator);

            return validationsGroup;
        }
    }
}
