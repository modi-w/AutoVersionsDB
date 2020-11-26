using AutoVersionsDB;
using AutoVersionsDB.CLI;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI
{
    public class ConsoleProcessMessagesForTests : IConsoleProcessMessages
    {

        private ConsoleProcessMessages _consoleProcessMessages;

        public ConsoleProcessMessagesForTests(ConsoleProcessMessages consoleProcessMessages)
        {
            _consoleProcessMessages = consoleProcessMessages;
        }

        public void OnNotificationStateChanged(ProcessTrace processTrace, StepNotificationState notificationStateItem)
        {
            _consoleProcessMessages.OnNotificationStateChanged(processTrace, notificationStateItem);
        }

        public void ProcessComplete(ProcessResults processReults)
        {
            _consoleProcessMessages.ProcessComplete(processReults);

            ProcessCompleteForMockSniffer(processReults);
        }

        public virtual void ProcessCompleteForMockSniffer(ProcessResults processReults)
        {

        }


        public void SetErrorInstruction(string message)
        {
            _consoleProcessMessages.SetErrorInstruction(message);
        }

        public void SetErrorMessage(string message)
        {
            _consoleProcessMessages.SetErrorMessage(message);
        }

        public void SetInfoMessage(string message)
        {
            _consoleProcessMessages.SetInfoMessage(message);
        }

        public void SetInlineInfoMessage(string message, ConsoleColor color)
        {
            _consoleProcessMessages.SetInlineInfoMessage(message, color);
        }


        public void StartProcessMessage(string processName)
        {
            _consoleProcessMessages.SetInfoMessage(processName);
        }

        public void StartProcessMessage(string processName, string paramsStr)
        {
            _consoleProcessMessages.StartProcessMessage(processName, paramsStr);
        }

        public void StartSpiiner()
        {
            _consoleProcessMessages.StartSpiiner();
        }

        public void StopSpinner()
        {
            _consoleProcessMessages.StopSpinner();
        }



        public void Dispose()
        {
            _consoleProcessMessages.Dispose();
        }

    }
}
