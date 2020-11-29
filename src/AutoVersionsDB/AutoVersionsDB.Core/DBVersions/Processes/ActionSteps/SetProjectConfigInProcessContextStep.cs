using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.ConfigProjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps
{
    public class SetProjectConfigInProcessContextStep : DBVersionsStep
    {
        public override string StepName => "Set Project Config In Process Context";

        private readonly ProjectConfigsStorage _projectConfigsStorage;

        public SetProjectConfigInProcessContextStep(ProjectConfigsStorage projectConfigsStorage)
        {
            projectConfigsStorage.ThrowIfNull(nameof(projectConfigsStorage));

            _projectConfigsStorage = projectConfigsStorage;
        }



        public override void Execute(DBVersionsProcessContext processContext)
        {
            string id = (processContext.ProcessParams as DBVersionsProcessParams).Id;

            processContext.SetProjectConfig(_projectConfigsStorage.GetProjectConfigById(id));

        }
    }
}