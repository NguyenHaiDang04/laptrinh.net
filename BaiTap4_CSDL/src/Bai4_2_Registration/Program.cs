using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace Bai4_2_Registration
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            var culture = (CultureInfo)CultureInfo.GetCultureInfo("vi-VN").Clone();
            culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            Thread.CurrentThread.CurrentCulture = culture;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Control.CheckForIllegalCrossThreadCalls = true;
            Application.Run(new FrmRegistration());
        }
    }
}
