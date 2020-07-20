using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AutoVersionsDB.Core;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.WinApp
{
    public partial class NotificationsControl : UserControl
    {
        private readonly AutoVersionsDbAPI _autoVersionsDbAPI = null;


        public NotificationsControl()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                _autoVersionsDbAPI = AutoVersionsDbAPI.Instance;

                AutoVersionsDbAPI.Instance.NotificationExecutersFactoryManager.NotifictionStatesHistoryManager.OnNotificationStateItemChanged += _notifictionStatesHistoryManager_OnNotificationStateItemChanged;

                this.Disposed += NotificationsControl_Disposed;
            }

        }

        private void _notifictionStatesHistoryManager_OnNotificationStateItemChanged(NotificationStateItem notificationStateItem)
        {
            resolveColorAndImageByErrorStatus(true);

            lblProcessStatusMessage.BeginInvoke((MethodInvoker)(() =>
            {
                if (!string.IsNullOrWhiteSpace(notificationStateItem.LowLevelInstructionsMessage))
                {
                    lblProcessStatusMessage.Text = notificationStateItem.LowLevelInstructionsMessage;
                }
                else
                {
                    lblProcessStatusMessage.Text = notificationStateItem.ToString();
                }
            }));
        }


        public void SetAttentionMessage(string message)
        {
            Clear();

            lblProcessStatusMessage.BeginInvoke((MethodInvoker)(() =>
            {
                lblProcessStatusMessage.Text = message;
                lblProcessStatusMessage.ForeColor = Color.DarkOrange;
            }));
        }


        public void Clear()
        {
            System.Threading.Thread.Sleep(500);

            pbStatus.BeginInvoke((MethodInvoker)(() =>
            {
                pbStatus.Visible = false;
                pbStatus.Cursor = Cursors.Default;

                //      resolveMessageLocation();
            }));


            lblProcessStatusMessage.BeginInvoke((MethodInvoker)(() =>
            {
                lblProcessStatusMessage.ForeColor = Color.Black;
                lblProcessStatusMessage.Text = "Waiting for your command.";
                lblProcessStatusMessage.Cursor = Cursors.Default;
            }));

        }







        public void PreparingMesage()
        {
            pbStatus.BeginInvoke((MethodInvoker)(() =>
            {
                pbStatus.Visible = false;
                pbStatus.Cursor = Cursors.Default;

                //          resolveMessageLocation();
            }));


            lblProcessStatusMessage.BeginInvoke((MethodInvoker)(() =>
            {
                lblProcessStatusMessage.ForeColor = Color.Black;
                lblProcessStatusMessage.Text = "Please wait, preparing...";
                lblProcessStatusMessage.Cursor = Cursors.Default;
            }));

        }


        public void BeforeStart()
        {
            //pbStatus.BeginInvoke((MethodInvoker)(() =>
            //{
            //    pbStatus.Visible = false;
            //    //   resolveMessageLocation();
            //}));

            pbStatus.BeginInvoke((MethodInvoker)(() =>
            {
                pbStatus.Cursor = Cursors.Hand;
                pbStatus.Visible = true;
                pbStatus.Image = Properties.Resources.Spinner3_32;
            }));


            lblProcessStatusMessage.BeginInvoke((MethodInvoker)(() =>
            {
                lblProcessStatusMessage.Visible = true;
                lblProcessStatusMessage.ForeColor = Color.Black;
                lblProcessStatusMessage.Text = "Prepare...";
            }));


        }

        public void AfterComplete()
        {
            System.Threading.Thread.Sleep(500);

            resolveColorAndImageByErrorStatus(false);

            if (!string.IsNullOrWhiteSpace(_autoVersionsDbAPI.InstructionsMessage))
            {
                lblProcessStatusMessage.BeginInvoke((MethodInvoker)(() =>
                {
                    lblProcessStatusMessage.Text = _autoVersionsDbAPI.InstructionsMessage;
                }));
            }
            else
            {
                lblProcessStatusMessage.BeginInvoke((MethodInvoker)(() =>
                {
                    lblProcessStatusMessage.Text = "The process complete successfully";
                }));
            }

        }

        private void resolveColorAndImageByErrorStatus(bool isInProcess)
        {
            if (isInProcess)
            {
            }
            else
            {
                if (_autoVersionsDbAPI.HasError)
                {
                    pbStatus.BeginInvoke((MethodInvoker)(() =>
                    {
                        pbStatus.Cursor = Cursors.Hand;
                        pbStatus.Visible = true;
                        pbStatus.Image = Properties.Resources.StopIcon_32;
                    }));
                }
                else
                {
                    pbStatus.BeginInvoke((MethodInvoker)(() =>
                    {
                        pbStatus.Visible = true;
                        pbStatus.Image = Properties.Resources.info2_32_32;
                        pbStatus.Cursor = Cursors.Hand;
                    }));
                }
            }

            if (_autoVersionsDbAPI.HasError)
            {

                lblProcessStatusMessage.BeginInvoke((MethodInvoker)(() =>
                {
                    lblProcessStatusMessage.ForeColor = Color.DarkRed;
                    lblProcessStatusMessage.Cursor = Cursors.Hand;
                }));
            }
            else
            {
                lblProcessStatusMessage.BeginInvoke((MethodInvoker)(() =>
                {
                    lblProcessStatusMessage.ForeColor = Color.Black;
                    lblProcessStatusMessage.Cursor = Cursors.Hand;
                }));
            }
        }

        private void pbStatus_Click(object sender, EventArgs e)
        {
            showMessageWindow();
        }

        private void showMessageWindow()
        {
            //if (NotifictionStatesHistoryManager != null)
            //{
            using (MessageWindow messageWindow = new MessageWindow(_autoVersionsDbAPI.NotificationExecutersFactoryManager.NotifictionStatesHistoryManager))
            {
                messageWindow.ShowDialog();
            }
            //  }

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

        private void lblPrecents_Click(object sender, EventArgs e)
        {
            showMessageWindow();
        }

        private void lblProcessStatusMessage_Click(object sender, EventArgs e)
        {
            showMessageWindow();
        }


        private void NotificationsControl_Disposed(object sender, EventArgs e)
        {
            AutoVersionsDbAPI.Instance.NotificationExecutersFactoryManager.NotifictionStatesHistoryManager.OnNotificationStateItemChanged -= _notifictionStatesHistoryManager_OnNotificationStateItemChanged;
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
                _autoVersionsDbAPI.Dispose();

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
