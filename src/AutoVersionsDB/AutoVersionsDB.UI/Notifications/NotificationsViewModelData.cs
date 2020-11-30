using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoVersionsDB.UI.Notifications
{
    public class NotificationsViewModelData : INotifyPropertyChanged
    {

        private NotificationStatus _notificationStatus;
        public NotificationStatus NotificationStatus
        {
            get => _notificationStatus;
            set =>
                //Console.WriteLine($"NotificationStatus Changed: {_notificationStatus} -> {value}. StackTrace: {Environment.StackTrace}");
                SetField(ref _notificationStatus, value);
        }


        private string _processStatusMessage;
        public string ProcessStatusMessage
        {
            get => _processStatusMessage;
            set => SetField(ref _processStatusMessage, value);
        }



        private StatusImageType _statusImageType;
        public StatusImageType StatusImageType
        {
            get => _statusImageType;
            set => SetField(ref _statusImageType, value);
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
