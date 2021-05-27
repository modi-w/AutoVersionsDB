using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.UI.StatesLog;
using System;
using System.ComponentModel;

namespace AutoVersionsDB.UI.Notifications
{
    public interface INotificationsViewModel : INotifyPropertyChanged
    {
         bool HasProcessTrace { get; }
        bool IsEventsBinded { get; set; }
        NotificationsControls NotificationsControls { get; }
        NotificationsViewModelData NotificationsViewModelData { get; }
        RelayCommand ShowStatesLogViewCommand { get; }
        StatesLogViewModel StatesLogViewModel { get; }

        event EventHandler<StatesLogViewModelEventArgs> OnShowStatesLog;

        void AfterComplete(ProcessResults processResults);
        void BeforeStartProcess();
        void OnNotificationStateChanged(ProcessTrace processTrace, StepNotificationState notificationStateItem);
        void Preparing();
        void SetAttentionMessage(string message);
        void WaitingForUser();
    }
}