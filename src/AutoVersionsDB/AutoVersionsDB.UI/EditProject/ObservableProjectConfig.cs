﻿using AutoVersionsDB.Core.ConfigProjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AutoVersionsDB.UI.EditProject
{
    public class ObservableProjectConfig : INotifyPropertyChanged
    {
        public ProjectConfigItem ActualProjectConfig { get; }


        public ObservableProjectConfig(ProjectConfigItem _projectConfig)
        {
            ActualProjectConfig = _projectConfig;
        }

        public string Id
        {
            get => ActualProjectConfig.Id;
            set
            {
                ActualProjectConfig.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }


        public string Description
        {
            get => ActualProjectConfig.Description;
            set
            {
                ActualProjectConfig.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }


        public string DBType
        {
            get => ActualProjectConfig.DBType;
            set
            {
                ActualProjectConfig.DBType = value;
                OnPropertyChanged(nameof(DBType));
            }
        }


        public string Server
        {
            get => ActualProjectConfig.Server;
            set
            {
                ActualProjectConfig.Server = value;
                OnPropertyChanged(nameof(Server));
            }
        }

        public string DBName
        {
            get => ActualProjectConfig.DBName;
            set
            {
                ActualProjectConfig.DBName = value;
                OnPropertyChanged(nameof(DBName));
            }
        }


        public string Username
        {
            get => ActualProjectConfig.Username;
            set
            {
                ActualProjectConfig.Username = value;
                OnPropertyChanged(nameof(Username));
            }
        }


        public string Password
        {
            get => ActualProjectConfig.Password;
            set
            {
                ActualProjectConfig.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }


        public string BackupFolderPath
        {
            get => ActualProjectConfig.BackupFolderPath;
            set
            {
                ActualProjectConfig.BackupFolderPath = value;
                OnPropertyChanged(nameof(BackupFolderPath));
            }
        }

        public bool DevEnvironment
        {
            get => ActualProjectConfig.DevEnvironment;
            set
            {
                ActualProjectConfig.DevEnvironment = value;
                OnPropertyChanged(nameof(DevEnvironment));
            }
        }

        public bool DeliveryEnvironment
        {
            get => !ActualProjectConfig.DevEnvironment;
            set
            {
                ActualProjectConfig.DevEnvironment = !value;
                OnPropertyChanged(nameof(DeliveryEnvironment));
            }
        }

        public string DevScriptsBaseFolderPath
        {
            get => ActualProjectConfig.DevScriptsBaseFolderPath;
            set
            {
                ActualProjectConfig.DevScriptsBaseFolderPath = value;
                OnPropertyChanged(nameof(DevScriptsBaseFolderPath));
                OnPropertyChanged(nameof(IncrementalScriptsFolderPath));
                OnPropertyChanged(nameof(RepeatableScriptsFolderPath));
                OnPropertyChanged(nameof(DevDummyDataScriptsFolderPath));
            }
        }


        public string IncrementalScriptsFolderPath
        {
            get => ActualProjectConfig.IncrementalScriptsFolderPath;
        }
        public string RepeatableScriptsFolderPath
        {
            get => ActualProjectConfig.RepeatableScriptsFolderPath;
        }
        public string DevDummyDataScriptsFolderPath
        {
            get => ActualProjectConfig.DevDummyDataScriptsFolderPath;
        }



        public string DeployArtifactFolderPath
        {
            get => ActualProjectConfig.DeployArtifactFolderPath;
            set
            {
                ActualProjectConfig.DeployArtifactFolderPath = value;
                OnPropertyChanged(nameof(DeployArtifactFolderPath));
            }
        }


        public string DeliveryArtifactFolderPath
        {
            get => ActualProjectConfig.DeliveryArtifactFolderPath;
            set
            {
                ActualProjectConfig.DeliveryArtifactFolderPath = value;
                OnPropertyChanged(nameof(DeliveryArtifactFolderPath));
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
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }


        #endregion


    }
}