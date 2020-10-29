using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AutoVersionsDB.UI.EditProject
{

    public class EditProjectControls : INotifyPropertyChanged
    {



        private bool _imgErrorVisible;
        public bool ImgErrorVisible
        {
            get => _imgErrorVisible;
            set
            {
                SetField(ref _imgErrorVisible, value);
            }
        }
        private bool _imgValidVisible;
        public bool ImgValidVisible
        {
            get => _imgValidVisible;
            set
            {
                SetField(ref _imgValidVisible, value);
            }
        }


        private bool _btnNavToProcessVisible;
        public bool BtnNavToProcessVisible
        {
            get => _btnNavToProcessVisible;
            set
            {
                SetField(ref _btnNavToProcessVisible, value);
            }
        }
        private bool _btnNavToProcessEnabled;
        public bool BtnNavToProcessEnabled
        {
            get => _btnNavToProcessEnabled;
            set
            {
                SetField(ref _btnNavToProcessEnabled, value);
            }
        }



        private bool _btnSaveEnabled;
        public bool BtnSaveEnabled
        {
            get => _btnSaveEnabled;
            set
            {
                SetField(ref _btnSaveEnabled, value);
            }
        }
        //private bool _btnSaveVisible;
        //public bool BtnSaveVisible
        //{
        //    get => _btnSaveVisible;
        //    set
        //    {
        //        SetField(ref _btnSaveVisible, value);
        //    }
        //}


        private bool _btnEditIdEnabled;
        public bool BtnEditIdEnabled
        {
            get => _btnEditIdEnabled;
            set
            {
                SetField(ref _btnEditIdEnabled, value);
            }
        }
        private bool _btnEditIdVisible;
        public bool BtnEditIdVisible //
        {
            get => _btnEditIdVisible;
            set
            {
                SetField(ref _btnEditIdVisible, value);
            }
        }

        private bool _btnSaveIdEnabled;
        public bool BtnSaveIdEnabled
        {
            get => _btnSaveIdEnabled;
            set
            {
                SetField(ref _btnSaveIdEnabled, value);
            }
        }
        private bool _btnSaveIdVisible;
        public bool BtnSaveIdVisible//
        {
            get => _btnSaveIdVisible;
            set
            {
                SetField(ref _btnSaveIdVisible, value);
            }
        }


        private bool _btnCancelEditIdVisible;
        public bool BtnCancelEditIdVisible//
        {
            get => _btnCancelEditIdVisible;
            set
            {
                SetField(ref _btnCancelEditIdVisible, value);
            }
        }
        private bool _btnCancelEditIdEnabled;
        public bool BtnCancelEditIdEnabled//
        {
            get => _btnCancelEditIdEnabled;
            set
            {
                SetField(ref _btnCancelEditIdEnabled, value);
            }
        }


        private bool _pnlDevEnvFoldersFieldsVisible;
        public bool PnlDevEnvFoldersFieldsVisible//
        {
            get => _pnlDevEnvFoldersFieldsVisible;
            set
            {
                SetField(ref _pnlDevEnvFoldersFieldsVisible, value);
            }
        }
        private bool _pnlDevEnvDeplyFolderVisible;
        public bool PnlDevEnvDeplyFolderVisible//
        {
            get => _pnlDevEnvDeplyFolderVisible;
            set
            {
                SetField(ref _pnlDevEnvDeplyFolderVisible, value);
            }
        }
        private bool _pnlDelEnvFieldsVisible;
        public bool PnlDelEnvFieldsVisible
        {
            get => _pnlDelEnvFieldsVisible;
            set
            {
                SetField(ref _pnlDelEnvFieldsVisible, value);
            }
        }





        private bool _cboConncectionTypeEnabled;
        public bool CboConncectionTypeEnabled
        {
            get => _cboConncectionTypeEnabled;
            set
            {
                SetField(ref _cboConncectionTypeEnabled, value);
            }
        }

        private bool _tbServerEnabled;
        public bool TbServerEnabled
        {
            get => _tbServerEnabled;
            set
            {
                SetField(ref _tbServerEnabled, value);
            }
        }
        private bool _tbDBNameEnabled;
        public bool TbDBNameEnabled
        {
            get => _tbDBNameEnabled;
            set
            {
                SetField(ref _tbDBNameEnabled, value);
            }
        }
        private bool _tbUsernameEnabled;
        public bool TbUsernameEnabled
        {
            get => _tbUsernameEnabled;
            set
            {
                SetField(ref _tbUsernameEnabled, value);
            }
        }
        private bool _tbPasswordEnabled;
        public bool TbPasswordEnabled
        {
            get => _tbPasswordEnabled;
            set
            {
                SetField(ref _tbPasswordEnabled, value);
            }
        }
        private bool _tbDevScriptsFolderPathEnabled;
        public bool TbDevScriptsFolderPathEnabled
        {
            get => _tbDevScriptsFolderPathEnabled;
            set
            {
                SetField(ref _tbDevScriptsFolderPathEnabled, value);
            }
        }
        private bool _tbDBBackupFolderEnabled;
        public bool TbDBBackupFolderEnabled
        {
            get => _tbDBBackupFolderEnabled;
            set
            {
                SetField(ref _tbDBBackupFolderEnabled, value);
            }
        }
        private bool _tbIdEnabled;
        public bool TbIdEnabled
        {
            get => _tbIdEnabled;
            set
            {
                SetField(ref _tbIdEnabled, value);
            }
        }
        private bool _rbDevEnvEnabled;
        public bool RbDevEnvEnabled
        {
            get => _rbDevEnvEnabled;
            set
            {
                SetField(ref _rbDevEnvEnabled, value);
            }
        }
        private bool _rbDelEnvEnabled;
        public bool RbDelEnvEnabled
        {
            get => _rbDelEnvEnabled;
            set
            {
                SetField(ref _rbDelEnvEnabled, value);
            }
        }
        private bool _tbDeployArtifactFolderPathEnabled;
        public bool TbDeployArtifactFolderPathEnabled
        {
            get => _tbDeployArtifactFolderPathEnabled;
            set
            {
                SetField(ref _tbDeployArtifactFolderPathEnabled, value);
            }
        }
        private bool _tbDeliveryArtifactFolderPathEnabled;
        public bool TbDeliveryArtifactFolderPathEnabled
        {
            get => _tbDeliveryArtifactFolderPathEnabled;
            set
            {
                SetField(ref _tbDeliveryArtifactFolderPathEnabled, value);
            }
        }



        private bool _tbProjectDescriptionEnabled;
        public bool TbProjectDescriptionEnabled
        {
            get => _tbProjectDescriptionEnabled;
            set
            {
                SetField(ref _tbProjectDescriptionEnabled, value);
            }
        }







        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            var a = PropertyChanged.GetInvocationList();

            foreach (var item in PropertyChanged.GetInvocationList())
            {

            }
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
