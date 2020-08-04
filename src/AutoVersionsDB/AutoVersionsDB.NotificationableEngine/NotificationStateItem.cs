using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public class NotificationStateItem
    {
        private readonly double _minPrecentChangeToNotify = 1;


        public NotificationStateItem Clone()
        {
            NotificationStateItem outItem = this.MemberwiseClone() as NotificationStateItem;

            if (InternalNotificationStateItem != null)
            {
                outItem.InternalNotificationStateItem = this.InternalNotificationStateItem.Clone();
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

                if (InternalNotificationStateItem != null)
                {
                    outLowLevelStepName = InternalNotificationStateItem.StepName;
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

                if (InternalNotificationStateItem != null)
                {
                    if (!string.IsNullOrWhiteSpace(InternalNotificationStateItem.LowLevelErrorCode))
                    {
                        outLowLevelErrorCode = InternalNotificationStateItem.LowLevelErrorCode;
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

                if (InternalNotificationStateItem != null)
                {
                    if (!string.IsNullOrWhiteSpace(InternalNotificationStateItem.LowLevelErrorMessage))
                    {
                        outErrorMessage = InternalNotificationStateItem.LowLevelErrorMessage;

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

                if (InternalNotificationStateItem != null)
                {
                    if (!string.IsNullOrWhiteSpace(InternalNotificationStateItem.LowLevelInstructionsMessage))
                    {
                        outInstructionsMessage = InternalNotificationStateItem.LowLevelInstructionsMessage;

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




        public NotificationStateItem InternalNotificationStateItem { get; set; }




        internal NotificationStateItem(int numOfSteps)
        {
            NumOfSteps = numOfSteps;
        }



      



        public override string ToString()
        {
            return ToString(false, false);
        }

        public string ToString(bool isIncludeTimestamp, bool isIncludeStepStage)
        {
            string outStr;

            string stepStageStr = "";
            if (isIncludeStepStage)
            {
                stepStageStr = $" ({StepNumber}/{NumOfSteps})";
            }

            //if (parentStateItem == null)
            //{
            //    outStr = $"Process {Precents:N0}%{stepStageStr} -> {StepName}";
            //}
            //else
            //{
            outStr = $"{Precents:N0}%{stepStageStr} -> {StepName}";
            //    }

            if (InternalNotificationStateItem != null)
            {
                outStr = $"{outStr} {InternalNotificationStateItem.ToString(false, isIncludeStepStage)}";
            }



            //if (isIncludeStepStage)
            //{
            //    outStr = $"{StepName} {Precents:N0}% ({StepNumber}/{NumOfSteps})";
            //}
            //else
            //{
            //    outStr = $"{StepName} {Precents:N0}%";

            //}


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
