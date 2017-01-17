using PInvoke;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BastardFat.ProcessSniffer.Processes.Sniffing
{
    public class BaseSniffer
    {
        public virtual List<IntPtr> GetAllProcesses()
        {
            var processes = new List<IntPtr>();

            User32.WNDENUMPROC WNDENUMPROC = (handle, handleParams) =>
            {
                string windowText;
                if (!TryGetWindowText(handle, out windowText)) return true;

                processes.Add(handle);
                return true;
            };

            try
            {
                using (var safeDesktopHandle = new User32.SafeDesktopHandle())
                    User32.EnumDesktopWindows(safeDesktopHandle, WNDENUMPROC, default(IntPtr));
            }
            finally
            {
                GC.KeepAlive(WNDENUMPROC);
            }

            return processes;
        }


        public bool TryGetWindowText(IntPtr handle, out string windowtext)
        {
            var chars = new char[1024];
            var findWindowTextLenght = User32.GetWindowText(handle, chars, chars.Length);
            if (findWindowTextLenght == 0)
            {
                windowtext = null;
                return false;
            }
            windowtext = new string(chars, 0, findWindowTextLenght);
            return true;
        }

        public string GetTitle(IntPtr handle)
        {
            string windowTitleText;
            if (!TryGetWindowText(handle, out windowTitleText))
                return null;

            return windowTitleText;
        }


    }
}
