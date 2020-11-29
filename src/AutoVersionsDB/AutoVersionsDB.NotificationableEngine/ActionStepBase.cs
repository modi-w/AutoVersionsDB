using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public abstract class ActionStepBase
    {
        //private readonly List<ActionStepBase> _internalSteps;
        //public ReadOnlyCollection<ActionStepBase> ReadOnlyInternalSteps
        //{
        //    get
        //    {
        //        return _internalSteps.AsReadOnly();
        //    }
        //}

        public abstract string StepName { get; }

        //public bool IsContinueOnErrorInIternalStep { get; }

        private IStepsExecuter _stepsExecuter;

        public abstract void Execute(ProcessContext processContext);


        protected ActionStepBase()
        {
            //_internalSteps = new List<ActionStepBase>();
        }


        internal void SetStepsExecuter(IStepsExecuter stepsExecuter)
        {
            _stepsExecuter = stepsExecuter;
        }


        //protected void AddInternalStep(ActionStepBase internalStep)
        //{
        //    _internalSteps.Add(internalStep);
        //}




        protected void ExecuteInternalSteps(List<ActionStepBase> internalSteps, bool isContinueOnError)
        {
            _stepsExecuter.ExecuteSteps(internalSteps, isContinueOnError);

            disposStepsList(internalSteps);
        }



        private static void disposStepsList(IEnumerable<ActionStepBase> processStepsToDispose)
        {
            foreach (var processStep in processStepsToDispose)
            {
                //disposStepsList(processStep.ReadOnlyInternalSteps);

                IDisposable disposeStep = processStep as IDisposable;
                if (disposeStep != null)
                {
                    disposeStep.Dispose();
                }
            }
        }
    }



    public abstract class ActionStepBase<TProcessContext> : ActionStepBase
            where TProcessContext : ProcessContext
    {


        public override void Execute(ProcessContext processContext)
        {
            Execute(processContext as TProcessContext);
        }

        public abstract void Execute(TProcessContext processContext);
    }


}
