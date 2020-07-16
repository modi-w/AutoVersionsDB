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
    public class DBStateValidationStep : ValidationsStep
    {
        private readonly ScriptFilesComparersManager _scriptFilesComparersManager;

        protected override bool ShouldContinueWhenFindError => false;

        public DBStateValidationStep(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                            SingleValidationStep singleValidationStep,
                                            ScriptFilesComparersManager scriptFilesComparersManager)
            : base(notificationExecutersFactoryManager, singleValidationStep)
        {
            _scriptFilesComparersManager = scriptFilesComparersManager;
        }

        protected override void SetValidators(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            ScriptFilesComparersProvider scriptFilesComparersProvider = _scriptFilesComparersManager.GetScriptFilesComparersProvider(projectConfig.ProjectGuid);

            IsHistoryExecutedFilesChangedValidator isHistoryExecutedFilesChangedValidator = new IsHistoryExecutedFilesChangedValidator(scriptFilesComparersProvider);
            Validators.Add(isHistoryExecutedFilesChangedValidator);
        }


    }
}
