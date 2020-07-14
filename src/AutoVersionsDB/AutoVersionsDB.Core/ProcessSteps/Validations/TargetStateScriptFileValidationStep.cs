using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.Core.Validations.ExectutionParamsValidations;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public class TargetStateScriptFileValidationStep : ValidationsStep
    {
        private ScriptFilesComparersManager _scriptFilesComparersManager;

        protected override bool ShouldContinueWhenFindError => true;

        public TargetStateScriptFileValidationStep(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
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

            TargetStateScriptFileExistValidator targetStateScriptFileExistValidator = new TargetStateScriptFileExistValidator(scriptFilesComparersProvider);
            Validators.Add(targetStateScriptFileExistValidator);

            IsTargetScriptFiletAlreadyExecutedValidator isTargetScriptFiletAlreadyExecutedValidator = new IsTargetScriptFiletAlreadyExecutedValidator(scriptFilesComparersProvider);
            Validators.Add(isTargetScriptFiletAlreadyExecutedValidator);

        }

    }
}
