using System;
using System.Windows.Forms;

namespace Bai4_1_Login
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Control.CheckForIllegalCrossThreadCalls = true;
            Application.Run(new FrmLogin());
        }
    }
}
