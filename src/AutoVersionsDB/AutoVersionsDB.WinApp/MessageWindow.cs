using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.WinApp.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.UI;
using System.ComponentModel;

namespace AutoVersionsDB.WinApp
{
    public partial class MessageWindow : Form
    {
        public StatesLogViewModel ViewModel { get; }

        public MessageWindow(StatesLogViewModel viewModel)
        {
            InitializeComponent();


            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                ViewModel = viewModel;

                ViewModel.PropertyChanged += _viewModel_PropertyChanged;

                SetDataBindings();
            }
        }

        private void _viewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                default:
                    break;
            }
        }


        private void SetDataBindings()
        {
            this.chkShowOnlyErrors.DataBindings.Clear();
            this.chkShowOnlyErrors.DataBindings.Add(
                nameof(chkShowOnlyErrors.Checked),
                ViewModel,
                nameof(ViewModel.ShowOnlyErrors),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.lblMessageType.DataBindings.Clear();
            this.lblMessageType.DataBindings.Add(
                nameof(lblMessageType.Text),
                ViewModel,
                nameof(ViewModel.Caption),
                false,
                DataSourceUpdateMode.OnPropertyChanged);
            this.lblMessageType.DataBindings.Add(
                nameof(lblMessageType.ForeColor),
                ViewModel,
                nameof(ViewModel.CaptionColor),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

            this.rtbMessages.DataBindings.Clear();
            this.rtbMessages.DataBindings.Add(
                nameof(rtbMessages.Text),
                ViewModel,
                nameof(ViewModel.StatesLogText),
                false,
                DataSourceUpdateMode.OnPropertyChanged);

        }




    }
}
