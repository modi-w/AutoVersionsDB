
/* Unmerged change from project 'AutoVersionsDB.WinApp'
Before:
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.WinApp.Utils;
using System;
using System.Drawing;
After:
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.UI.StatesLog;
using System.WinApp.Utils;
*/
using AutoVersionsDB.UI.StatesLog;
using AutoVersionsDB.ComponentModel;
using System;
/* Unmerged change from project 'AutoVersionsDB.WinApp'
Before:
using AutoVersionsDB.Helpers;
using System.ComponentModel;
using AutoVersionsDB.UI.StatesLog;
After:
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
*/


namespace AutoVersionsDB.WinApp
{
    public partial class StatesLogView : Form
    {
        public StatesLogViewModel ViewModel { get; }

        public StatesLogView(StatesLogViewModel viewModel)
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
