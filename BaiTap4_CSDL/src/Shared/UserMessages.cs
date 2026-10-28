using System.Windows.Forms;

namespace BaiTap4.Shared
{
    public interface IUserMessages
    {
        void Show(IWin32Window owner, string text, string title, MessageBoxIcon icon);
    }
    public sealed class DialogMessages : IUserMessages
    {
        public void Show(IWin32Window owner, string text, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(owner, text, title, MessageBoxButtons.OK, icon);
        }
    }
}
