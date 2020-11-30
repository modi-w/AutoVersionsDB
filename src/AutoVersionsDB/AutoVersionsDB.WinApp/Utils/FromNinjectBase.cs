using System.ComponentModel;
using System.Windows.Forms;

namespace AutoVersionsDB.WinApp.Utils
{
    public class FromNinjectBase : Form
    {
        public FromNinjectBase()
        {
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                DIConfig.Kernel.Inject(this);
            }
        }
    }
}
