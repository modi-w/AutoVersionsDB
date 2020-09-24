﻿using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.DBVersions.Processes.ProcessDefinitions
{
    public class TargetStateScriptFileValidationProcessDefinition : DBVersionsProcessDefinition
    {
        public override string EngineTypeName => "Target State Script File Validation";


        public TargetStateScriptFileValidationProcessDefinition(ValidationsStep<ProjectCodeExistDBVersionsValidationsFactory> projectCodeExistValidationStep,
                                                                SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep,
                                                                CreateScriptFilesStateStep createScriptFilesStateStep,
                                                                ValidationsStep<TargetStateScriptFileValidationsFactory> targetStateScriptFileValidationStep)
            : base(null, projectCodeExistValidationStep, setProjectConfigInProcessContextStep)
        {
            AddStep(createScriptFilesStateStep);
            AddStep(targetStateScriptFileValidationStep);
        }
    }
}
