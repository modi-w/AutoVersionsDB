using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.Common.CLI
{
    public interface IConsoleProcessMessages
    {
        void OnNotificationStateChanged(ProcessTrace processTrace, StepNotificationState notificationStateItem);
        void ProcessComplete(ProcessResults processReults);
        void StartSpiiner();
        void StopSpinner();

        void SetErrorMessage(string message);

        void SetErrorInstruction(string message);

        void SetInfoMessage(string message);

        void StartProcessMessage(string processName);
        void StartProcessMessage(string processName, string paramsStr);
    }
}