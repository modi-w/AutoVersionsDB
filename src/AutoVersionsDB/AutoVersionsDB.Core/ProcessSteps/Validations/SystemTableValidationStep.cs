using AutoVersionsDB.Core.ConfigProjects;
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
    public class SystemTableValidationStep : ValidationsStep
    {
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;

        private IDBCommands _dbCommands;

        protected override bool ShouldContinueWhenFindError => false;

        public SystemTableValidationStep(SingleValidationStep singleValidationStep,
                                            DBCommandsFactoryProvider dbCommandsFactoryProvider)
         : base(singleValidationStep)
        {
            _dbCommandsFactoryProvider = dbCommandsFactoryProvider ?? throw new ArgumentNullException(nameof(dbCommandsFactoryProvider));
        }


        protected override void SetValidators(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            _dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout);

            SystemTablesValidator systemTablesValidator = new SystemTablesValidator(_dbCommands, projectConfig.IsDevEnvironment);
            Validators.Add(systemTablesValidator);
        }


        #region IDisposable

        private bool _disposed = false;

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                if (_dbCommands != null)
                {
                    _dbCommands.Dispose();
                }

            }

            _disposed = true;

            base.Dispose(disposing);
        }

        #endregion


    }
}
