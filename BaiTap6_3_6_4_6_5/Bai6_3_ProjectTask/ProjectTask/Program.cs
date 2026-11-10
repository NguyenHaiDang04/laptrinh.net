using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using ProjectTask.GUI;
using ProjectTask.Tests;

namespace ProjectTask
{
    internal static class Program
    {
        [STAThread]
        private static int Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("vi-VN");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length == 2 && args[0] == "--verify")
                return SmokeTest.Run(args[1]);

            Application.Run(new MainForm());
            return 0;
        }
    }
}
