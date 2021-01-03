using AutoVersionsDB.NotificationableEngine;
using System;

namespace AutoVersionsDB.CLI
{
    public interface IConsoleProcessMessages : IDisposable
    {
        void OnNotificationStateChanged(ProcessTrace processTrace, StepNotificationState notificationStateItem);
        void ProcessComplete(ProcessResults processReults);
        void StartSpiiner();
        void StopSpinner();

        void SetErrorMessage(string message);

        void SetErrorInstruction(string message);

        void SetInfoMessage(string message);
        void SetInlineInfoMessage(string message, ConsoleColor color);

        void StartProcessMessage(string processName);
        void StartProcessMessage(string processName, string args);
    }
}