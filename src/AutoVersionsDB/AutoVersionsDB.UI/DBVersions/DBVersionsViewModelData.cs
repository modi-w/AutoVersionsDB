using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AutoVersionsDB.UI.DBVersions
{
    public class DBVersionsViewModelData : INotifyPropertyChanged
    {
        
        private string _targetStateScriptFileName;
        public string TargetStateScriptFileName
        {
            get => _targetStateScriptFileName;
            set
            {
                SetField(ref _targetStateScriptFileName, value);
            }
        }


        private List<RuntimeScriptFileBase> _incrementalScriptFiles;
        public List<RuntimeScriptFileBase> IncrementalScriptFiles
        {
            get => _incrementalScriptFiles;
            set
            {
                SetField(ref _incrementalScriptFiles, value);
            }
        }

        private List<RuntimeScriptFileBase> _repeatableSScriptFiles;
        public List<RuntimeScriptFileBase> RepeatableScriptFiles
        {
            get => _repeatableSScriptFiles;
            set
            {
                SetField(ref _repeatableSScriptFiles, value);
            }
        }

        private List<RuntimeScriptFileBase> _devDummyDataSScriptFiles;
        public List<RuntimeScriptFileBase> DevDummyDataScriptFiles
        {
            get => _devDummyDataSScriptFiles;
            set
            {
                SetField(ref _devDummyDataSScriptFiles, value);
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
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }


        #endregion
    }
}
