using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WordGameLib
{
    public class SendKeysUtil
    {
        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public static void SendKeyStrokes(string className, string windowName, string keys)
        {
            IntPtr winPtr = FindWindow(className, windowName);
            if (winPtr == IntPtr.Zero) throw new Exception("Target window not found.");

            SetForegroundWindow(winPtr);
            SendKeys.SendWait(keys);
            SendKeys.SendWait("{ENTER}");
        }

        public static void SendKeyStrokesToActiveWindow(string keys)
        {
            SendKeys.SendWait(keys);
            SendKeys.SendWait("{ENTER}");
        }
    }
}
