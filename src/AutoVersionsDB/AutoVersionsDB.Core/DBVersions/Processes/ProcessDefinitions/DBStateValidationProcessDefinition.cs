using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.DBVersions.Processes.ProcessDefinitions
{
    public class DBStateValidationProcessDefinition : DBVersionsProcessDefinition
    {
        public override string EngineTypeName => "DB State Validation";


        public DBStateValidationProcessDefinition(CreateScriptFilesStateStep createScriptFilesStateStep,
                                                    ValidationsStep<ProjectCodeExistDBVersionsValidationsFactory> projectCodeExistValidationStep,
                                                    SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep,
                                                    ValidationsStep<DBStateValidationsFactory> dbStateValidationStep)
            : base(null, projectCodeExistValidationStep, setProjectConfigInProcessContextStep)
        {
            AddStep(createScriptFilesStateStep);
            AddStep(dbStateValidationStep);
        }
    }
}
