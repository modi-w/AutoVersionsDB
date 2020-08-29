using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public class ProcessTraceHandler
    {
        private ProcessTrace _processTrace;

        internal ProcessTrace ProcessTrace
        {
            get
            {
                return _processTrace;
            }
        }

        internal bool HasError
        {
            get
            {
                return _processTrace.HasError;
            }
        }


        public ProcessTraceHandler()
        {

        }

        internal void StartProcess(string processName, Action<ProcessTrace, StepNotificationState> onStepNotificationStateChanged)
        {
            _processTrace = new ProcessTrace(processName, onStepNotificationStateChanged);
        }

        internal void StepStart(string stepName)
        {
            _processTrace.StepStart(stepName);
        }


        internal void SetInternalSteps(int numOfSteps)
        {
            _processTrace.SetInternalSteps(numOfSteps);
        }

        internal void StepEnd()
        {
            _processTrace.StepEnd();
        }

        internal void StepError(string errorCode, string errorMessage, string instructionsMessage)
        {
            _processTrace.StepError(errorCode, errorMessage, instructionsMessage);
        }

        internal void ClearAllInternalProcessState()
        {
            _processTrace.ClearAllInternalProcessState();
        }
    }


    public class ProcessTrace
    {
        private readonly List<StepNotificationState> _statesHistory;

        private Action<ProcessTrace, StepNotificationState> OnStepNotificationStateChanged { get; }


        public List<StepNotificationState> StatesHistory
        {
            get
            {
                return _statesHistory.ToList();
            }
        }


        public bool HasError
        {
            get
            {
                bool outVal;

                lock (_statesHistory)
                {
                    outVal = _statesHistory.Any(e => e.HasError);
                }

                return outVal;
            }
        }


        public string ErrorCode
        {
            get
            {
                string outStr = "";

                lock (_statesHistory)
                {
                    var lastStateWithErrorCode = _statesHistory.LastOrDefault(e => !string.IsNullOrWhiteSpace(e.LowLevelErrorCode));
                    if (lastStateWithErrorCode != null)
                    {
                        outStr = lastStateWithErrorCode.LowLevelErrorCode;
                    }
                }

                return outStr;
            }
        }


        public string InstructionsMessage
        {
            get
            {
                string outStr = "";

                lock (_statesHistory)
                {
                    var lastStateWithInstructionsMessage = _statesHistory.LastOrDefault(e => !string.IsNullOrWhiteSpace(e.LowLevelInstructionsMessage));
                    if (lastStateWithInstructionsMessage != null)
                    {
                        outStr = lastStateWithInstructionsMessage.LowLevelInstructionsMessage;
                    }
                }

                return outStr;
            }
        }

        public string InstructionsMessageStepName
        {
            get
            {
                string outStr = "";

                lock (_statesHistory)
                {
                    StepNotificationState lastStateWithInstructionsMessage = _statesHistory.LastOrDefault(e => !string.IsNullOrWhiteSpace(e.LowLevelInstructionsMessage));

                    outStr = lastStateWithInstructionsMessage.LowLevelStepName;
                }

                return outStr;
            }
        }


        private StepNotificationState _rootStepNotificationState;


        private StepNotificationState _parentStepNotificationState
        {
            get
            {
                StepNotificationState parentStepNotificationState = this._rootStepNotificationState;
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





        internal ProcessTrace(string processName, Action<ProcessTrace, StepNotificationState> onStepNotificationStateChanged)
        {
            _statesHistory = new List<StepNotificationState>();

            OnStepNotificationStateChanged = onStepNotificationStateChanged;

            _rootStepNotificationState = new StepNotificationState(processName);

        }


        private void Appand(StepNotificationState notificationStateItem)
        {
            lock (_statesHistory)
            {
                _statesHistory.Add(notificationStateItem);
            }
        }

        internal void StepStart(string stepName)
        {
            this._currentStepNotificationState
                .InternalStepNotificationState = new StepNotificationState(stepName);
        }

        internal void SetInternalSteps(int numOfSteps)
        {
            this._currentStepNotificationState
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
            this._currentStepNotificationState.ErrorCode = errorCode;
            this._currentStepNotificationState.ErrorMesage = errorMessage;
            this._currentStepNotificationState.InstructionsMessage = instructionsMessage;

            RiseNotificationStateChanged();
        }



        internal void ClearAllInternalProcessState()
        {
            this._rootStepNotificationState
                .InternalStepNotificationState = null;
        }


        private void RiseNotificationStateChanged()
        {

            StepNotificationState snapshotNotificationState = this._rootStepNotificationState.Clone();

            snapshotNotificationState.SnapshotTimeStemp = DateTime.Now;

            this.Appand(snapshotNotificationState);

            Task.Run(() =>
            {
                this.OnStepNotificationStateChanged?.Invoke(this, snapshotNotificationState);
            });
        }





        public string GetAllHistoryAsString()
        {
            StringBuilder sbStrResults = new StringBuilder();

            lock (_statesHistory)
            {
                foreach (var notificationState in _statesHistory.ToList())
                {
                    sbStrResults.AppendLine(notificationState.ToString(true, true));
                }
            }

            return sbStrResults.ToString();
        }

        public string GetOnlyErrorsHistoryAsString()
        {
            StringBuilder sbStrResults = new StringBuilder();


            lock (_statesHistory)
            {
                foreach (var notificationState in _statesHistory.Where(e => e.HasError).ToList())
                {
                    sbStrResults.AppendLine(notificationState.ToString(true, true));
                }
            }


            return sbStrResults.ToString();
        }


    }
}
