using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AutoVersionsDB.UI
{
    public class ChooseProjectViewModel : INotifyPropertyChanged
    {
        private readonly ProjectConfigsAPI _projectConfigsAPI;

        private List<ProjectConfigItem> _allProjectsList;


        public const string SearchPlaceHolderText = "Search Project...";

        internal ViewRouter ViewRouter { get; set; }






        private string _serchProjectText;
        public string SerchProjectText
        {
            get => _serchProjectText;
            set
            {
                SetField(ref _serchProjectText, value);
                RefreshProjectList();
            }
        }

        public bool IsSerchProjectTextEmpty => string.IsNullOrWhiteSpace(SerchProjectText) || SerchProjectText == SearchPlaceHolderText;


        private List<ProjectConfigItem> _filteredProjectList;
        public List<ProjectConfigItem> FilteredProjectList
        {
            get => _filteredProjectList;
            set => SetField(ref _filteredProjectList, value);
        }



        public ChooseProjectViewModel(ProjectConfigsAPI projectConfigsAPI)
        {
            _projectConfigsAPI = projectConfigsAPI;

            SerchProjectText = SearchPlaceHolderText;
            RefreshProjectList();
        }



        public void RefreshProjectList()
        {
            string searchText = "";
            if (SerchProjectText == SearchPlaceHolderText)
            {
                searchText = "";
            }
            else
            {
                searchText = SerchProjectText;
            }


            _allProjectsList = _projectConfigsAPI.GetProjectsList();

            FilteredProjectList =
                _allProjectsList
                .Where(e => string.IsNullOrWhiteSpace(searchText)
                            || e.Id.Trim().ToUpperInvariant().Contains(searchText.Trim().ToUpperInvariant())
                            || e.Description.Trim().ToUpperInvariant().Contains(searchText.Trim().ToUpperInvariant()))
                .OrderBy(e => e.Id)
                .ToList();
        }


        public void ResolveSerchProjectTextPlaceHolder()
        {
            if (string.IsNullOrWhiteSpace(SerchProjectText))
            {
                SerchProjectText = SearchPlaceHolderText;
            }
        }

        public void ResolveSerchProjectTextOnFocus()
        {
            if (SerchProjectText == SearchPlaceHolderText)
            {
                SerchProjectText = "";
            }
        }


        public void NavToCreateNewProjectConfig()
        {
            ViewRouter.NavToEditProjectConfig(null);
        }


        public void NavToEditProjectConfig(string id)
        {
            ViewRouter.NavToEditProjectConfig(id);
        }



        public void NavToDBVersions(string id)
        {
            ViewRouter.NavToDBVersions(id);
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
