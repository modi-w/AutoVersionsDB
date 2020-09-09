using System;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public class ProcessTraceHandler
    {
        internal ProcessTrace ProcessTrace { get; private set; }


        private Action<ProcessTrace, StepNotificationState> _onStepNotificationStateChanged;


        private StepNotificationState _rootStepNotificationState;

        private StepNotificationState _parentStepNotificationState
        {
            get
            {
                StepNotificationState parentStepNotificationState = _rootStepNotificationState;
                StepNotificationState prevParentStepNotificationState = parentStepNotificationState;

                while (parentStepNotificationState != null
                        && parentStepNotificationState.InternalStepNotificationState != null)
                {
                    prevParentStepNotificationState = parentStepNotificationState;
                    parentStepNotificationState = parentStepNotificationState.InternalStepNotificationState;
                }

                return prevParentStepNotificationState;
            }
        }

        private StepNotificationState _currentStepNotificationState
        {
            get
            {
                StepNotificationState parentStepNotificationState = _parentStepNotificationState;

                if (parentStepNotificationState.InternalStepNotificationState == null)
                {
                    return parentStepNotificationState;
                }
                else
                {
                    return parentStepNotificationState.InternalStepNotificationState;
                }

            }
        }




        internal bool HasError
        {
            get
            {
                return ProcessTrace.HasError;
            }
        }


        public ProcessTraceHandler()
        {
        }

        internal void StartProcess(string processName, Action<ProcessTrace, StepNotificationState> onStepNotificationStateChanged)
        {
            ProcessTrace = new ProcessTrace();

            _onStepNotificationStateChanged = onStepNotificationStateChanged;

            _rootStepNotificationState = new StepNotificationState(processName);

        }

        internal void StepStart(string stepName)
        {
            _currentStepNotificationState
                .InternalStepNotificationState = new StepNotificationState(stepName);
        }

        internal void SetNumOfInternalSteps(int numOfSteps)
        {
            _currentStepNotificationState
                .SetNumOfSteps(numOfSteps);

            RiseNotificationStateChanged();
        }


        internal void StepEnd()
        {
            _parentStepNotificationState.StepNumber++;

            if (_parentStepNotificationState.NumOfSteps > 0)
            {
                if (_parentStepNotificationState.IsPrecentsAboveMin)
                {
                    _parentStepNotificationState.LastNotifyPrecents = _parentStepNotificationState.Precents;

                    RiseNotificationStateChanged();
                }
            }

            _parentStepNotificationState.InternalStepNotificationState = null;
        }


        internal void StepError(string errorCode, string errorMessage, string instructionsMessage)
        {
            _currentStepNotificationState.ErrorCode = errorCode;
            _currentStepNotificationState.ErrorMesage = errorMessage;
            _currentStepNotificationState.InstructionsMessage = instructionsMessage;

            RiseNotificationStateChanged();
        }



        internal void ClearAllStepNotificationState()
        {
            _rootStepNotificationState
                .InternalStepNotificationState = null;
        }


        private void RiseNotificationStateChanged()
        {

            StepNotificationState snapshotNotificationState = _rootStepNotificationState.Clone();

            snapshotNotificationState.SnapshotTimeStemp = DateTime.Now;

            ProcessTrace.Appand(snapshotNotificationState);

            Task.Run(() =>
            {
                _onStepNotificationStateChanged?.Invoke(ProcessTrace, snapshotNotificationState);
            });
        }

    }
}
