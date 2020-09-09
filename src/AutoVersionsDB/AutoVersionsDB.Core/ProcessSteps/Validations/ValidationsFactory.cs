using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessDefinitions;
using AutoVersionsDB.Core.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public abstract  class ValidationsFactory
    {
        public abstract string ValidationName { get; }

        public abstract ValidationsGroup Create(ProjectConfigItem projectConfig, AutoVersionsDbProcessContext processContext);
    }
}
