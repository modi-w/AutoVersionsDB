using Ninject;
using System;
using System.Windows.Forms;

namespace AutoVersionsDB.WinApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application . 
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += Application_ThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            NinjectUtils_Winform.CreateKernel();


            Main mainFrame = NinjectUtils_Winform.NinjectKernelContainer.Get<Main>();
            Application.Run(mainFrame);
        }


        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            //LogManagerObj.AddExceptionMessageToLog(e.Exception, "WinApp.Program.Application_ThreadException()");
            MessageBox.Show(e.Exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //LogManagerObj.AddExceptionMessageToLog(e.ExceptionObject as Exception, "WinApp.Program.CurrentDomain_UnhandledException");
            MessageBox.Show(e.ExceptionObject.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
