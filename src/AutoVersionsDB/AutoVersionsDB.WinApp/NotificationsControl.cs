using AutoVersionsDB.UI.Notifications;
using AutoVersionsDB.UI.StatesLog;
using AutoVersionsDB.WinApp.Properties;
using AutoVersionsDB.WinApp.Utils;
using Ninject;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoVersionsDB.WinApp
{
    public partial class NotificationsControl : UserControlNinjectBase
    {
        [Inject]
        public INotificationsViewModel ViewModel { get; set; }



        public NotificationsControl()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                if (!ViewModel.IsEventsBinded)
                {
                    ViewModel.OnShowStatesLog += ViewModel_OnShowStatesLog; 

                    ViewModel.IsEventsBinded = true;
                }

                ViewModel.NotificationsViewModelData.PropertyChanged += ViewModel_PropertyChanged;
                SetDataBindings();

            }

        }

        private void ViewModel_OnShowStatesLog(object sender, StatesLogViewModelEventArgs e)
        {
            using (StatesLogView statesLogView = new StatesLogView(e.StatesLogViewModel))
            {
                statesLogView.ShowDialog();
            }
        }

      

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModel.NotificationsViewModelData.StatusImageType):

                    StatusImageTypeChanged();
                    break;

                default:
                    break;
            }
        }


        private void SetDataBindings()
        {
            lblProcessStatusMessage.DataBindings.Clear();
            lblProcessStatusMessage.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblProcessStatusMessage,
                    nameof(lblProcessStatusMessage.Text),
                    ViewModel.NotificationsViewModelData,
                    nameof(ViewModel.NotificationsViewModelData.ProcessStatusMessage)
                    )
                );
            lblProcessStatusMessage.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblProcessStatusMessage,
                    nameof(lblProcessStatusMessage.ForeColor),
                    ViewModel.NotificationsControls,
                    nameof(ViewModel.NotificationsControls.ProcessStatusMessageColor)
                    )
                );

            pbStatus.DataBindings.Clear();
            pbStatus.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    pbStatus,
                    nameof(pbStatus.Visible),
                    ViewModel.NotificationsControls,
                    nameof(ViewModel.NotificationsControls.StatusImageVisible)
                    )
                );

            btnProcessLog.DataBindings.Clear();
            btnProcessLog.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    btnProcessLog,
                    nameof(btnProcessLog.Visible),
                    ViewModel.NotificationsControls,
                    nameof(ViewModel.NotificationsControls.BtnProcessLogVisible)
                    )
                );
            lblProcessLog.DataBindings.Clear();
            lblProcessLog.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblProcessLog,
                    nameof(lblProcessLog.Visible),
                    ViewModel.NotificationsControls,
                    nameof(ViewModel.NotificationsControls.BtnProcessLogVisible)
                    )
                );

        }


        private void StatusImageTypeChanged()
        {
            switch (ViewModel.NotificationsViewModelData.StatusImageType)
            {
                case StatusImageType.Spinner:

                    pbStatus.BeginInvoke((MethodInvoker)(() =>
                    {
                        pbStatus.Image = Resources.Spinner3_32;
                    }));
                    break;

                case StatusImageType.Warning:

                    pbStatus.BeginInvoke((MethodInvoker)(() =>
                    {
                        pbStatus.Image = Resources.warning_32;
                    }));
                    break;

                case StatusImageType.Error:

                    pbStatus.BeginInvoke((MethodInvoker)(() =>
                    {
                        pbStatus.Image = Resources.StopIcon_32;
                    }));
                    break;

                case StatusImageType.Succeed:

                    pbStatus.BeginInvoke((MethodInvoker)(() =>
                    {
                        pbStatus.Image = Resources.succeed;
                    }));
                    break;

            }


        }




        private void PbStatus_Click(object sender, EventArgs e)
        {
            if (ViewModel.HasProcessTrace)
            {
                ViewModel.ShowStatesLogViewCommand.Execute();
            }
        }



        private void LblPrecents_Click(object sender, EventArgs e)
        {
            if (ViewModel.HasProcessTrace)
            {
                ViewModel.ShowStatesLogViewCommand.Execute();
            }
        }

        private void LblProcessStatusMessage_Click(object sender, EventArgs e)
        {
            if (ViewModel.HasProcessTrace)
            {
                ViewModel.ShowStatesLogViewCommand.Execute();
            }
        }

        private void BtnProcessLog_Click(object sender, EventArgs e)
        {
            if (ViewModel.HasProcessTrace)
            {
                ViewModel.ShowStatesLogViewCommand.Execute();
            }
        }
        private void LblProcessLog_Click(object sender, EventArgs e)
        {
            if (ViewModel.HasProcessTrace)
            {
                ViewModel.ShowStatesLogViewCommand.Execute();
            }
        }




        #region Dispose

        // To detect redundant calls
        private bool _disposed;

        // Protected implementation of Dispose pattern.
        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects).
                //AutoVersionsDBAPI.Dispose();

                if (components != null)
                {
                    components.Dispose();
                }
            }

            _disposed = true;
            // Call base class implementation.
            base.Dispose(disposing);
        }

        #endregion

    }
}
