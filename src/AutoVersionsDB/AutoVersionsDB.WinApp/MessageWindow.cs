using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.WinApp.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AutoVersionsDB.WinApp
{
    public partial class MessageWindow : Form
    {

        public NotifictionStatesHistory NotifictionStatesHistoryManager { get; private set; }

        public MessageWindow(NotifictionStatesHistory notifictionStatesHistoryManager)
        {
            notifictionStatesHistoryManager.ThrowIfNull(nameof(notifictionStatesHistoryManager));


            InitializeComponent();

            NotifictionStatesHistoryManager = notifictionStatesHistoryManager;

            chkShowOnlyErrors.Checked = NotifictionStatesHistoryManager.HasError;
            updateMessage();
        }


        private void chkShowOnlyErrors_CheckedChanged(object sender, EventArgs e)
        {
            updateMessage();
        }


        private void updateMessage()
        {
            rtbMessages.Clear();

            if (NotifictionStatesHistoryManager.HasError)
            {
                Text = "Errors";
                lblMessageType.Text = "Errors";
               // imgMsgType.Image = Properties.Resources.error2_32_32;
                lblMessageType.ForeColor = Color.DarkRed;
            }
            else
            {
                Text = "Process Messages";
                lblMessageType.Text = "Process Messages";
             //   imgMsgType.Image = Properties.Resources.info2_32_32;
                lblMessageType.ForeColor = Color.Black;
            }

            if (chkShowOnlyErrors.Checked)
            {
                string errorMessage = NotifictionStatesHistoryManager.GetOnlyErrorsHistoryAsString();
                rtbMessages.AppendText(errorMessage);
            }
            else
            {
                string processMessage = NotifictionStatesHistoryManager.GetAllHistoryAsString();
                rtbMessages.AppendText(processMessage);
            }
        }

    }
}
