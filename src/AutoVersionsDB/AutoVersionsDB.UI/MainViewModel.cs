using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AutoVersionsDB.UI
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private bool _btnChooseProjectVisible;
        public bool BtnChooseProjectVisible
        {
            get => _btnChooseProjectVisible;
            set => SetField(ref _btnChooseProjectVisible, value);
        }

        public ViewRouter ViewRouter { get; set; }

        public MainViewModel()
        {

        }


        public void NavToChooseProject()
        {
            ViewRouter.NavToChooseProject();
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
