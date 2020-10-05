using AutoVersionsDB.Core.Common;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.NotificationableEngine.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.DBVersions.Processes.ProcessDefinitions
{
    public class GetScriptFilesStateProcessDefinitions : DBVersionsProcessDefinition
    {
        public override string EngineTypeName => "Get Script Files State";

        public GetScriptFilesStateProcessDefinitions(ValidationsStep<IdExistDBVersionsValidationsFactory> idExistValidationStep,
                                                        SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep,
                                                        ValidationsStep<ArtifactFileValidationsFactory> artifactFileValidationStep,
                                                        ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                                                        CreateScriptFilesStateStep createScriptFilesStateStep,
                                                        SetScriptFilesStateAsResultsStep setScriptFilesStateAsResultsStep)
         : base(null, idExistValidationStep, setProjectConfigInProcessContextStep)
        {
            AddStep(artifactFileValidationStep);
            AddStep(projectConfigValidationStep);
            AddStep(createScriptFilesStateStep);
            AddStep(setScriptFilesStateAsResultsStep);
        }
    }
}
