using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AutoVersionsDB.Core.ProcessSteps
{
    public class ExternalProcessStatusStep : AutoVersionsDbStep
    {

        public int StepNumber { get; }

        public int CurrentStepNumber { get; private set; }
        public Exception ProcessExpetion { get; private set; }

        public override string StepName => StepNumber.ToString();


        public bool IsCompleted
        {
            get
            {
                return CurrentStepNumber >= StepNumber || ProcessExpetion!=null;
            }
        }


        public ExternalProcessStatusStep(int stepNumber)
        {
            StepNumber = stepNumber;
        }

        public void SetProcessState( int currentStepNumber, Exception processExpetion)
        {
            CurrentStepNumber = currentStepNumber;
            ProcessExpetion = processExpetion;
        }

        public override void Execute(AutoVersionsDbEngineContext processState)
        {
            while (!IsCompleted)
            {
                if (ProcessExpetion!= null)
                {
                    throw ProcessExpetion;
                }

                Thread.Sleep(15);
            }
        }

    }
}
