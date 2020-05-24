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
        private AutoVersionsDbAPI _autoVersionsDbAPI = null;


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
            resolveColorAndImageByErrorStatus();

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
            imgBtnStatus.BeginInvoke((MethodInvoker)(() =>
            {
                imgBtnStatus.Visible = false;
                imgBtnStatus.Cursor = Cursors.Default;

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
            imgBtnStatus.BeginInvoke((MethodInvoker)(() =>
            {
                imgBtnStatus.Visible = false;
                imgBtnStatus.Cursor = Cursors.Default;

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
            imgBtnStatus.BeginInvoke((MethodInvoker)(() =>
            {
                imgBtnStatus.Visible = false;
                //   resolveMessageLocation();
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
            resolveColorAndImageByErrorStatus();

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

        private void resolveColorAndImageByErrorStatus()
        {
            if (_autoVersionsDbAPI.HasError)
            {
                imgBtnStatus.BeginInvoke((MethodInvoker)(() =>
                {
                    imgBtnStatus.Cursor = Cursors.Hand;
                    imgBtnStatus.Visible = true;
                    imgBtnStatus.Image = Properties.Resources.error2_32_32;
                }));

                lblProcessStatusMessage.BeginInvoke((MethodInvoker)(() =>
                {
                    lblProcessStatusMessage.ForeColor = Color.DarkRed;
                    lblProcessStatusMessage.Cursor = Cursors.Hand;
                }));
            }
            else
            {
                imgBtnStatus.BeginInvoke((MethodInvoker)(() =>
                {
                    imgBtnStatus.Visible = true;
                    imgBtnStatus.Image = Properties.Resources.info2_32_32;
                    imgBtnStatus.Cursor = Cursors.Hand;
                }));

                lblProcessStatusMessage.BeginInvoke((MethodInvoker)(() =>
                {
                    lblProcessStatusMessage.ForeColor = Color.Black;
                    lblProcessStatusMessage.Cursor = Cursors.Hand;
                }));
            }
        }

        private void imgBtnStatus_Click(object sender, EventArgs e)
        {
            showMessageWindow();
        }

        private void showMessageWindow()
        {
            //if (NotifictionStatesHistoryManager != null)
            //{
            MessageWindow messageWindow = new MessageWindow(_autoVersionsDbAPI.NotificationExecutersFactoryManager.NotifictionStatesHistoryManager);
            messageWindow.ShowDialog();
            //  }

        }

        //private void resolveMessageLocation()
        //{
        //    lblProcessStatusMessage.BeginInvoke((MethodInvoker)(() =>
        //    {
        //        if (imgBtnStatus.Visible)
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


    }
}
