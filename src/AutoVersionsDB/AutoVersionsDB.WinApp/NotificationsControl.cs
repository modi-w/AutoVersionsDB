using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using AutoVersionsDB.Core;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.UI;
using AutoVersionsDB.WinApp.Properties;
using AutoVersionsDB.WinApp.Utils;
using Ninject;

namespace AutoVersionsDB.WinApp
{
    public partial class NotificationsControl : UserControlNinjectBase
    {
        [Inject]
        public NotificationsViewModel ViewModel { get; set; }



        public NotificationsControl()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                if (!ViewModel.IsEventsBinded)
                {
                    ViewModel.PropertyChanged += _viewModel_PropertyChanged;
                    ViewModel.OnShowStatesLog += ViewModel_OnShowStatesLog;

                    ViewModel.IsEventsBinded = true;
                }

                SetDataBindings();

            }

        }

        private void ViewModel_OnShowStatesLog(object sender, StatesLogViewModel statesLogViewModel)
        {
            StatesLogView statesLogView = new StatesLogView(statesLogViewModel);
            statesLogView.ShowDialog();
        }

        private void _viewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModel.StatusImageType):

                    StatusImageTypeChanged();
                    break;

                default:
                    break;
            }
        }


        private void SetDataBindings()
        {
            this.lblProcessStatusMessage.DataBindings.Clear();
            this.lblProcessStatusMessage.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblProcessStatusMessage,
                    nameof(lblProcessStatusMessage.Text),
                    ViewModel,
                    nameof(ViewModel.ProcessStatusMessage)
                    )
                );
            this.lblProcessStatusMessage.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lblProcessStatusMessage,
                    nameof(lblProcessStatusMessage.ForeColor),
                    ViewModel,
                    nameof(ViewModel.ProcessStatusMessageColor)
                    )
                );

            this.pbStatus.DataBindings.Clear();
            this.pbStatus.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    pbStatus,
                    nameof(pbStatus.Visible),
                    ViewModel,
                    nameof(ViewModel.StatusImageVisible)
                    )
                );


        }


        private void StatusImageTypeChanged()
        {
            switch (ViewModel.StatusImageType)
            {
                case NotificationsViewModel.eStatusImageType.Spinner:

                    pbStatus.BeginInvoke((MethodInvoker)(() =>
                    {
                        pbStatus.Image = Resources.Spinner3_32;
                    }));
                    break;

                case NotificationsViewModel.eStatusImageType.Warning:

                    //TODO: add image
                    //pbStatus.Image = Resources.W;
                    break;

                case NotificationsViewModel.eStatusImageType.Error:

                    pbStatus.BeginInvoke((MethodInvoker)(() =>
                    {
                        pbStatus.Image = Resources.StopIcon_32;
                    }));
                    break;

                case NotificationsViewModel.eStatusImageType.Succeed:

                    pbStatus.BeginInvoke((MethodInvoker)(() =>
                    {
                        pbStatus.Image = Resources.info2_32_32;
                    }));
                    break;

            }


        }




        private void PbStatus_Click(object sender, EventArgs e)
        {
            ViewModel.ShowStatesLogViewCommand.Execute();
        }



        private void LblPrecents_Click(object sender, EventArgs e)
        {
            ViewModel.ShowStatesLogViewCommand.Execute();
        }

        private void LblProcessStatusMessage_Click(object sender, EventArgs e)
        {
            ViewModel.ShowStatesLogViewCommand.Execute();
        }




        #region Dispose

        // To detect redundant calls
        private bool _disposed = false;

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
                //AutoVersionsDbAPI.Dispose();

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
