using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessDefinitions;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public class CheckDeliveryEnvValidationsFactory : ValidationsFactory
    {
        public override string ValidationName => "Check Delivery Environment";

        public override ValidationsGroup Create(ProjectConfigItem projectConfig, AutoVersionsDbProcessContext processContext)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            ValidationsGroup validationsGroup = new ValidationsGroup(false);

            CheckDeliveryEnvValidator checkDeliveryEnvValidator = new CheckDeliveryEnvValidator(projectConfig.IsDevEnvironment);
            validationsGroup.Add(checkDeliveryEnvValidator);

            return validationsGroup;
        }


    }
}
