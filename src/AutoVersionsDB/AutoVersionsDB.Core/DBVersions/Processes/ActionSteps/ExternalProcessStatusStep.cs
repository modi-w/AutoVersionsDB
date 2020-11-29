using System;
using System.Threading;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps
{
    public class ExternalProcessStatusStep : DBVersionsStep
    {

        public int StepNumber { get; }

        public int CurrentStepNumber { get; private set; }
        public Exception ProcessExpetion { get; private set; }

        public override string StepName => StepNumber.ToString();


        public bool IsCompleted => CurrentStepNumber >= StepNumber || ProcessExpetion != null;


        public ExternalProcessStatusStep(int stepNumber)
        {
            StepNumber = stepNumber;
        }

        public void SetProcessState(int currentStepNumber, Exception processExpetion)
        {
            CurrentStepNumber = currentStepNumber;
            ProcessExpetion = processExpetion;
        }

        public override void Execute(DBVersionsProcessContext processContext)
        {
            while (!IsCompleted)
            {
                if (ProcessExpetion != null)
                {
                    throw ProcessExpetion;
                }

                Thread.Sleep(15);
            }
        }

    }
}
