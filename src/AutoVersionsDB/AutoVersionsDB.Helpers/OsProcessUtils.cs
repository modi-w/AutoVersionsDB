using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AutoVersionsDB.Helpers
{
    public static class OsProcessUtils
    {
        public static void StartOsProcess(string filename)
        {
            using (var osProcess = new Process())
            {
                osProcess.StartInfo = new ProcessStartInfo(filename)
                {
                    UseShellExecute = true
                };
                osProcess.Start();
            }

        }
    }
}
