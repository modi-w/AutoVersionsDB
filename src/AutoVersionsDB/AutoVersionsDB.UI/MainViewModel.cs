using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AutoVersionsDB.UI
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ViewType _currentView;
        public ViewType CurrentView
    {
            get => _currentView;
            set => SetField(ref _currentView, value);
        }

        private bool _btnChooseProjectVisible;
        public bool BtnChooseProjectVisible
        {
            get => _btnChooseProjectVisible;
            set => SetField(ref _btnChooseProjectVisible, value);
        }

        public RelayCommand NavToChooseProjectCommand { get; private set; }


        private ViewRouter _viewRouter;
        public ViewRouter ViewRouter
        {
            get
            {
                return _viewRouter;
            }
            set
            {
                _viewRouter = value;
                SetRouteCommands();
            }
        }




        public MainViewModel()
        {
         
        }



        private void SetRouteCommands()
        {
            NavToChooseProjectCommand = new RelayCommand(_viewRouter.NavToChooseProject);
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
