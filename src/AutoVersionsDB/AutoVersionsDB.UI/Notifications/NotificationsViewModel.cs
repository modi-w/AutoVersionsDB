using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.UI.StatesLog;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace AutoVersionsDB.UI.Notifications
{

    public class StatesLogViewModelEventArgs : EventArgs
    {
        public StatesLogViewModel StatesLogViewModel { get; set; }

        public StatesLogViewModelEventArgs(StatesLogViewModel statesLogViewModel)
        {
            StatesLogViewModel = statesLogViewModel;
        }

    }

    public class NotificationsViewModel : INotificationsViewModel
    {

        private ProcessTrace _processTrace;
        public bool HasProcessTrace => _processTrace != null;


        public NotificationsViewModelData NotificationsViewModelData { get; }
        public NotificationsControls NotificationsControls { get; }


        public StatesLogViewModel StatesLogViewModel { get; }




        public bool IsEventsBinded { get; set; }

        public event EventHandler<StatesLogViewModelEventArgs> OnShowStatesLog;


        public RelayCommand ShowStatesLogViewCommand { get; private set; }




        public NotificationsViewModel(NotificationsViewModelData notificationsViewModelData,
                                        NotificationsControls notificationsControls)
        {
            NotificationsViewModelData = notificationsViewModelData;
            NotificationsControls = notificationsControls;

            ShowStatesLogViewCommand = new RelayCommand(ShowStatesLogView);

            NotificationsViewModelData.PropertyChanged += NotificationsViewModelData_PropertyChanged;
        }

        private void NotificationsViewModelData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {
                case nameof(NotificationsViewModelData.NotificationStatus):

                    NotificationStatusChanged();
                    break;

                default:
                    break;
            }
        }

        private void ShowStatesLogView()
        {
            FireOnShowStatesLog(_processTrace);
        }



        private void NotificationStatusChanged()
        {
            switch (NotificationsViewModelData.NotificationStatus)
            {
                case NotificationStatus.WaitingForUser:

                    NotificationsControls.StatusImageVisible = false;

                    NotificationsControls.ProcessStatusMessageColor = Color.Black;

                    break;

                case NotificationStatus.InProgress:

                    NotificationsControls.StatusImageVisible = true;
                    NotificationsViewModelData.StatusImageType = StatusImageType.Spinner;

                    NotificationsControls.ProcessStatusMessageColor = Color.Black;
                    break;


                case NotificationStatus.Error:

                    NotificationsControls.StatusImageVisible = true;
                    NotificationsViewModelData.StatusImageType = StatusImageType.Error;

                    NotificationsControls.ProcessStatusMessageColor = Color.DarkRed;
                    break;

                case NotificationStatus.CompleteSuccessfully:

                    NotificationsControls.StatusImageVisible = true;
                    NotificationsViewModelData.StatusImageType = StatusImageType.Succeed;

                    NotificationsControls.ProcessStatusMessageColor = Color.Black;
                    break;

                case NotificationStatus.Attention:

                    NotificationsControls.StatusImageVisible = true;
                    NotificationsViewModelData.StatusImageType = StatusImageType.Warning;

                    NotificationsControls.ProcessStatusMessageColor = Color.DarkOrange;
                    break;

                case NotificationStatus.None:
                default:
                    break;
            }

            NotificationsControls.BtnProcessLogVisible = HasProcessTrace;
        }



        public void OnNotificationStateChanged(ProcessTrace processTrace, StepNotificationState notificationStateItem)
        {
            notificationStateItem.ThrowIfNull(nameof(notificationStateItem));

            _processTrace = processTrace;

            if (NotificationsViewModelData.NotificationStatus != NotificationStatus.InProgress)
            {
                NotificationsViewModelData.NotificationStatus = NotificationStatus.InProgress;
            }

            if (!string.IsNullOrWhiteSpace(notificationStateItem.LowLevelInstructionsMessage))
            {
                NotificationsViewModelData.ProcessStatusMessage = notificationStateItem.LowLevelInstructionsMessage;
            }
            else
            {
                NotificationsViewModelData.ProcessStatusMessage = notificationStateItem.ToString();
            }
        }



        public void WaitingForUser()
        {
            System.Threading.Thread.Sleep(500);

            NotificationsViewModelData.NotificationStatus = NotificationStatus.WaitingForUser;
            NotificationsViewModelData.ProcessStatusMessage = "Waiting for your command.";
        }

        public void Preparing()
        {
            NotificationsViewModelData.NotificationStatus = NotificationStatus.InProgress;
            NotificationsViewModelData.ProcessStatusMessage = "Please wait, preparing...";
        }


        public void SetAttentionMessage(string message)
        {
            System.Threading.Thread.Sleep(500);

            NotificationsViewModelData.NotificationStatus = NotificationStatus.Attention;
            NotificationsViewModelData.ProcessStatusMessage = message;
        }



        public void BeforeStartProcess()
        {
            NotificationsViewModelData.NotificationStatus = NotificationStatus.InProgress;
            NotificationsViewModelData.ProcessStatusMessage = "Prepare...";
        }

        public void AfterComplete(ProcessResults processResults)
        {
            processResults.ThrowIfNull(nameof(processResults));

            _processTrace = processResults.Trace;

            System.Threading.Thread.Sleep(500);


            if (_processTrace.HasError)
            {
                NotificationsViewModelData.ProcessStatusMessage = _processTrace.InstructionsMessage;

                NotificationsViewModelData.NotificationStatus = _processTrace.NotificationErrorType 
                    switch
                {
                    NotificationErrorType.Error => NotificationStatus.Error,
                    NotificationErrorType.Attention => NotificationStatus.Attention,
                    _ => throw new Exception($"Invalid NotificationErrorType '{_processTrace.NotificationErrorType}'"),
                };
            }
            else
            {
                NotificationsViewModelData.NotificationStatus = NotificationStatus.CompleteSuccessfully;
                NotificationsViewModelData.ProcessStatusMessage = "The process complete successfully";
            }

        }






        private void FireOnShowStatesLog(ProcessTrace processTrace)
        {
            if (OnShowStatesLog == null)
            {
                throw new Exception($"Bind method to 'OnShowStatesLog' event is mandatory");
            }

            StatesLogViewModel statesLogViewModel = new StatesLogViewModel(processTrace);

            OnShowStatesLog(this, new StatesLogViewModelEventArgs(statesLogViewModel));
        }




        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion


    }
}
