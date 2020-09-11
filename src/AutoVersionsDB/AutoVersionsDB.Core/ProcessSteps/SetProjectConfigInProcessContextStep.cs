using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessSteps
{
    public class SetProjectConfigInProcessContextStep : DBVersionsStep
    {
        public override string StepName => "Set Project Config In Process Context";

        private readonly ProjectConfigs _projectConfigs;

        public SetProjectConfigInProcessContextStep(ProjectConfigs projectConfigs)
        {
            projectConfigs.ThrowIfNull(nameof(projectConfigs));

            _projectConfigs = projectConfigs;
        }



        public override void Execute(DBVersionsProcessContext processContext)
        {
            string projectCode = (processContext.ProcessParams as DBVersionsProcessParams).ProjectCode;

            processContext.ProjectConfig = _projectConfigs.GetProjectConfigByProjectCode(projectCode);

        }
    }
}