using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace WinFormsApp
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("vi-VN");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("vi-VN");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Control.CheckForIllegalCrossThreadCalls = true;
            if (args.Length == 2 && args[0] == "--verify")
            {
                Application.Run(new Verification.AppVerification(args[1]));
                return;
            }
            Application.Run(new Forms.frmMain());
        }
    }
}
