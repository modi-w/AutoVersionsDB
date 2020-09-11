using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ConfigProjects
{
    public interface IProjectConfigable
    {
        ProjectConfigItem ProjectConfig { get; }
    }
}
