using AutoVersionsDB.Core.Common;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.NotificationableEngine.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.DBVersions.Processes.ProcessDefinitions
{
    public class DBVersionsValidationsProcessDefinitions : DBVersionsProcessDefinition
    {
        public override string EngineTypeName => "DB Versions Validations";

        public DBVersionsValidationsProcessDefinitions(ValidationsStep<ProjectCodeExistDBVersionsValidationsFactory> projectCodeExistValidationStep,
                                                        SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep,
                                                        ValidationsStep<ArtifactFileValidationsFactory> artifactFileValidationStep,
                                                        ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                                                        CreateScriptFilesStateStep createScriptFilesStateStep,
                                                        ValidationsStep<SystemTableValidationsFactory> systemTableValidationStep,
                                                        ValidationsStep<DBStateValidationsFactory> dbStateValidationStep)
         : base(null, projectCodeExistValidationStep, setProjectConfigInProcessContextStep)
        {
            AddStep(artifactFileValidationStep);
            AddStep(projectConfigValidationStep);
            AddStep(createScriptFilesStateStep);
            AddStep(systemTableValidationStep);
            AddStep(dbStateValidationStep);
        }
    }
}
