using AutoVersionsDB;
using AutoVersionsDB.CLI;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.UI;
using AutoVersionsDB.UI;
using AutoVersionsDB.UI.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.TestsUtils.UI
{
    public class NotificationsViewModelAsserts
    {
        private readonly PropertiesAsserts _propertiesAsserts;

        public NotificationsViewModelAsserts(PropertiesAsserts propertiesAsserts)
        {
            _propertiesAsserts = propertiesAsserts;
        }

        public void AssertNotificationsViewModelCompleteSuccessfully(string testName, NotificationsViewModelData notificationsViewModelData)
        {
            _propertiesAsserts.AssertProperty(testName, nameof(notificationsViewModelData.NotificationStatus), notificationsViewModelData.NotificationStatus.ToString(), NotificationStatus.CompleteSuccessfully.ToString());
            _propertiesAsserts.AssertProperty(testName, nameof(notificationsViewModelData.ProcessStatusMessage), notificationsViewModelData.ProcessStatusMessage, UITextResources.CompleteSuccessfullyMessage);
            _propertiesAsserts.AssertProperty(testName, nameof(notificationsViewModelData.StatusImageType), notificationsViewModelData.StatusImageType.ToString(), StatusImageType.Succeed.ToString());
        }


        public void AssertNotificationsViewModelProcessError(string testName, NotificationsViewModelData notificationsViewModelData)
        {
            _propertiesAsserts.AssertProperty(testName, nameof(notificationsViewModelData.NotificationStatus), notificationsViewModelData.NotificationStatus.ToString(), NotificationStatus.Error.ToString());
            _propertiesAsserts.AssertProperty(testName, nameof(notificationsViewModelData.ProcessStatusMessage), notificationsViewModelData.ProcessStatusMessage, "Error occurred during the process.");
            _propertiesAsserts.AssertProperty(testName, nameof(notificationsViewModelData.StatusImageType), notificationsViewModelData.StatusImageType.ToString(), StatusImageType.Error.ToString());
        }


        public void AssertNotificationsViewModelWaitingForUser(string testName, NotificationsViewModelData notificationsViewModelData)
        {
            _propertiesAsserts.AssertProperty(testName, nameof(notificationsViewModelData.NotificationStatus), notificationsViewModelData.NotificationStatus.ToString(), NotificationStatus.WaitingForUser.ToString());
            _propertiesAsserts.AssertProperty(testName, nameof(notificationsViewModelData.ProcessStatusMessage), notificationsViewModelData.ProcessStatusMessage, "Waiting for your command.");
            _propertiesAsserts.AssertProperty(testName, nameof(notificationsViewModelData.StatusImageType), notificationsViewModelData.StatusImageType.ToString(), StatusImageType.Succeed.ToString());
        }

        public void AssertNotificationsViewModelError(string testName, NotificationsViewModelData notificationsViewModelData, string instrationMessage)
        {
            _propertiesAsserts.AssertProperty(testName, nameof(notificationsViewModelData.NotificationStatus), notificationsViewModelData.NotificationStatus.ToString(), NotificationStatus.Error.ToString());
            _propertiesAsserts.AssertProperty(testName, nameof(notificationsViewModelData.ProcessStatusMessage), notificationsViewModelData.ProcessStatusMessage, instrationMessage);
            _propertiesAsserts.AssertProperty(testName, nameof(notificationsViewModelData.StatusImageType), notificationsViewModelData.StatusImageType.ToString(), StatusImageType.Error.ToString());
        }

        public void AssertNotificationsViewModelAttention(string testName, NotificationsViewModelData notificationsViewModelData, string instrationMessage)
        {
            _propertiesAsserts.AssertProperty(testName, nameof(notificationsViewModelData.NotificationStatus), notificationsViewModelData.NotificationStatus.ToString(), NotificationStatus.Attention.ToString());
            _propertiesAsserts.AssertProperty(testName, nameof(notificationsViewModelData.ProcessStatusMessage), notificationsViewModelData.ProcessStatusMessage, instrationMessage);
            _propertiesAsserts.AssertProperty(testName, nameof(notificationsViewModelData.StatusImageType), notificationsViewModelData.StatusImageType.ToString(), StatusImageType.Warning.ToString());
        }
    }
}
