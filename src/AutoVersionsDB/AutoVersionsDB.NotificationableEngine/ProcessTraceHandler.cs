using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public class ProcessTraceHandler : IDisposable
    {
        private BlockingCollection<StepNotificationState> _stepsNotificationStateChangedQueue;
        private Action<ProcessTrace, StepNotificationState> _onStepNotificationStateChanged;
        private StepNotificationState _rootStepNotificationState;
        private Task _riseStepNotificationStateChangesTask;

        private StepNotificationState ParentStepNotificationState
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

        private StepNotificationState CurrentStepNotificationState
        {
            get
            {
                StepNotificationState parentStepNotificationState = ParentStepNotificationState;

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


        internal ProcessTrace ProcessTrace { get; private set; }


        internal bool HasError => ProcessTrace.HasError;


        public ProcessTraceHandler()
        {

        }

        internal void StartProcess(string processName, Action<ProcessTrace, StepNotificationState> onStepNotificationStateChanged)
        {
            ProcessTrace = new ProcessTrace();

            _onStepNotificationStateChanged = onStepNotificationStateChanged;

            _rootStepNotificationState = new StepNotificationState(processName);

            _stepsNotificationStateChangedQueue = new BlockingCollection<StepNotificationState>();

            RiseStepNotificationStateChanges();
        }

        internal void StepStart(string stepName)
        {
            CurrentStepNotificationState
                .InternalStepNotificationState = new StepNotificationState(stepName);
        }

        internal void SetNumOfInternalSteps(int numOfSteps)
        {
            CurrentStepNotificationState
                .SetNumOfSteps(numOfSteps);

            SaveNotificationStateSnapshot();
        }


        internal void StepEnd()
        {
            ParentStepNotificationState.StepNumber++;

            if (ParentStepNotificationState.NumOfSteps > 0)
            {
                if (ParentStepNotificationState.IsPrecentsAboveMin)
                {
                    ParentStepNotificationState.LastNotifyPrecents = ParentStepNotificationState.Precents;

                    SaveNotificationStateSnapshot();
                }
            }

            ParentStepNotificationState.InternalStepNotificationState = null;
        }


        internal void StepError(string errorCode, string errorMessage, string instructionsMessage, NotificationErrorType notificationErrorType)
        {
            CurrentStepNotificationState.ErrorCode = errorCode;
            CurrentStepNotificationState.ErrorMesage = errorMessage;
            CurrentStepNotificationState.InstructionsMessage = instructionsMessage;
            CurrentStepNotificationState.NotificationErrorType = notificationErrorType;

            SaveNotificationStateSnapshot();
        }



        internal void ClearAllStepNotificationState()
        {
            _rootStepNotificationState
                .InternalStepNotificationState = null;
        }


        private void SaveNotificationStateSnapshot()
        {

            StepNotificationState snapshotNotificationState = _rootStepNotificationState.Clone();

            snapshotNotificationState.SnapshotTimeStemp = DateTime.Now;

            ProcessTrace.Appand(snapshotNotificationState);

            if (_stepsNotificationStateChangedQueue.IsCompleted)
            {

            }
            _stepsNotificationStateChangedQueue.Add(snapshotNotificationState);
        }


        private void RiseStepNotificationStateChanges()
        {

            _riseStepNotificationStateChangesTask = Task.Run(() =>
             {
                 try
                 {
                     while (true)
                     {
                         StepNotificationState snapshotNotificationState = _stepsNotificationStateChangedQueue.Take();

                         if (snapshotNotificationState.StepName != "EndProcessDummyStepNotificationState")
                         {
                             try
                             {
                                 _onStepNotificationStateChanged?.Invoke(ProcessTrace, snapshotNotificationState);
                             }
                             catch (Exception ex)
                             {
                                 StepError("OnStepNotificationStateChanged", ex.ToString(), "Error occured on your OnStepNotificationStateChanged callback method", NotificationErrorType.Error);
                             }
                         }
                         else
                         {
                             break;
                         }

                     }
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine($"ProcessTraceHandler.RiseStepNotificationStateChanges() >>> {ex}");
                 }

             });
        }


        public void WaitForStepNotificationStateChangesComplete()
        {
            StepNotificationState endDummyStepNotificationState = new StepNotificationState("EndProcessDummyStepNotificationState");
            _stepsNotificationStateChangedQueue.Add(endDummyStepNotificationState);
            _riseStepNotificationStateChangesTask.Wait();
        }



        #region IDisposable

        private bool _disposed;

        ~ProcessTraceHandler() => Dispose(false);

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

                if (_stepsNotificationStateChangedQueue != null)
                {
                    _stepsNotificationStateChangedQueue.Dispose();
                }
            }

            _disposed = true;
        }



        #endregion

    }
}
