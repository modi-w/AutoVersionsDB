﻿using System.ComponentModel;
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


        private bool _pnlInitDBVisible;
        public bool PnlInitDBVisible
        {
            get => _pnlInitDBVisible;
            set => SetField(ref _pnlInitDBVisible, value);
        }
        private bool _pnlInitDBEnabled;
        public bool PnlInitDBEnabled
        {
            get => _pnlInitDBEnabled;
            set => SetField(ref _pnlInitDBEnabled, value);
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

        private bool _pnlRestoreDBErrorVisible;
        public bool PnlRestoreDBErrorVisible
        {
            get => _pnlRestoreDBErrorVisible;
            set => SetField(ref _pnlRestoreDBErrorVisible, value);
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

        private bool _btnRecreateDBFromScratchMainVisible;
        public bool BtnRecreateDBFromScratchMainVisible
        {
            get => _btnRecreateDBFromScratchMainVisible;
            set => SetField(ref _btnRecreateDBFromScratchMainVisible, value);
        }

        private bool _btnRecreateDBbFromScratchSecondaryVisible;
        public bool BtnRecreateDBFromScratchSecondaryVisible
        {
            get => _btnRecreateDBbFromScratchSecondaryVisible;
            set => SetField(ref _btnRecreateDBbFromScratchSecondaryVisible, value);
        }

        
        private bool _btnVirtualDDDVisible;
        public bool BtnVirtualDDDVisible
        {
            get => _btnVirtualDDDVisible;
            set => SetField(ref _btnVirtualDDDVisible, value);
        }

        private bool _btnVirtualDDDEnabled;
        public bool BtnVirtualDDDEnabled
        {
            get => _btnVirtualDDDEnabled;
            set => SetField(ref _btnVirtualDDDEnabled, value);
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


        private string _lblIncNumOfExecutedText;
        public string LblIncNumOfExecutedText
        {
            get => _lblIncNumOfExecutedText;
            set => SetField(ref _lblIncNumOfExecutedText, value);
        }
        private string _lblIncNumOfVirtualText;
        public string LblIncNumOfVirtualText
        {
            get => _lblIncNumOfVirtualText;
            set => SetField(ref _lblIncNumOfVirtualText, value);
        }
        private string _lblIncNumOfChangedText;
        public string LblIncNumOfChangedText
        {
            get => _lblIncNumOfChangedText;
            set => SetField(ref _lblIncNumOfChangedText, value);
        }

        private string _lblRptNumOfExecutedText;
        public string LblRptNumOfExecutedText
        {
            get => _lblRptNumOfExecutedText;
            set => SetField(ref _lblRptNumOfExecutedText, value);
        }
        private string _lblRptNumOfVirtualText;
        public string LblRptNumOfVirtualText
        {
            get => _lblRptNumOfVirtualText;
            set => SetField(ref _lblRptNumOfVirtualText, value);
        }
        private string _lblRptNumOfChangedText;
        public string LblRptNumOfChangedText
        {
            get => _lblRptNumOfChangedText;
            set => SetField(ref _lblRptNumOfChangedText, value);
        }

        private string _lblDDDNumOfExecutedText;
        public string LblDDDNumOfExecutedText
        {
            get => _lblDDDNumOfExecutedText;
            set => SetField(ref _lblDDDNumOfExecutedText, value);
        }
        private string _lblDDDNumOfVirtualText;
        public string LblDDDNumOfVirtualText
        {
            get => _lblDDDNumOfVirtualText;
            set => SetField(ref _lblDDDNumOfVirtualText, value);
        }
        private string _lblDDDNumOfChangedText;
        public string LblDDDNumOfChangedText
        {
            get => _lblDDDNumOfChangedText;
            set => SetField(ref _lblDDDNumOfChangedText, value);
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

        private string _btnRecreateDBFromScratchTooltip;
        public string BtnRecreateDBFromScratchMainTooltip
        {
            get => _btnRecreateDBFromScratchTooltip;
            set => SetField(ref _btnRecreateDBFromScratchTooltip, value);
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
        private string _btnVirtualDDDTooltip;
        public string BtnVirtualDDDTooltip
        {
            get => _btnVirtualDDDTooltip;
            set => SetField(ref _btnVirtualDDDTooltip, value);
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
