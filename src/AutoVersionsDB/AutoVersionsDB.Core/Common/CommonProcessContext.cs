using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.Common
{
    public abstract class CommonProcessContext : ProcessContext
    {
        public abstract ProjectConfigItem ProjectConfig { get; }

    }
}
