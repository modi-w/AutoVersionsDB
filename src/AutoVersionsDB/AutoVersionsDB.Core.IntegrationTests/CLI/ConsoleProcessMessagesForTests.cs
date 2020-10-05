using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.Common.CLI;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.CLI;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.CLI
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
