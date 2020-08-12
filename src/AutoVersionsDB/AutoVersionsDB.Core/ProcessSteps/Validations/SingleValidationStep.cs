using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.Core.Validations;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.ProcessSteps.Validations
{
    public class SingleValidationStep : AutoVersionsDbStep
    {
        private readonly ValidatorBase _validator;


        public override string StepName => _validator.ValidatorName;
        public override bool HasInternalStep => false;



        public SingleValidationStep(ValidatorBase validator)
        {
            _validator = validator;

        }



        public override int GetNumOfInternalSteps(ProjectConfigItem projectConfig, AutoVersionsDbProcessState processState)
        {
            return 1;
        }

        public override void Execute(ProjectConfigItem projectConfig, NotificationExecutersProvider notificationExecutersProvider, AutoVersionsDbProcessState processState)
        {
            processState.ThrowIfNull(nameof(processState));

            string errorMsg = _validator.Validate(processState.ExecutionParams as AutoVersionsDBExecutionParams);

            if (!string.IsNullOrWhiteSpace(errorMsg))
            {
                throw new NotificationEngineException(_validator.ValidatorName, errorMsg, _validator.ErrorInstructionsMessage);
            }
        }



    }
}
