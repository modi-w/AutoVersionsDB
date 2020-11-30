using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.UI;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.UI;
using AutoVersionsDB.UI.Notifications;
using AutoVersionsDB.UI.StatesLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.TestsUtils.UI
{


    public class NotificationsViewModelForTests : INotificationsViewModel
    {
        private readonly NotificationsViewModel _notificationsViewModel;

        public bool IsEventsBinded
        {
            get => _notificationsViewModel.IsEventsBinded;
            set => _notificationsViewModel.IsEventsBinded = value;
        }

        public NotificationsControls NotificationsControls => _notificationsViewModel.NotificationsControls;

        public NotificationsViewModelData NotificationsViewModelData => _notificationsViewModel.NotificationsViewModelData;

        public RelayCommand ShowStatesLogViewCommand => _notificationsViewModel.ShowStatesLogViewCommand;

        public StatesLogViewModel StatesLogViewModel => _notificationsViewModel.StatesLogViewModel;

        public event EventHandler<StatesLogViewModelEventArgs> OnShowStatesLog
        {
            add
            {
                _notificationsViewModel.OnShowStatesLog += value;
            }
            remove
            {
                _notificationsViewModel.OnShowStatesLog -= value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                _notificationsViewModel.PropertyChanged += value;
            }
            remove
            {
                _notificationsViewModel.PropertyChanged -= value;
            }
        }



        public NotificationsViewModelForTests(NotificationsViewModel notificationsViewModel)
        {
            _notificationsViewModel = notificationsViewModel;
        }

        public void AfterComplete(ProcessResults processResults)
        {
            _notificationsViewModel.AfterComplete(processResults);

            AfterCompleteForMockSniffer(processResults);
        }

        public virtual void AfterCompleteForMockSniffer(ProcessResults processReults)
        {

        }


        public void BeforeStartProcess()
        {
            _notificationsViewModel.BeforeStartProcess();
        }

        public void OnNotificationStateChanged(ProcessTrace processTrace, StepNotificationState notificationStateItem)
        {
            _notificationsViewModel.OnNotificationStateChanged(processTrace, notificationStateItem);
        }

        public void Preparing()
        {
            _notificationsViewModel.Preparing();
        }

        public void SetAttentionMessage(string message)
        {
            _notificationsViewModel.SetAttentionMessage(message);
        }

        public void WaitingForUser()
        {
            _notificationsViewModel.WaitingForUser();
        }
    }
}
