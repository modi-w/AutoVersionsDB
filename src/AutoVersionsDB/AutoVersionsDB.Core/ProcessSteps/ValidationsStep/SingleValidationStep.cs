using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.ProcessSteps.ValidationsStep
{
    public class SingleValidationStep : NotificationableActionStepBase<AutoVersionsDbProcessState, ValidatorStepArgs>
    {
        public override string StepName => "Validate";



        public SingleValidationStep()
        {
        }


        public override int GetNumOfInternalSteps(AutoVersionsDbProcessState processState, ValidatorStepArgs actionStepArgs)
        {
            return 1;
        }

        public override void Execute(AutoVersionsDbProcessState processState, ValidatorStepArgs actionStepArgs)
        {
            string errorMsg = actionStepArgs.Validator.Validate(processState.ExecutionParams as AutoVersionsDBExecutionParams);

            if (!string.IsNullOrWhiteSpace(errorMsg))
            {
                throw new NotificationEngineException(actionStepArgs.Validator.ValidatorName,errorMsg, actionStepArgs.Validator.ErrorInstructionsMessage);
            }
        }



    }
}
