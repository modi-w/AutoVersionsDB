using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.Helpers
{
    //https://www.sqlshack.com/how-to-connect-and-use-microsoft-sql-server-express-localdb/
    //https://knowledge-base.havit.eu/2018/09/04/sql-localdb-upgrade-to-2017-14-0-1000/
    //https://stackoverflow.com/questions/57811000/patching-sql-server-localdb
    //https://stackoverflow.com/questions/50234050/create-newest-version-of-sqllocaldb-but-version-mismatch

    //https://www.google.com/search?ei=T5HLXrb7DNGLmwXOl5uwDQ&q=sqllocaldb+CREATE+FILE+encountered+operating+system+error+5%28Access+is+denied.%29+while+attempting+to+open+or+create+the+physical+file+&oq=sqllocaldb+CREATE+FILE+encountered+operating+system+error+5%28Access+is+denied.%29+while+attempting+to+open+or+create+the+physical+file+&gs_lcp=CgZwc3ktYWIQAzIECAAQRzIECAAQRzIECAAQRzIECAAQRzIECAAQRzIECAAQRzIECAAQRzIECAAQR1DjP1jhRmCQSGgAcAF4AIABAIgBAJIBAJgBAKABAaABAqoBB2d3cy13aXo&sclient=psy-ab&ved=0ahUKEwj23vuP287pAhXRxaYKHc7LBtYQ4dUDCAw&uact=5
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
