using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;

namespace AutoVersionsDB.UI.Notifications
{
    public class NotificationsViewModelData : INotifyPropertyChanged
    {

        private eNotificationStatus _notificationStatus;
        public eNotificationStatus NotificationStatus
        {
            get => _notificationStatus;
            set
            {
                Console.WriteLine($"NotificationStatus Changed: {_notificationStatus} -> {value}. StackTrace: {Environment.StackTrace}");
                SetField(ref _notificationStatus, value);
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

       

        private eStatusImageType _statusImageType;
        public eStatusImageType StatusImageType
        {
            get => _statusImageType;
            set
            {
                SetField(ref _statusImageType, value);
            }
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
