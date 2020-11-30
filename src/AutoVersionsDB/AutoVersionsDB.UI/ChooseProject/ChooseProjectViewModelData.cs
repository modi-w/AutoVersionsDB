using AutoVersionsDB.Core.ConfigProjects;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoVersionsDB.UI.ChooseProject
{
    public class ChooseProjectViewModelData : INotifyPropertyChanged
    {
        private string _serchProjectText;
        public string SerchProjectText
        {
            get => _serchProjectText;
            set => SetField(ref _serchProjectText, value);
        }

        private IList<ProjectConfigItem> _filteredProjectList;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "<Pending>")]
        public IList<ProjectConfigItem> FilteredProjectList
        {
            get => _filteredProjectList;
            set => SetField(ref _filteredProjectList, value);
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
