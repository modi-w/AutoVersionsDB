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



        private string _targetIncScriptFileName;
        public string TargetIncScriptFileName
        {
            get => _targetIncScriptFileName;
            set => SetField(ref _targetIncScriptFileName, value);
        }
        //private string _targetRptScriptFileName;
        //public string TargetRptScriptFileName
        //{
        //    get => _targetRptScriptFileName;
        //    set => SetField(ref _targetRptScriptFileName, value);
        //}
        //private string _targetDDDScriptFileName;
        //public string TargetDDDScriptFileName
        //{
        //    get => _targetDDDScriptFileName;
        //    set => SetField(ref _targetDDDScriptFileName, value);
        //}



        private IList<RuntimeScriptFile> _incrementalScriptFiles;

        public IList<RuntimeScriptFile> IncrementalScriptFiles
        {
            get => _incrementalScriptFiles;
            set => SetField(ref _incrementalScriptFiles, value);
        }

        private IList<RuntimeScriptFile> _repeatableSScriptFiles;
        public IList<RuntimeScriptFile> RepeatableScriptFiles
        {
            get => _repeatableSScriptFiles;
            set => SetField(ref _repeatableSScriptFiles, value);
        }

        private IList<RuntimeScriptFile> _devDummyDataSScriptFiles;
        public IList<RuntimeScriptFile> DevDummyDataScriptFiles
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
