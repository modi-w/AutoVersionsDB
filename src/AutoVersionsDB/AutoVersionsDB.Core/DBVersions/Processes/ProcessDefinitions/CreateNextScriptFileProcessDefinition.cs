using AutoVersionsDB.Core.Common;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ExecuteScripts;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.DBVersions.Processes.ProcessDefinitions
{
    public class CreateNextScriptFileProcessDefinition<TScriptFileType> : DBVersionsProcessDefinition
      where TScriptFileType : ScriptFileTypeBase
    {
        public override string EngineTypeName => "Create Next Script File";


        public CreateNextScriptFileProcessDefinition(ValidationsStep<ProjectCodeExistDBVersionsValidationsFactory> projectCodeExistValidationStep,
                                                    SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep,
                                                    ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                                                    ValidationsStep<CheckDeliveryEnvValidationsFactory> checkDeliveryEnvValidationStep,
                                                    CreateScriptFilesStateStep createScriptFilesStateStep,
                                                    ValidationsStep<NextScriptFileNameValidationsFactory<TScriptFileType>> nextScriptFileNameValidations,
                                                    CreateNextScriptFileStep<TScriptFileType> createNextScriptFileStep)
            : base(null, projectCodeExistValidationStep, setProjectConfigInProcessContextStep)
        {
            AddStep(projectConfigValidationStep);
            AddStep(checkDeliveryEnvValidationStep);
            AddStep(createScriptFilesStateStep);
            AddStep(nextScriptFileNameValidations);
            AddStep(createNextScriptFileStep);
        }



    }
}
