using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using dotPLC.Settings;

namespace dotPLC
{
    static class Program
    {
        static Mutex mutex;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Guid guid = GetGuid(Assembly.GetExecutingAssembly());
            string guidstr = "{" + guid.ToString() + "}";
            mutex = new Mutex(true, guidstr);
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmMain());
            }
            else
            {
                // send our Win32 message to make the currently running instance
                // jump on top of all the other windows
                //Dùng SendMessage thay vì PostMessage vì không nhận được ShowMe() trong frmMain khi form đang ở 
                //trạng thái this.ShowInTaskbar = false;
                NativeMethods.SendMessage(
                    (IntPtr)NativeMethods.HWND_BROADCAST,
                    NativeMethods.WM_SHOWME,
                    IntPtr.Zero,
                    IntPtr.Zero);
            }
        }
        static Guid GetGuid(Assembly assembly)
        {
            var guidAttribute = (GuidAttribute)assembly?.GetCustomAttributes(typeof(GuidAttribute), false).SingleOrDefault();
            if (Guid.TryParse(guidAttribute?.Value, out Guid guid)) { return guid; }
            return Guid.Empty;
        }
    }
}
