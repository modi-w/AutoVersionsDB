using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoVersionsDB.UI.Main
{
    public class MainViewModelData : INotifyPropertyChanged
    {
        private ViewType _currentView;
        public ViewType CurrentView
        {
            get => _currentView;
            set => SetField(ref _currentView, value);
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
