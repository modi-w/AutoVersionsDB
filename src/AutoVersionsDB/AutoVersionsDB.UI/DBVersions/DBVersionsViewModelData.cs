using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoVersionsDB.UI.DBVersions
{
    public class DBVersionsViewModelData : INotifyPropertyChanged
    {

        private ProjectConfigItem _projectConfig;
        public ProjectConfigItem ProjectConfig
        {
            get => _projectConfig;
            set => SetField(ref _projectConfig, value);
        }

        private ScriptFilesState _scriptFilesState;
        public ScriptFilesState ScriptFilesState
        {
            get => _scriptFilesState;
            set => SetField(ref _scriptFilesState, value);
        }



        private string _targetStateScriptFileName;
        public string TargetStateScriptFileName
        {
            get => _targetStateScriptFileName;
            set => SetField(ref _targetStateScriptFileName, value);
        }


        private IList<RuntimeScriptFileBase> _incrementalScriptFiles;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "<Pending>")]
        public IList<RuntimeScriptFileBase> IncrementalScriptFiles
        {
            get => _incrementalScriptFiles;
            set => SetField(ref _incrementalScriptFiles, value);
        }

        private IList<RuntimeScriptFileBase> _repeatableSScriptFiles;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "<Pending>")]
        public IList<RuntimeScriptFileBase> RepeatableScriptFiles
        {
            get => _repeatableSScriptFiles;
            set => SetField(ref _repeatableSScriptFiles, value);
        }

        private IList<RuntimeScriptFileBase> _devDummyDataSScriptFiles;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "<Pending>")]
        public IList<RuntimeScriptFileBase> DevDummyDataScriptFiles
        {
            get => _devDummyDataSScriptFiles;
            set => SetField(ref _devDummyDataSScriptFiles, value);
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
