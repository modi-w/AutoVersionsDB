using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.Core.Validations.DBStateValidators;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public class DBStateValidationStep : ValidationsStep, IDisposable
    {
        private ScriptFilesComparersProvider _scriptFilesComparersProvider;

        protected override bool ShouldContinueWhenFindError => false;

        public DBStateValidationStep(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                            SingleValidationStep singleValidationStep,
                                            ScriptFilesComparersProvider scriptFilesComparersProvider)
            : base(notificationExecutersFactoryManager, singleValidationStep)
        {
            _scriptFilesComparersProvider = scriptFilesComparersProvider;
        }

        protected override void SetValidators(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            _scriptFilesComparersProvider.SetProjectConfig(projectConfig);

            IsHistoryExecutedFilesChangedValidator isHistoryExecutedFilesChangedValidator = new IsHistoryExecutedFilesChangedValidator(_scriptFilesComparersProvider);
            Validators.Add(isHistoryExecutedFilesChangedValidator);
        }


    }
}
