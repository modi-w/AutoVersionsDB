using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessDefinitions;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.DbCommands.Integration;

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


        public override ValidationsGroup Create(ProjectConfigItem projectConfig, AutoVersionsDbProcessContext processContext)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            ValidationsGroup validationsGroup = new ValidationsGroup(false);

            SystemTablesValidator systemTablesValidator = new SystemTablesValidator(_dbCommandsFactoryProvider, projectConfig.ConnStr, projectConfig.DBTypeCode, projectConfig.IsDevEnvironment);
            validationsGroup.Add(systemTablesValidator);

            return validationsGroup;
        }


    }
}
