using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ConfigProjects.Processes.ProcessDefinitions;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AutoVersionsDB.UI
{
    public class StatesLogViewModel : INotifyPropertyChanged
    {
        private ProcessTrace _processTrace;


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
            set
            {
                SetField(ref _caption, value);
            }
        }

        private Color _captionColor;
        public Color CaptionColor
        {
            get => _captionColor;
            set
            {
                SetField(ref _captionColor, value);
            }
        }

        private string _statesLogText;
        public string StatesLogText
        {
            get => _statesLogText;
            set
            {
                SetField(ref _statesLogText, value);
            }
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
            }
            else
            {
                Caption = "Process Log";
                CaptionColor = Color.Black;
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion


    }
}
