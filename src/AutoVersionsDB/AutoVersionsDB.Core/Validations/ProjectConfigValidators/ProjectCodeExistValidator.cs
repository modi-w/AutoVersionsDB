﻿using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.Validations.ProjectConfigValidators
{
    public class ProjectCodeExistValidator : ValidatorBase
    {
        private readonly ProjectConfigs _projectConfigs;
        private readonly string _projectCode;

        public override string ValidatorName => "ProjectCodeExist";

        public override string ErrorInstructionsMessage => "Project Config Validation Error";


        public ProjectCodeExistValidator(string projectCode,
                                            ProjectConfigs projectConfigs)
        {
            _projectCode = projectCode;
            _projectConfigs = projectConfigs;
        }

        public override string Validate()
        {
            if (!string.IsNullOrWhiteSpace(_projectCode))
            {
                if (!_projectConfigs.IsProjectCodeExsit(_projectCode))
                {
                    string errorMsg = $"Project Code: '{_projectCode}' is not exist.";
                    return errorMsg;
                }
            }

            return "";
        }
    }
}