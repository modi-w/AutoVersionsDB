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
        private ScriptFilesComparersProvider _scriptFilesComparersProvider;

        protected override bool ShouldContinueWhenFindError => true;

        public TargetStateScriptFileValidationStep(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
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

            TargetStateScriptFileExistValidator targetStateScriptFileExistValidator = new TargetStateScriptFileExistValidator(_scriptFilesComparersProvider);
            Validators.Add(targetStateScriptFileExistValidator);
            
            IsTargetScriptFiletAlreadyExecutedValidator isTargetScriptFiletAlreadyExecutedValidator = new IsTargetScriptFiletAlreadyExecutedValidator(_scriptFilesComparersProvider);
            Validators.Add(isTargetScriptFiletAlreadyExecutedValidator);

        }

    }
}
