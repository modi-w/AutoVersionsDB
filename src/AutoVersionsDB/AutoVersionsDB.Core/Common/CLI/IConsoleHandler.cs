using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.Common.CLI
{
    public interface IConsoleHandler
    {
        void OnNotificationStateChanged(ProcessTrace processTrace, StepNotificationState notificationStateItem);
        void ProcessComplete(ProcessTrace processReults);
        void StartSpiiner();
        void StopSpinner();

        void SetErrorMessage(string message);

        void SetErrorInstruction(string message);

        void WriteLineInfo(string message);

    }
}