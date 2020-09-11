using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessDefinitions;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;

namespace AutoVersionsDB.Core.Processes.DBVersionsProcesses
{
    public class ProjectConfigProcessContext : ProcessContext, IProjectConfigable
    {
        public ProjectConfigItem ProjectConfig
        {
            get
            {
                return (ProcessParams as ProjectConfigProcessParams).ProjectConfig;
            }
        }


        public override bool CanRollback
        {
            get
            {
                return false;
            }
        }



        public ProjectConfigProcessContext()
        {
        }

    }
}
