using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AutoVersionsDB.NotificationableEngine
{
    public abstract class ProcessDefinition : IDisposable
    {
        public abstract string EngineTypeName { get; }

        public ActionStepBase RollbackStep { get; }

        private readonly List<ActionStepBase> _processSteps;
        public ReadOnlyCollection<ActionStepBase> ProcessSteps
        {
            get
            {
                return _processSteps.AsReadOnly();
            }
        }

        public ProcessDefinition(ActionStepBase rollbackStep)
        {
            _processSteps = new List<ActionStepBase>();

            RollbackStep = rollbackStep;
        }

        protected void AddStep(ActionStepBase step)
        {
            _processSteps.Add(step);
        }



        #region IDisposable

        private bool _disposed = false;

        ~ProcessDefinition() => Dispose(false);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {

                if (RollbackStep != null)
                {
                    if (RollbackStep is IDisposable)
                    {
                        (RollbackStep as IDisposable).Dispose();
                    }
                }


                disposStepsList(this.ProcessSteps);
            }

            _disposed = true;
        }

        private static void disposStepsList(IEnumerable<ActionStepBase> processStepsToDispose)
        {
            foreach (var processStep in processStepsToDispose)
            {
                disposStepsList(processStep.ReadOnlyInternalSteps);

                IDisposable disposeStep = processStep as IDisposable;
                if (disposeStep != null)
                {
                    disposeStep.Dispose();
                }
            }
        }

        #endregion

    }
}
