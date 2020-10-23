using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AutoVersionsDB.UI
{


    public class NotificationsViewModel : INotifyPropertyChanged
    {
        public enum eNotificationStatus
        {
            None = 0,
            WaitingForUser = 1,
            InProgress = 2,
            Error = 3,
            CompleteSuccessfully = 4,
            Attention = 5,
        }

        public enum eStatusImageType
        {
            None =0,
            Spinner = 1,
            Warning = 2,
            Error = 3,
            Succeed = 4,
        }


        private ProcessTrace _processTrace;


        public StatesLogViewModel StatesLogViewModel { get; }


        private eNotificationStatus _notificationStatus;
        public eNotificationStatus NotificationStatus
        {
            get => _notificationStatus;
            set
            {
                SetField(ref _notificationStatus, value);
                NotificationStatusChanged();
            }
        }


        private string _processStatusMessage;
        public string ProcessStatusMessage
        {
            get => _processStatusMessage;
            set
            {
                SetField(ref _processStatusMessage, value);
            }
        }

        private Color _processStatusMessageColor;
        public Color ProcessStatusMessageColor
        {
            get => _processStatusMessageColor;
            set
            {
                SetField(ref _processStatusMessageColor, value);
            }
        }



        private bool _statusImageVisible;
        public bool StatusImageVisible
        {
            get => _statusImageVisible;
            set
            {
                SetField(ref _statusImageVisible, value);
            }
        }

        private eStatusImageType _statusImageType;
        public eStatusImageType StatusImageType
        {
            get => _statusImageType;
            set
            {
                SetField(ref _statusImageType, value);
            }
        }



        public NotificationsViewModel(StatesLogViewModel statesLogViewModel)
        {
            StatesLogViewModel = statesLogViewModel;
        }




        private void NotificationStatusChanged()
        {
            switch (NotificationStatus)
            {
                case eNotificationStatus.WaitingForUser:

                    StatusImageVisible = false;

                    ProcessStatusMessageColor = Color.Black;

                    break;

                case eNotificationStatus.InProgress:

                    StatusImageVisible = true;
                    StatusImageType = eStatusImageType.Spinner;

                    ProcessStatusMessageColor = Color.Black;
                    break;


                case eNotificationStatus.Error:

                    StatusImageVisible = true;
                    StatusImageType = eStatusImageType.Error;

                    ProcessStatusMessageColor = Color.DarkRed;
                    break;

                case eNotificationStatus.CompleteSuccessfully:

                    StatusImageVisible = true;
                    StatusImageType = eStatusImageType.Succeed;

                    ProcessStatusMessageColor = Color.Black;
                    break;

                case eNotificationStatus.Attention:

                    StatusImageVisible = true;
                    StatusImageType = eStatusImageType.Warning;

                    ProcessStatusMessageColor = Color.DarkOrange;
                    break;

                case eNotificationStatus.None:
                default:
                    break;
            }
        }



        public void OnNotificationStateChanged(ProcessTrace processTrace, StepNotificationState notificationStateItem)
        {
            _processTrace = processTrace;

            NotificationStatus = eNotificationStatus.InProgress;

            if (!string.IsNullOrWhiteSpace(notificationStateItem.LowLevelInstructionsMessage))
            {
                ProcessStatusMessage = notificationStateItem.LowLevelInstructionsMessage;
            }
            else
            {
                ProcessStatusMessage = notificationStateItem.ToString();
            }
        }



        public void WaitingForUser()
        {
            NotificationStatus = eNotificationStatus.WaitingForUser;
            ProcessStatusMessage = "Waiting for your command.";
        }

        public void Preparing()
        {
            NotificationStatus = eNotificationStatus.InProgress;
            ProcessStatusMessage = "Please wait, preparing...";
        }


        public void SetAttentionMessage(string message)
        {
            NotificationStatus = eNotificationStatus.Attention;
            ProcessStatusMessage = message;
        }



        public void BeforeStartProcess()
        {
            NotificationStatus = eNotificationStatus.InProgress;
            ProcessStatusMessage = "Prepare...";
        }

        public void AfterComplete()
        {
            System.Threading.Thread.Sleep(500);


            if (_processTrace.HasError)
            {
                NotificationStatus = eNotificationStatus.Error;
                ProcessStatusMessage = _processTrace.InstructionsMessage;
            }
            else
            {
                NotificationStatus = eNotificationStatus.CompleteSuccessfully;
                ProcessStatusMessage = "The process complete successfully";
            }

        }



        public void UpdateStatesLogViewModel()
        {
            StatesLogViewModel.SetProcessTrace(_processTrace);
        }







        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion


    }
}
