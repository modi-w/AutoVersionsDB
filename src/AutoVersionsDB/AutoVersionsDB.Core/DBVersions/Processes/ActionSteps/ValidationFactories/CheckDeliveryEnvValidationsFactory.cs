using AutoVersionsDB.Core.Common;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.Processes.Validators;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories
{
    public class CheckDeliveryEnvValidationsFactory : ValidationsFactory
    {
        public const string Name = "Check Delivery Environment";
        public override string ValidationName => Name;


        public override ValidationsGroup Create(ProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            ProjectConfigItem projectConfig = (processContext as CommonProcessContext).ProjectConfig;


            ValidationsGroup validationsGroup = new ValidationsGroup(false);

            CheckDeliveryEnvValidator checkDeliveryEnvValidator = new CheckDeliveryEnvValidator(projectConfig.DevEnvironment);
            validationsGroup.Add(checkDeliveryEnvValidator);

            return validationsGroup;
        }


    }
}
