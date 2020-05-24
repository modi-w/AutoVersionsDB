using System;
using System.Windows.Forms;

namespace AutoVersionsDB.WinApp
{
    public partial class TextInputWindow : Form
    {
        public string ResultText { get; private set; }

        public bool IsApply { get; private set; }

        public TextInputWindow(string message)
        {
            InitializeComponent();

            lblMessage.Text = message;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            IsApply = false;

            Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ResultText = tbInput.Text;
            IsApply = true;

            Close();
        }
    }
}
