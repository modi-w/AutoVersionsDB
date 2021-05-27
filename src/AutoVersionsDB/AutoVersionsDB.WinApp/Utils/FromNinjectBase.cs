using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoVersionsDB.WinApp.Utils
{
    public class FromNinjectBase : Form
    {
        public FromNinjectBase()
        {
            //if (this.DesignMode)
            //{
            //    DIConfig.Kernel.Inject(this);
            //}
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!this.DesignMode)
            {
                DIConfig.Kernel.Inject(this);
            }

        }
    }
}
