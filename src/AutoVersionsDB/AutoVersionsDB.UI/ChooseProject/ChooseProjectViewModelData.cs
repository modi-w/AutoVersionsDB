﻿using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AutoVersionsDB.UI.ChooseProject
{
    public class ChooseProjectViewModelData : INotifyPropertyChanged
    {
        private string _serchProjectText;
        public string SerchProjectText
        {
            get => _serchProjectText;
            set
            {
                SetField(ref _serchProjectText, value);
            }
        }

        private List<ProjectConfigItem> _filteredProjectList;
        public List<ProjectConfigItem> FilteredProjectList
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