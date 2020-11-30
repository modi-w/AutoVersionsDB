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
        private readonly DBCommandsFactory dbCommandsFactoryProvider;

        public override string ValidationName => "System Tables";

        public SystemTableValidationsFactory(DBCommandsFactory dbCommandsFactory)
        {
            dbCommandsFactoryProvider = dbCommandsFactory;
        }


        public override ValidationsGroup Create(ProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            ProjectConfigItem projectConfig = (processContext as CommonProcessContext).ProjectConfig;


            ValidationsGroup validationsGroup = new ValidationsGroup(false);

            SystemTablesValidator systemTablesValidator = new SystemTablesValidator(dbCommandsFactoryProvider, projectConfig.DevEnvironment, projectConfig.DBConnectionInfo);
            validationsGroup.Add(systemTablesValidator);

            return validationsGroup;
        }


    }
}
