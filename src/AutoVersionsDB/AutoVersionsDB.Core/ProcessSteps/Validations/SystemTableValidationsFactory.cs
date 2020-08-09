using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public class SystemTableValidationsFactory : ValidationsFactory
    {
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;


        public SystemTableValidationsFactory(DBCommandsFactoryProvider dbCommandsFactoryProvider)
        {
            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
        }


        public override List<ValidatorBase> Create(ProjectConfig projectConfig, AutoVersionsDbProcessState processState)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            List<ValidatorBase> validators = new List<ValidatorBase>();

            SystemTablesValidator systemTablesValidator = new SystemTablesValidator(_dbCommandsFactoryProvider, projectConfig.ConnStr, projectConfig.DBTypeCode, projectConfig.IsDevEnvironment);
            validators.Add(systemTablesValidator);

            return validators;
        }


    }
}
