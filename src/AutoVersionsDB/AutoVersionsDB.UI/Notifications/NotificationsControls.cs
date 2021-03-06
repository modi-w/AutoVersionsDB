﻿using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace AutoVersionsDB.UI.Notifications
{

    public class NotificationsControls : INotifyPropertyChanged
    {

        private Color _processStatusMessageColor;
        public Color ProcessStatusMessageColor
        {
            get => _processStatusMessageColor;
            set => SetField(ref _processStatusMessageColor, value);
        }



        private bool _statusImageVisible;
        public bool StatusImageVisible
        {
            get => _statusImageVisible;
            set => SetField(ref _statusImageVisible, value);
        }

        private bool _btnProcessLogVisible;
        public bool BtnProcessLogVisible
        {
            get => _btnProcessLogVisible;
            set => SetField(ref _btnProcessLogVisible, value);
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
