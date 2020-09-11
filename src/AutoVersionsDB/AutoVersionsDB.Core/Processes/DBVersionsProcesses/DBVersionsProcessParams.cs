﻿using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.Processes.DBVersionsProcesses
{
    public class DBVersionsProcessParams : ProcessParams, IProjectCode
    {
        public string ProjectCode { get; set; }
        public string TargetStateScriptFileName { get; set; }

        public DBVersionsProcessParams(string projectCode, string targetStateScriptFileName)
        {
            ProjectCode = projectCode;
            TargetStateScriptFileName = targetStateScriptFileName;
        }
    }
}