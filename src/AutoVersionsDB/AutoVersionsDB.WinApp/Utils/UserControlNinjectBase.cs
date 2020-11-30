using System.ComponentModel;
using System.Windows.Forms;

namespace AutoVersionsDB.WinApp.Utils
{
    public class UserControlNinjectBase : UserControl
    {
        public UserControlNinjectBase()
        {
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                DIConfig.Kernel.Inject(this);
            }
        }
    }
}
