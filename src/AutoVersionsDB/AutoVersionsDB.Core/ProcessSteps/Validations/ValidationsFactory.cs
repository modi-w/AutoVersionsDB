using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
  public abstract  class ValidationsFactory
    {

        public abstract ValidationsGroup Create(ProjectConfigItem projectConfig, AutoVersionsDbProcessState processState);
    }
}
