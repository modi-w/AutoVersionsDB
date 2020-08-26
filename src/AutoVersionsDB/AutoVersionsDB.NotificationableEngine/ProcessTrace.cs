using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
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


        internal StepNotificationState RootStepNotificationState { get; }


        internal StepNotificationState ParentStepNotificationState
        {
            get
            {
                StepNotificationState parentStepNotificationState = this.RootStepNotificationState;
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

        internal StepNotificationState CurrentStepNotificationState
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





        internal ProcessTrace(string processName, Action<ProcessTrace, StepNotificationState> onStepNotificationStateChanged)
        {
            _statesHistory = new List<StepNotificationState>();

            OnStepNotificationStateChanged = onStepNotificationStateChanged;

            RootStepNotificationState = new StepNotificationState(processName);

        }


        internal void Appand(StepNotificationState notificationStateItem)
        {
            lock (_statesHistory)
            {
                _statesHistory.Add(notificationStateItem);
            }
        }

        internal void StepStart(string stepName)
        {
            this.CurrentStepNotificationState
                .InternalStepNotificationState = new StepNotificationState(stepName);


            //     stepNotificationState.InternalStepNotificationState = null;

            //if (stepNotificationState.NumOfSteps > 0)
            //{
            //    stepNotificationState.LastNotifyPrecents = stepNotificationState.Precents;

            //    RiseNotificationStateChanged();
            //}
        }

        internal void SetInternalSteps( int numOfSteps)
        {
            this.CurrentStepNotificationState
                .SetNumOfSteps(numOfSteps);

            RiseNotificationStateChanged();
        }


        internal void StepEnd()
        {
            ParentStepNotificationState.StepNumber++;

            if (ParentStepNotificationState.NumOfSteps > 0)
            {
                if (ParentStepNotificationState.IsPrecentsAboveMin)
                {
                    ParentStepNotificationState.LastNotifyPrecents = ParentStepNotificationState.Precents;

                    RiseNotificationStateChanged();
                }
            }

            ParentStepNotificationState.InternalStepNotificationState = null;
        }


        internal void StepError( string errorCode, string errorMessage, string instructionsMessage)
        {
            this.CurrentStepNotificationState.ErrorCode = errorCode;
            this.CurrentStepNotificationState.ErrorMesage = errorMessage;
            this.CurrentStepNotificationState.InstructionsMessage = instructionsMessage;

            RiseNotificationStateChanged();
        }



        internal void ClearAllInternalProcessState()
        {
            this.RootStepNotificationState
                .InternalStepNotificationState = null;
        }


        private void RiseNotificationStateChanged()
        {

            StepNotificationState snapshotNotificationState = this.RootStepNotificationState.Clone();

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
