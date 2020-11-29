using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Common
{
    public abstract class CommonProcessContext : ProcessContext
    {
        public abstract ProjectConfigItem ProjectConfig { get; }

    }
}
