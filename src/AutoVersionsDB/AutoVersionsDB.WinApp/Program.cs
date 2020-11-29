﻿using AutoVersionsDB.UI;
using Ninject;
using System;
using System.Windows.Forms;

namespace AutoVersionsDB.WinApp
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application . 
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.ThreadException += Application_ThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            NinjectUtils_Winform.CreateKernel();

            UIGeneralEvents.OnException += UIGeneralEvents_OnException;
            UIGeneralEvents.OnConfirm += UIGeneralEvents_OnConfirm;


            Main mainFrame = NinjectUtils_Winform.NinjectKernelContainer.Get<Main>();

            Application.Run(mainFrame);
        }

        private static void UIGeneralEvents_OnException(object sender, string exceptionMessage)
        {
            MessageBox.Show(exceptionMessage);
        }

        private static bool UIGeneralEvents_OnConfirm(object sender, string confirmMessage)
        {
            return MessageBox.Show(null, confirmMessage, "Pay Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes;
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
