using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public class SystemTableValidationsFactory : ValidationsFactory
    {
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;

        public override string ValidationName => "System Tables";

        public SystemTableValidationsFactory(DBCommandsFactoryProvider dbCommandsFactoryProvider)
        {
            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
        }


        public override ValidationsGroup Create(ProcessContext processContext)
        {
            ProjectConfigItem projectConfig = (processContext as IProjectConfigable).ProjectConfig;


            ValidationsGroup validationsGroup = new ValidationsGroup(false);

            SystemTablesValidator systemTablesValidator = new SystemTablesValidator(_dbCommandsFactoryProvider, projectConfig.ConnStr, projectConfig.DBTypeCode, projectConfig.IsDevEnvironment);
            validationsGroup.Add(systemTablesValidator);

            return validationsGroup;
        }


    }
}
