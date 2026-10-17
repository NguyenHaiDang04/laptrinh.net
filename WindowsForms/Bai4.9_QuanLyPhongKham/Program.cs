using Bai4_9_QuanLyPhongKham;
using System;
using System.Windows.Forms;

namespace Bai4_9_QuanLyPhongKham
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}