using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.WinApp.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;
using AutoVersionsDB.Helpers;


namespace AutoVersionsDB.WinApp
{
    public partial class MessageWindow : Form
    {

        public ProcessTrace ProcessTrace { get; private set; }

        public MessageWindow(ProcessTrace processTrace)
        {
            processTrace.ThrowIfNull(nameof(processTrace));


            InitializeComponent();

            ProcessTrace = processTrace;

            chkShowOnlyErrors.Checked = ProcessTrace.HasError;
            UpdateMessage();
        }


        private void ChkShowOnlyErrors_CheckedChanged(object sender, EventArgs e)
        {
            UpdateMessage();
        }


        private void UpdateMessage()
        {
            rtbMessages.Clear();

            if (ProcessTrace.HasError)
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
                string errorMessage = ProcessTrace.GetOnlyErrorsHistoryAsString();
                rtbMessages.AppendText(errorMessage);
            }
            else
            {
                string processMessage = ProcessTrace.GetAllHistoryAsString();
                rtbMessages.AppendText(processMessage);
            }
        }

    }
}
