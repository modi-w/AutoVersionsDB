using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.NotificationableEngine
{
    public class NotificationStateItem
    {
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


        //public bool HasStepName(string stepName)
        //{
        //    if (CurrentStepName == stepName)
        //    {
        //        return true;
        //    }
        //    else if (InternalNotificationStateItem != null)
        //    {
        //        return InternalNotificationStateItem.HasStepName(stepName);
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}


        public NotificationStateItem(int numOfSteps)
        {
            NumOfSteps = numOfSteps;
        }


        public void StepStart(string stepName, string additionalStepInfo)
        {
            this.StepName = stepName;

            if (!string.IsNullOrWhiteSpace(additionalStepInfo))
            {
                this.StepName = $"{this.StepName} - {additionalStepInfo}";
            }

            InternalNotificationStateItem = null;
        }

        public void StepEnd()
        {
            this.StepNumber++;

        }

        public void StepsProgressByValue(int forceSecondaryProcessStepNumber)
        {
            this.StepNumber = forceSecondaryProcessStepNumber;

        }


        public void StepError(string errorCode,string errorMessage, string instructionsMessage)
        {
            this.ErrorCode = errorCode;
            this.ErrorMesage = errorMessage;
            this.InstructionsMessage = instructionsMessage;
        }




        public override string ToString()
        {
            return ToString(false, false);
        }

        public string ToString(bool isIncludeTimestamp, bool isIncludeStepStage)
        {
            string outStr = "";

            if (isIncludeStepStage)
            {
                outStr = $" {Precents:N0}% ({StepNumber}/{NumOfSteps}) {StepName}";
            }
            else
            {
                outStr = $" {Precents:N0}% {StepName}";

            }


            if (InternalNotificationStateItem != null)
            {
                outStr = $"{outStr} -> {InternalNotificationStateItem.ToString(false, isIncludeStepStage)}";
            }

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
