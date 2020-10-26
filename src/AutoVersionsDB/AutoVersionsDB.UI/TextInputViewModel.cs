//using AutoVersionsDB.Core;
//using AutoVersionsDB.Core.ConfigProjects;
//using AutoVersionsDB.Core.ConfigProjects.Processes.ProcessDefinitions;
//using AutoVersionsDB.NotificationableEngine;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Text;

//namespace AutoVersionsDB.UI
//{
//    public delegate void ShowTextInputEventHandler(object sender, string instructionMessageText);

//    public class TextInputViewModel : INotifyPropertyChanged
//    {

//        private string _instructionMessageText;
//        public string InstructionMessageText
//        {
//            get => _instructionMessageText;
//            set
//            {
//                SetField(ref _instructionMessageText, value);
//            }
//        }

//        private bool _isApply;
//        public bool IsApply
//        {
//            get => _isApply;
//            set
//            {
//                SetField(ref _isApply, value);
//            }
//        }

//        private string _resultText;
//        public string ResultText
//        {
//            get => _resultText;
//            set
//            {
//                SetField(ref _resultText, value);
//            }
//        }

       


//        public RelayCommand CancelCommand { get; private set; }
//        public RelayCommand ApplyCommand { get; private set; }



//        public TextInputViewModel()
//        {
//            CancelCommand = new RelayCommand(Cancel);
//            ApplyCommand = new RelayCommand(Apply);
//        }


//        public void Set(string instructionMessageText)
//        {
//            ResultText = "";
//            InstructionMessageText = instructionMessageText;
//        }


//        private void Cancel()
//        {
//            IsApply = false;
//        }


//        private void Apply()
//        {
//            IsApply = true;
//        }




//        #region INotifyPropertyChanged

//        public event PropertyChangedEventHandler? PropertyChanged;

//        protected void OnPropertyChanged(string? propertyName)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }

//        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
//        {
//            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
//            field = value;
//            OnPropertyChanged(propertyName);
//            return true;
//        }

//        #endregion


//    }
//}
