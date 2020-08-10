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
        public ProcessTrace _processState;

        public NotificationsControl()
        {
            InitializeComponent();

        }

        public void OnNotificationStateChanged(ProcessTrace processState, NotificationStateItem notificationStateItem)
        {
            _processState = processState;

            ResolveColorAndImageByErrorStatus(true);

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

            ResolveColorAndImageByErrorStatus(false);

            if (!string.IsNullOrWhiteSpace(_processState.InstructionsMessage))
            {
                lblProcessStatusMessage.BeginInvoke((MethodInvoker)(() =>
                {
                    lblProcessStatusMessage.Text = _processState.InstructionsMessage;
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

        private void ResolveColorAndImageByErrorStatus(bool isInProcess)
        {
            if (isInProcess)
            {
            }
            else
            {
                if (_processState.HasError)
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

            if (_processState.HasError)
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

        private void PbStatus_Click(object sender, EventArgs e)
        {
            ShowMessageWindow(_processState);
        }

        private void ShowMessageWindow(ProcessTrace processResults)
        {
            using (MessageWindow messageWindow = new MessageWindow(processResults))
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
            ShowMessageWindow(_processState);
        }

        private void LblProcessStatusMessage_Click(object sender, EventArgs e)
        {
            ShowMessageWindow(_processState);
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
