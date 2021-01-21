using AutoVersionsDB.Core.Common;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.Processes.Validators;
using AutoVersionsDB.DB;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories
{
    public class SystemTableValidationsFactory : ValidationsFactory
    {
        private readonly DBCommandsFactory _dbCommandsFactory;

        public const string Name = "System Tables";
        public override string ValidationName => Name;


        public SystemTableValidationsFactory(DBCommandsFactory dbCommandsFactory)
        {
            _dbCommandsFactory = dbCommandsFactory;
        }


        public override ValidationsGroup Create(ProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            ProjectConfigItem projectConfig = (processContext as CommonProcessContext).ProjectConfig;

            DBVersionsProcessContext dbVersionsProcessContext = processContext as DBVersionsProcessContext;


            ValidationsGroup validationsGroup = new ValidationsGroup(false);

            NewProjectValidator newProjectValidator = 
                new NewProjectValidator(_dbCommandsFactory,
                                        projectConfig.DevEnvironment,
                                        projectConfig.DBConnectionInfo,
                                        dbVersionsProcessContext.ScriptFilesState);
            validationsGroup.Add(newProjectValidator);

            SystemTablesValidator systemTablesValidator = new SystemTablesValidator(_dbCommandsFactory, projectConfig.DevEnvironment, projectConfig.DBConnectionInfo);
            validationsGroup.Add(systemTablesValidator);

            return validationsGroup;
        }


    }
}
