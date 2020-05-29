using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.Helpers
{
    public class SqlServerInstanceHelpers
    {
        public static void SetupLocalDb()
        {
            // Use a ProcessStartInfo object to provide a simple solution to create a new LocalDbInstance
            var _processInfo = new ProcessStartInfo("cmd.exe", "/c " + "sqllocaldb.exe create localtestdb 14.0 -s")
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            var _process = Process.Start(_processInfo);
            _process.WaitForExit();

            string _output = _process.StandardOutput.ReadToEnd();
            string _error = _process.StandardError.ReadToEnd();

            var _exitCode = _process.ExitCode;

            Console.WriteLine("output>>" + (String.IsNullOrEmpty(_output) ? "(none)" : _output));
            Console.WriteLine("error>>" + (String.IsNullOrEmpty(_error) ? "(none)" : _error));
            Console.WriteLine("ExitCode: " + _exitCode.ToString());
            _process.Close();
        }
    }
}
