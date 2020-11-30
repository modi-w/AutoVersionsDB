using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoVersionsDB.UI.DBVersions
{

    public class DBVersionsControls : INotifyPropertyChanged
    {


        private string _lblProjectNameText;
        public string LblProjectNameText
        {
            get => _lblProjectNameText;
            set => SetField(ref _lblProjectNameText, value);
        }


        private bool _pnlMainActionsVisible;
        public bool PnlMainActionsVisible
        {
            get => _pnlMainActionsVisible;
            set => SetField(ref _pnlMainActionsVisible, value);
        }
        private bool _pnlMainActionsEnabled;
        public bool PnlMainActionsEnabled
        {
            get => _pnlMainActionsEnabled;
            set => SetField(ref _pnlMainActionsEnabled, value);
        }

        private bool _pnlSyncToSpecificStateVisible;
        public bool PnlSyncToSpecificStateVisible
        {
            get => _pnlSyncToSpecificStateVisible;
            set => SetField(ref _pnlSyncToSpecificStateVisible, value);
        }

        private bool _pnlMissingSystemTablesVisible;
        public bool PnlMissingSystemTablesVisible
        {
            get => _pnlMissingSystemTablesVisible;
            set => SetField(ref _pnlMissingSystemTablesVisible, value);
        }
        private bool _pnlMissingSystemTablesEnabled;
        public bool PnlMissingSystemTablesEnabled
        {
            get => _pnlMissingSystemTablesEnabled;
            set => SetField(ref _pnlMissingSystemTablesEnabled, value);
        }

        private bool _pnlSetDBStateManuallyVisible;
        public bool PnlSetDBStateManuallyVisible
        {
            get => _pnlSetDBStateManuallyVisible;
            set => SetField(ref _pnlSetDBStateManuallyVisible, value);
        }
        private bool _pnlSetDBStateManuallyEnabled;
        public bool PnlSetDBStateManuallyEnabled
        {
            get => _pnlSetDBStateManuallyEnabled;
            set => SetField(ref _pnlSetDBStateManuallyEnabled, value);
        }




        private bool _pnlRepeatableFilesVisible;
        public bool PnlRepeatableFilesVisible
        {
            get => _pnlRepeatableFilesVisible;
            set => SetField(ref _pnlRepeatableFilesVisible, value);
        }


        private bool _pnlDevDummyDataFilesVisible;
        public bool PnlDevDummyDataFilesVisible
        {
            get => _pnlDevDummyDataFilesVisible;
            set => SetField(ref _pnlDevDummyDataFilesVisible, value);
        }

        private bool _pnlRestoreDbErrorVisible;
        public bool PnlRestoreDbErrorVisible
        {
            get => _pnlRestoreDbErrorVisible;
            set => SetField(ref _pnlRestoreDbErrorVisible, value);
        }



        private bool _incrementalScriptsGridEnabled;
        public bool IncrementalScriptsGridEnabled
        {
            get => _incrementalScriptsGridEnabled;
            set => SetField(ref _incrementalScriptsGridEnabled, value);
        }

        private bool _gridToSelectTargetStateEnabled;
        public bool GridToSelectTargetStateEnabled
        {
            get => _gridToSelectTargetStateEnabled;
            set => SetField(ref _gridToSelectTargetStateEnabled, value);
        }


        private bool _btnRefreshEnable;
        public bool BtnRefreshEnable
        {
            get => _btnRefreshEnable;
            set => SetField(ref _btnRefreshEnable, value);
        }

        private bool _btnRecreateDbFromScratchMainVisible;
        public bool BtnRecreateDbFromScratchMainVisible
        {
            get => _btnRecreateDbFromScratchMainVisible;
            set => SetField(ref _btnRecreateDbFromScratchMainVisible, value);
        }

        private bool _btnRecreateDbFromScratchSecondaryVisible;
        public bool BtnRecreateDbFromScratchSecondaryVisible
        {
            get => _btnRecreateDbFromScratchSecondaryVisible;
            set => SetField(ref _btnRecreateDbFromScratchSecondaryVisible, value);
        }


        private bool _btnDeployVisible;
        public bool BtnDeployVisible
        {
            get => _btnDeployVisible;
            set => SetField(ref _btnDeployVisible, value);
        }




        private bool _btnShowHistoricalBackupsEnabled;
        public bool BtnShowHistoricalBackupsEnabled
        {
            get => _btnShowHistoricalBackupsEnabled;
            set => SetField(ref _btnShowHistoricalBackupsEnabled, value);
        }

        private bool _btnCreateNewIncrementalScriptFileEnabled;
        public bool BtnCreateNewIncrementalScriptFileEnabled
        {
            get => _btnCreateNewIncrementalScriptFileEnabled;
            set => SetField(ref _btnCreateNewIncrementalScriptFileEnabled, value);
        }

        private bool _btnCreateNewRepeatableScriptFileEnabled;
        public bool BtnCreateNewRepeatableScriptFileEnabled
        {
            get => _btnCreateNewRepeatableScriptFileEnabled;
            set => SetField(ref _btnCreateNewRepeatableScriptFileEnabled, value);
        }

        private bool _btnCreateNewDevDummyDataScriptFileEnabled;
        public bool BtnCreateNewDevDummyDataScriptFileEnabled
        {
            get => _btnCreateNewDevDummyDataScriptFileEnabled;
            set => SetField(ref _btnCreateNewDevDummyDataScriptFileEnabled, value);
        }




        private bool _lblColorTargetStateSquareVisible;
        public bool LblColorTargetStateSquareVisible
        {
            get => _lblColorTargetStateSquareVisible;
            set => SetField(ref _lblColorTargetStateSquareVisible, value);
        }

        private bool _lblColorTargetStateCaptionVisible;
        public bool LblColorTargetStateCaptionVisible
        {
            get => _lblColorTargetStateCaptionVisible;
            set => SetField(ref _lblColorTargetStateCaptionVisible, value);
        }



        private string _btnRefreshTooltip;
        public string BtnRefreshTooltip
        {
            get => _btnRefreshTooltip;
            set => SetField(ref _btnRefreshTooltip, value);
        }

        private string _btnRunSyncTooltip;
        public string BtnRunSyncTooltip
        {
            get => _btnRunSyncTooltip;
            set => SetField(ref _btnRunSyncTooltip, value);
        }

        private string _btnRecreateDbFromScratchTooltip;
        public string BtnRecreateDbFromScratchMainTooltip
        {
            get => _btnRecreateDbFromScratchTooltip;
            set => SetField(ref _btnRecreateDbFromScratchTooltip, value);
        }

        private string _btnDeployTooltip;
        public string BtnDeployTooltip
        {
            get => _btnDeployTooltip;
            set => SetField(ref _btnDeployTooltip, value);
        }

        private string _btnSetDBToSpecificStateTooltip;
        public string BtnSetDBToSpecificStateTooltip
        {
            get => _btnSetDBToSpecificStateTooltip;
            set => SetField(ref _btnSetDBToSpecificStateTooltip, value);
        }

        private string _btnVirtualExecutionTooltip;
        public string BtnVirtualExecutionTooltip
        {
            get => _btnVirtualExecutionTooltip;
            set => SetField(ref _btnVirtualExecutionTooltip, value);
        }

        private string _btnShowHistoricalBackupsTooltip;
        public string BtnShowHistoricalBackupsTooltip
        {
            get => _btnShowHistoricalBackupsTooltip;
            set => SetField(ref _btnShowHistoricalBackupsTooltip, value);
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
