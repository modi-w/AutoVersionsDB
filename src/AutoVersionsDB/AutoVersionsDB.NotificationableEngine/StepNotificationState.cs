using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public class StepNotificationState
    {
        private readonly double _minPrecentChangeToNotify = 1;


        public StepNotificationState Clone()
        {
            StepNotificationState outItem = this.MemberwiseClone() as StepNotificationState;

            if (InternalStepNotificationState != null)
            {
                outItem.InternalStepNotificationState = this.InternalStepNotificationState.Clone();
            }

            return outItem;
        }

        public DateTime SnapshotTimeStemp { get; set; }

        public int NumOfSteps { get; set; }
        public string StepName { get; set; }

        public int StepNumber { get; set; }
        public double Precents
        {
            get
            {
                double results = 0;

                if (NumOfSteps != 0)
                {
                    results = (double)StepNumber / (double)NumOfSteps * 100;
                }

                return results;
            }
        }

        internal double LastNotifyPrecents { get; set; }
        internal bool IsPrecentsAboveMin
        {
            get
            {
                return LastNotifyPrecents == 0
                    || (Precents - LastNotifyPrecents) > _minPrecentChangeToNotify;
            }
        }


        public string LowLevelStepName
        {
            get
            {
                string outLowLevelStepName = StepName;

                if (InternalStepNotificationState != null)
                {
                    outLowLevelStepName = InternalStepNotificationState.StepName;
                }

                return outLowLevelStepName;
            }
        }


        public string ErrorCode { get; set; }
        public string LowLevelErrorCode
        {
            get
            {
                string outLowLevelErrorCode = ErrorCode;

                if (InternalStepNotificationState != null)
                {
                    if (!string.IsNullOrWhiteSpace(InternalStepNotificationState.LowLevelErrorCode))
                    {
                        outLowLevelErrorCode = InternalStepNotificationState.LowLevelErrorCode;
                    }
                }

                return outLowLevelErrorCode;
            }
        }


        public string ErrorMesage { get; set; }
        public string LowLevelErrorMessage
        {
            get
            {
                string outErrorMessage = ErrorMesage;

                if (InternalStepNotificationState != null)
                {
                    if (!string.IsNullOrWhiteSpace(InternalStepNotificationState.LowLevelErrorMessage))
                    {
                        outErrorMessage = InternalStepNotificationState.LowLevelErrorMessage;

                        //if (!string.IsNullOrWhiteSpace(outErrorMessage))
                        //{
                        //    outErrorMessage = $"{outErrorMessage} -> {internalErrorMessage}";
                        //}
                        //else
                        //{
                        //    outErrorMessage = internalErrorMessage;
                        //}
                    }
                }

                return outErrorMessage;
            }
        }

        public bool HasError
        {
            get
            {
                return !string.IsNullOrWhiteSpace(LowLevelErrorMessage);
            }
        }

        public string InstructionsMessage { get; set; }
        public string LowLevelInstructionsMessage
        {
            get
            {
                string outInstructionsMessage = InstructionsMessage;

                if (InternalStepNotificationState != null)
                {
                    if (!string.IsNullOrWhiteSpace(InternalStepNotificationState.LowLevelInstructionsMessage))
                    {
                        outInstructionsMessage = InternalStepNotificationState.LowLevelInstructionsMessage;

                        //string internalInstructionsMessage = InternalNotificationStateItem.InstructionsMessage;

                        //if (!string.IsNullOrWhiteSpace(outInstructionsMessage))
                        //{
                        //    outInstructionsMessage = $"{outInstructionsMessage} -> {internalInstructionsMessage}";
                        //}
                        //else
                        //{
                        //    outInstructionsMessage = internalInstructionsMessage;
                        //}
                    }
                }

                return outInstructionsMessage;
            }
        }




        public StepNotificationState InternalStepNotificationState { get; set; }




        internal StepNotificationState(string stepName)
        {
            StepName = stepName;
        }

        internal void SetNumOfSteps(int numOfSteps)
        {
            NumOfSteps = numOfSteps;
        }

        //internal void CreateInternalNotificationStateItem(int numOfSteps)
        //{
        //    InternalNotificationStateItem = new NotificationStateItem(numOfSteps);
        //}






        public override string ToString()
        {
            return ToString(false, false);
        }

        public string ToString(bool isIncludeTimestamp, bool isIncludeStepStage)
        {
            string outStr;

            string stepStageStr = "";
            if (NumOfSteps>0
                && isIncludeStepStage)
            {
                stepStageStr = $" ({StepNumber}/{NumOfSteps})";
            }


            //if (NumOfSteps > 0)
            //{
            //    outStr = $"{Precents:N0}%{stepStageStr} -> {StepName}";
            //}
            //else
            //{
            //    outStr = $" -> {StepName}";
            //}

            if (NumOfSteps > 0)
            {
                outStr = $"{StepName} {Precents:N0}%{stepStageStr}";
            }
            else
            {
                outStr = $"{StepName}";
            }


            if (InternalStepNotificationState != null)
            {
                outStr = $"{outStr} -> {InternalStepNotificationState.ToString(false, isIncludeStepStage)}";
            }



           


            //if (InternalNotificationStateItem != null)
            //{
            //    outStr = $"{outStr} -> {InternalNotificationStateItem.ToString(this,false, isIncludeStepStage)}";
            //}

            if (HasError)
            {
                outStr = $"{outStr}. Error: {LowLevelErrorMessage}";
            }

            if (isIncludeTimestamp)
            {
                outStr = $"{SnapshotTimeStemp:HH:mm:ss.fff} >> {outStr}";
            }

            return outStr;
        }
    }
}
