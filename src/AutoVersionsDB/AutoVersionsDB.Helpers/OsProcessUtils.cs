using System.Diagnostics;

namespace AutoVersionsDB.Helpers
{
    public class OsProcessUtils
    {
        public virtual void StartOsProcess(string filename)
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
