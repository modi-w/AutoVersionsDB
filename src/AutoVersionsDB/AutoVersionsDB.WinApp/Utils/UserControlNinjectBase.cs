using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoVersionsDB.WinApp.Utils
{
    public class UserControlNinjectBase : UserControl
    {
        //private bool isDesignTime;
        //public override ISite Site
        //{
        //    get => base.Site;
        //    set
        //    {
        //        base.Site = value;

        //        this.isDesignTime = true;
        //    }
        //}
        //private bool init = false;

        public UserControlNinjectBase()
        {
            //if (this.isDesignTime || this.init)
            //{
            //    return;
            //}
            //this.init = true;

            //DIConfig.Kernel.Inject(this);
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
