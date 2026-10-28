using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using BaiTap4.Shared;

namespace Verification
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            if (args.Length == 1 && args[0] == "--probe")
            {
                using (var connection = new SqlConnection(DatabaseOptions.Default().ConnectionString()))
                using (var command = new SqlCommand("SELECT COUNT(*) FROM dbo.Registrations", connection))
                { connection.Open(); Environment.ExitCode = Convert.ToInt32(command.ExecuteScalar()) >= 2 ? 0 : 2; }
                return;
            }
            if (args.Length != 2 || args[0] != "--verify") { Environment.ExitCode = 2; return; }
            var culture = (CultureInfo)CultureInfo.GetCultureInfo("vi-VN").Clone(); culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            Thread.CurrentThread.CurrentCulture = culture;
            Application.EnableVisualStyles(); Application.SetCompatibleTextRenderingDefault(false); Control.CheckForIllegalCrossThreadCalls = true;
            Application.Run(new VerificationForm(args[1]));
        }
    }
}
