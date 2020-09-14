using AutoVersionsDB.Common;
using AutoVersionsDB.Core.Common;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;

namespace AutoVersionsDB.Core.ConfigProjects.Processes
{
    public class ProjectConfigProcessContext : CommonProcessContext
    {
        public override ProjectConfigItem ProjectConfig
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
