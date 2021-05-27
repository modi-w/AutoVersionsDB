using AutoVersionsDB.UI.Notifications;
using AutoVersionsDB.UI.StatesLog;
using AutoVersionsDB.WinApp.Properties;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoVersionsDB.WinApp
{
    public partial class StatesLogView : Form
    {
        public StatesLogViewModel ViewModel { get; }

        public StatesLogView(StatesLogViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;

            //if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            //{
            //    ViewModel = viewModel;

            //    ViewModel.PropertyChanged += ViewModel_PropertyChanged;

            //    SetDataBindings();

            //    this.Load += StatesLogView_Load;
            //}

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;

            this.Load += StatesLogView_Load;

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);



            SetDataBindings();


        }


        private void StatesLogView_Load(object sender, System.EventArgs e)
        {
            StatusImageTypeChanged();
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
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

        private void StatusImageTypeChanged()
        {
            switch (ViewModel.StatusImageType)
            {
                case StatusImageType.Spinner:

                    imgMsgType.BeginInvoke((MethodInvoker)(() =>
                    {
                        imgMsgType.Image = Resources.Spinner3_32;
                    }));
                    break;

                case StatusImageType.Warning:

                    imgMsgType.BeginInvoke((MethodInvoker)(() =>
                    {
                        imgMsgType.Image = Resources.warning_32;
                    }));
                    break;

                case StatusImageType.Error:

                    imgMsgType.BeginInvoke((MethodInvoker)(() =>
                    {
                        imgMsgType.Image = Resources.StopIcon_32;
                    }));
                    break;

                case StatusImageType.Succeed:

                    imgMsgType.BeginInvoke((MethodInvoker)(() =>
                    {
                        imgMsgType.Image = Resources.succeed;
                    }));
                    break;

            }


        }


        private void SetDataBindings()
        {
            chkShowOnlyErrors.DataBindings.Clear();
            chkShowOnlyErrors.DataBindings.Add(
                nameof(chkShowOnlyErrors.Checked),
                ViewModel,
                nameof(ViewModel.ShowOnlyErrors),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            lblMessageType.DataBindings.Clear();
            lblMessageType.DataBindings.Add(
                nameof(lblMessageType.Text),
                ViewModel,
                nameof(ViewModel.Caption),
                false,
                DataSourceUpdateMode.OnPropertyChanged);
            lblMessageType.DataBindings.Add(
                nameof(lblMessageType.ForeColor),
                ViewModel,
                nameof(ViewModel.CaptionColor),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            rtbMessages.DataBindings.Clear();
            rtbMessages.DataBindings.Add(
                nameof(rtbMessages.Text),
                ViewModel,
                nameof(ViewModel.StatesLogText),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

        }




    }
}
