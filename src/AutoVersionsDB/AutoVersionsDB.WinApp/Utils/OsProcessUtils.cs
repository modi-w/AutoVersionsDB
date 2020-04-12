using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AutoVersionsDB.WinApp.Utils
{
    public class OsProcessUtils
    {
        public static void StartOsProcess(string filename)
        {
            var osProcess = new Process();
            osProcess.StartInfo = new ProcessStartInfo(filename)
            {
                UseShellExecute = true
            };
            osProcess.Start();
        }
    }
}
