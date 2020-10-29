using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.UI.StatesLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AutoVersionsDB.UI.Notifications
{


    public partial class NotificationsViewModel : INotifyPropertyChanged
    {

        private ProcessTrace _processTrace;

        
        public NotificationsViewModelData NotificationsViewModelData { get; }
        public NotificationsControls NotificationsControls { get; }


        public StatesLogViewModel StatesLogViewModel { get; }




        public bool IsEventsBinded { get; set; }

        public event OnShowStatesLogEventHandler OnShowStatesLog;


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
            fireOnShowStatesLog(_processTrace);
        }



        private void NotificationStatusChanged()
        {
            switch (NotificationsViewModelData.NotificationStatus)
            {
                case eNotificationStatus.WaitingForUser:

                   NotificationsControls.StatusImageVisible = false;

                    NotificationsControls.ProcessStatusMessageColor = Color.Black;

                    break;

                case eNotificationStatus.InProgress:

                    NotificationsControls.StatusImageVisible = true;
                    NotificationsViewModelData.StatusImageType = eStatusImageType.Spinner;

                    NotificationsControls.ProcessStatusMessageColor = Color.Black;
                    break;


                case eNotificationStatus.Error:

                    NotificationsControls.StatusImageVisible = true;
                    NotificationsViewModelData.StatusImageType = eStatusImageType.Error;

                    NotificationsControls.ProcessStatusMessageColor = Color.DarkRed;
                    break;

                case eNotificationStatus.CompleteSuccessfully:

                    NotificationsControls.StatusImageVisible = true;
                    NotificationsViewModelData.StatusImageType = eStatusImageType.Succeed;

                    NotificationsControls.ProcessStatusMessageColor = Color.Black;
                    break;

                case eNotificationStatus.Attention:

                    NotificationsControls.StatusImageVisible = true;
                    NotificationsViewModelData.StatusImageType = eStatusImageType.Warning;

                    NotificationsControls.ProcessStatusMessageColor = Color.DarkOrange;
                    break;

                case eNotificationStatus.None:
                default:
                    break;
            }
        }



        public void OnNotificationStateChanged(ProcessTrace processTrace, StepNotificationState notificationStateItem)
        {
            _processTrace = processTrace;

            if (NotificationsViewModelData.NotificationStatus != eNotificationStatus.InProgress)
            {
                NotificationsViewModelData.NotificationStatus = eNotificationStatus.InProgress;
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

            NotificationsViewModelData.NotificationStatus = eNotificationStatus.WaitingForUser;
            NotificationsViewModelData.ProcessStatusMessage = "Waiting for your command.";
        }

        public void Preparing()
        {
            NotificationsViewModelData.NotificationStatus = eNotificationStatus.InProgress;
            NotificationsViewModelData.ProcessStatusMessage = "Please wait, preparing...";
        }


        public void SetAttentionMessage(string message)
        {
            System.Threading.Thread.Sleep(500);

            NotificationsViewModelData.NotificationStatus = eNotificationStatus.Attention;
            NotificationsViewModelData.ProcessStatusMessage = message;
        }



        public void BeforeStartProcess()
        {
            NotificationsViewModelData.NotificationStatus = eNotificationStatus.InProgress;
            NotificationsViewModelData.ProcessStatusMessage = "Prepare...";
        }

        public void AfterComplete()
        {
            System.Threading.Thread.Sleep(500);


            if (_processTrace.HasError)
            {
                NotificationsViewModelData.NotificationStatus = eNotificationStatus.Error;
                NotificationsViewModelData.ProcessStatusMessage = _processTrace.InstructionsMessage;
            }
            else
            {
                NotificationsViewModelData.NotificationStatus = eNotificationStatus.CompleteSuccessfully;
                NotificationsViewModelData.ProcessStatusMessage = "The process complete successfully";
            }

        }






        private void fireOnShowStatesLog(ProcessTrace processTrace)
        {
            if (OnShowStatesLog == null)
            {
                throw new Exception($"Bind method to 'OnShowStatesLog' event is mandatory");
            }

            StatesLogViewModel statesLogViewModel = new StatesLogViewModel(processTrace);

            OnShowStatesLog(this, statesLogViewModel);
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
