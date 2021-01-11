using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.UI.Notifications;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace AutoVersionsDB.UI.StatesLog
{
    public class StatesLogViewModel : INotifyPropertyChanged
    {
        private readonly ProcessTrace _processTrace;


        private bool _showOnlyErrors;
        public bool ShowOnlyErrors
        {
            get => _showOnlyErrors;
            set
            {
                SetField(ref _showOnlyErrors, value);
                UpdateStatesLogText();
            }
        }

        private string _caption;
        public string Caption
        {
            get => _caption;
            set => SetField(ref _caption, value);
        }

        private Color _captionColor;
        public Color CaptionColor
        {
            get => _captionColor;
            set => SetField(ref _captionColor, value);
        }

        private string _statesLogText;
        public string StatesLogText
        {
            get => _statesLogText;
            set => SetField(ref _statesLogText, value);
        }

        private StatusImageType _statusImageType;
        public StatusImageType StatusImageType
        {
            get => _statusImageType;
            set => SetField(ref _statusImageType, value);
        }




        public StatesLogViewModel(ProcessTrace processTrace)
        {
            _processTrace = processTrace;

            UpdateCaption();

            ShowOnlyErrors = _processTrace.HasError;
        }




        private void UpdateCaption()
        {
            if (_processTrace.HasError)
            {
                Caption = "Error";
                CaptionColor = Color.DarkRed;
                StatusImageType = StatusImageType.Error;
            }
            else
            {
                Caption = "Process Log";
                CaptionColor = Color.Black;
                StatusImageType = StatusImageType.Succeed;
            }
        }


        private void UpdateStatesLogText()
        {
            if (ShowOnlyErrors)
            {
                StatesLogText = _processTrace.GetOnlyErrorsStatesLogAsString();
            }
            else
            {
                StatesLogText = _processTrace.GetAllStatesLogAsString();
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
