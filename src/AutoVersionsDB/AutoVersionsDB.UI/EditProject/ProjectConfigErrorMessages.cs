using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AutoVersionsDB.UI.EditProject
{
    public class ProjectConfigErrorMessages : INotifyPropertyChanged
    {

        private string _idErrorMessage;
        public string IdErrorMessage
        {
            get => _idErrorMessage;
            set
            {
                SetField(ref _idErrorMessage, value);
            }
        }
        private string _dbTypeCodeErrorMessage;
        public string DBTypeCodeErrorMessage
        {
            get => _dbTypeCodeErrorMessage;
            set
            {
                SetField(ref _dbTypeCodeErrorMessage, value);
            }
        }
        //private string _serverErrorMessage;
        //public string ServerErrorMessage
        //{
        //    get => _serverErrorMessage;
        //    set
        //    {
        //        SetField(ref _serverErrorMessage, value);
        //    }
        //}
        private string _dbNameErrorMessage;
        public string DBNameErrorMessage
        {
            get => _dbNameErrorMessage;
            set
            {
                SetField(ref _dbNameErrorMessage, value);
            }
        }
        //private string _usernameErrorMessage;
        //public string UsernameErrorMessage
        //{
        //    get => _usernameErrorMessage;
        //    set
        //    {
        //        SetField(ref _usernameErrorMessage, value);
        //    }
        //}
        //private string _passwordErrorMessage;
        //public string PasswordErrorMessage
        //{
        //    get => _passwordErrorMessage;
        //    set
        //    {
        //        SetField(ref _passwordErrorMessage, value);
        //    }
        //}
        private string _backupFolderPathErrorMessage;
        public string BackupFolderPathErrorMessage
        {
            get => _backupFolderPathErrorMessage;
            set
            {
                SetField(ref _backupFolderPathErrorMessage, value);
            }
        }
        private string _devScriptsBaseFolderPathErrorMessage;
        public string DevScriptsBaseFolderPathErrorMessage
        {
            get => _devScriptsBaseFolderPathErrorMessage;
            set
            {
                SetField(ref _devScriptsBaseFolderPathErrorMessage, value);
            }
        }
        private string _deployArtifactFolderPathErrorMessage;
        public string DeployArtifactFolderPathErrorMessage
        {
            get => _deployArtifactFolderPathErrorMessage;
            set
            {
                SetField(ref _deployArtifactFolderPathErrorMessage, value);
            }
        }
        private string _deliveryArtifactFolderPathErrorMessage;
        public string DeliveryArtifactFolderPathErrorMessage
        {
            get => _deliveryArtifactFolderPathErrorMessage;
            set
            {
                SetField(ref _deliveryArtifactFolderPathErrorMessage, value);
            }
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
