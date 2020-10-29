using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AutoVersionsDB.UI.Main
{
    public class MainViewModel : INotifyPropertyChanged
    {
        
        public MainViewModelData MainViewModelData { get; }
        public MainControls MainControls { get; }
        

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


        public RelayCommand NavToChooseProjectCommand { get; private set; }




        public MainViewModel(MainViewModelData mainViewModelData,
                                MainControls mainControls)
        {
            MainViewModelData = mainViewModelData;
            MainControls = mainControls;
        }



        private void SetRouteCommands()
        {
            NavToChooseProjectCommand = new RelayCommand(_viewRouter.NavToChooseProject);
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
