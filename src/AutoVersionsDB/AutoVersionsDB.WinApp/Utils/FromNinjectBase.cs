﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoVersionsDB.WinApp.Utils
{
    public class FromNinjectBase : Form
    {
        public FromNinjectBase()
        {
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                NinjectUtils_Winform.NinjectKernelContainer.Inject(this);
            }
        }
    }
}