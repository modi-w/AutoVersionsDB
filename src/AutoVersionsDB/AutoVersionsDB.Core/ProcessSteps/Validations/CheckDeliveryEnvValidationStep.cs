using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public class CheckDeliveryEnvValidationStep : ValidationsStep
    {
        protected override bool ShouldContinueWhenFindError => false;

        public CheckDeliveryEnvValidationStep(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                    SingleValidationStep singleValidationStep)
            : base(notificationExecutersFactoryManager, singleValidationStep)
        {
        }

        protected override void SetValidators(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            CheckDeliveryEnvValidator checkDeliveryEnvValidator = new CheckDeliveryEnvValidator(projectConfig.IsDevEnvironment);
            Validators.Add(checkDeliveryEnvValidator);

        }


    }
}
