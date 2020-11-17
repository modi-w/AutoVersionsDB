using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AutoVersionsDB.UI.Main
{

    public class MainControls : INotifyPropertyChanged
    {

        private bool _btnChooseProjectVisible;
        public bool BtnChooseProjectVisible
        {
            get => _btnChooseProjectVisible;
            set => SetField(ref _btnChooseProjectVisible, value);
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
