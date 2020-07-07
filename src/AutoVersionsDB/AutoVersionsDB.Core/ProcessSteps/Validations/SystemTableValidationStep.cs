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
    public class SystemTableValidationStep : ValidationsStep, IDisposable
    {
        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;
       
        private IDBCommands _dbCommands;

        protected override bool ShouldContinueWhenFindError => false;

        public SystemTableValidationStep(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                            SingleValidationStep singleValidationStep,
                                            DBCommandsFactoryProvider dbCommandsFactoryProvider)
         : base(notificationExecutersFactoryManager, singleValidationStep)
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

        ~SystemTableValidationStep() => Dispose(false);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
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
        }

        #endregion


    }
}
