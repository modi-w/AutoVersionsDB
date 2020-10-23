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
    public partial class NotificationsControl : UserControl// UserControleNinjectBase
    {
        private NotificationsViewModel _viewModel { get; set; }
        public NotificationsViewModel ViewModel
        {
            get
            {
                return _viewModel;
            }
            set
            {
                _viewModel = value;

                ViewModel.PropertyChanged += _viewModel_PropertyChanged;
                SetDataBindings();
            }
        }



        public NotificationsControl()
        {
            InitializeComponent();

         
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
                nameof(lblProcessStatusMessage.Text),
                ViewModel,
                nameof(ViewModel.ProcessStatusMessage),
                false,
                DataSourceUpdateMode.OnPropertyChanged);
            this.lblProcessStatusMessage.DataBindings.Add(
                nameof(lblProcessStatusMessage.ForeColor),
                ViewModel,
                nameof(ViewModel.ProcessStatusMessageColor),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.pbStatus.DataBindings.Clear();
            this.pbStatus.DataBindings.Add(
                nameof(pbStatus.Visible),
                ViewModel,
                nameof(ViewModel.StatusImageVisible),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

        }


        private void StatusImageTypeChanged()
        {
            pbStatus.BeginInvoke((MethodInvoker)(() =>
            {
                switch (ViewModel.StatusImageType)
                {
                    case NotificationsViewModel.eStatusImageType.Spinner:

                        pbStatus.Image = Resources.Spinner3_32;
                        break;

                    case NotificationsViewModel.eStatusImageType.Warning:

                        //TODO: add image
                        //pbStatus.Image = Resources.W;
                        break;

                    case NotificationsViewModel.eStatusImageType.Error:

                        pbStatus.Image = Resources.StopIcon_32;
                        break;

                    case NotificationsViewModel.eStatusImageType.Succeed:

                        pbStatus.Image = Resources.info2_32_32;
                        break;

                }
            }));

        
        }




        private void PbStatus_Click(object sender, EventArgs e)
        {
            ShowMessageWindow();
        }

        private void ShowMessageWindow()
        {
            ViewModel.UpdateStatesLogViewModel();
            using (MessageWindow messageWindow = new MessageWindow(ViewModel.StatesLogViewModel))
            {
                messageWindow.ShowDialog();
            }
        }

        //private void resolveMessageLocation()
        //{
        //    lblProcessStatusMessage.BeginInvoke((MethodInvoker)(() =>
        //    {
        //        if (pbStatus.Visible)
        //        {

        //            lblProcessStatusMessage.Location = new Point(120, 17);
        //        }
        //        else
        //        {
        //            lblProcessStatusMessage.Location = new Point(10, 17);

        //        }
        //    }));


        //}

        private void LblPrecents_Click(object sender, EventArgs e)
        {
            ShowMessageWindow();
        }

        private void LblProcessStatusMessage_Click(object sender, EventArgs e)
        {
            ShowMessageWindow();
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
