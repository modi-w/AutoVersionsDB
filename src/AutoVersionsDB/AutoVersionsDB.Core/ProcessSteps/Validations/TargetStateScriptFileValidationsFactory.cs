﻿using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessDefinitions;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.Core.Validations.ExectutionParamsValidations;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public class TargetStateScriptFileValidationsFactory : ValidationsFactory
    {
        public override string ValidationName => "Target State Script File";


        public override ValidationsGroup Create(ProjectConfigItem projectConfig, AutoVersionsDbProcessContext processContext)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));
            processContext.ThrowIfNull(nameof(processContext));

            ValidationsGroup validationsGroup = new ValidationsGroup(true);

            TargetStateScriptFileExistValidator targetStateScriptFileExistValidator = new TargetStateScriptFileExistValidator(processContext.ScriptFilesState);
            validationsGroup.Add(targetStateScriptFileExistValidator);

            IsTargetScriptFiletAlreadyExecutedValidator isTargetScriptFiletAlreadyExecutedValidator = new IsTargetScriptFiletAlreadyExecutedValidator(processContext.ScriptFilesState);
            validationsGroup.Add(isTargetScriptFiletAlreadyExecutedValidator);

            return validationsGroup;
        }

    }
}
