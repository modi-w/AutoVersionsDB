using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public class ProcessTraceStateChangeHandler
    {
        private ProcessTraceStateProvider _processTraceStateProvider;

        internal ProcessTraceStateChangeHandler(ProcessTraceStateProvider processTraceStateProvider)
        {
            _processTraceStateProvider = processTraceStateProvider;
        }


        internal string CreateNew(string processName, Action<ProcessTrace, StepNotificationState> onStepNotificationStateChanged)
        {
            string processTraceStateKey = _processTraceStateProvider.CreateNew(processName, onStepNotificationStateChanged);

            RiseNotificationStateChanged(processTraceStateKey);

            return processTraceStateKey;
        }




        internal void StepStart(string processTraceStateKey, string stepName)
        {
            _processTraceStateProvider
                .Get(processTraceStateKey)
                .CurrentStepNotificationState
                .InternalStepNotificationState = new StepNotificationState(stepName);


            //     stepNotificationState.InternalStepNotificationState = null;

            //if (stepNotificationState.NumOfSteps > 0)
            //{
            //    stepNotificationState.LastNotifyPrecents = stepNotificationState.Precents;

            //    RiseNotificationStateChanged();
            //}
        }

        public void SetInternalSteps(string processTraceStateKey, int numOfSteps)
        {
            _processTraceStateProvider
                .Get(processTraceStateKey)
                .CurrentStepNotificationState
                .SetNumOfSteps(numOfSteps);

            RiseNotificationStateChanged(processTraceStateKey);
        }


        internal void StepEnd(string processTraceStateKey)
        {
            StepNotificationState parentStepNotificationState =
                _processTraceStateProvider
                .Get(processTraceStateKey)
                .ParentStepNotificationState;

            parentStepNotificationState.StepNumber++;

            if (parentStepNotificationState.NumOfSteps > 0)
            {
                if (parentStepNotificationState.IsPrecentsAboveMin)
                {
                    parentStepNotificationState.LastNotifyPrecents = parentStepNotificationState.Precents;

                    RiseNotificationStateChanged(processTraceStateKey);
                }
            }

            parentStepNotificationState.InternalStepNotificationState = null;
        }


        internal ProcessTrace ProcessTrace(string processTraceStateKey)
        {
            return _processTraceStateProvider
                .Get(processTraceStateKey)
                .ProcessTrace;
        }

        internal bool HasError(string processTraceStateKey)
        {
            return _processTraceStateProvider
                .Get(processTraceStateKey)
            .ProcessTrace.HasError;
        }


        private void RiseNotificationStateChanged(string processTraceStateKey)
        {
            ProcessTraceState processTraceState = 
                _processTraceStateProvider
                .Get(processTraceStateKey);

            StepNotificationState snapshotNotificationState =
               processTraceState
                .RootStepNotificationState.Clone();

            snapshotNotificationState.SnapshotTimeStemp = DateTime.Now;


            lock (processTraceState.ProcessTrace)
            {
                processTraceState.ProcessTrace.Appand(snapshotNotificationState);
            }

            Task.Run(() =>
            {
                processTraceState.OnStepNotificationStateChanged?.Invoke(processTraceState.ProcessTrace, snapshotNotificationState);
            });
        }

        //internal void ForceStepProgress(StepNotificationState stepNotificationState, int forceSecondaryProcessStepNumber)
        //{
        //    stepNotificationState.StepNumber = forceSecondaryProcessStepNumber;

        //    if (stepNotificationState.IsPrecentsAboveMin)
        //    {
        //        stepNotificationState.LastNotifyPrecents = stepNotificationState.Precents;

        //        RiseNotificationStateChanged();
        //    }
        //}


        internal void StepError(string processTraceStateKey, string errorCode, string errorMessage, string instructionsMessage)
        {
            StepNotificationState currentStepNotificationState =
                _processTraceStateProvider
                .Get(processTraceStateKey)
                .CurrentStepNotificationState;

            currentStepNotificationState.ErrorCode = errorCode;
            currentStepNotificationState.ErrorMesage = errorMessage;
            currentStepNotificationState.InstructionsMessage = instructionsMessage;

            RiseNotificationStateChanged(processTraceStateKey);
        }



        internal void ClearAllInternalProcessState(string processTraceStateKey)
        {
            _processTraceStateProvider
                .Get(processTraceStateKey)
                .RootStepNotificationState
                .InternalStepNotificationState = null;
        }



        



      
    }
}
