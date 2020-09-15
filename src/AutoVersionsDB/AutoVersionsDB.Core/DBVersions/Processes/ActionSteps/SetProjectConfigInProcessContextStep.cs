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
            string projectCode = (processContext.ProcessParams as DBVersionsProcessParams).ProjectCode;

            processContext.SetProjectConfig(_projectConfigsStorage.GetProjectConfigByProjectCode(projectCode));

        }
    }
}