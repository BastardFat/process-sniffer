using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PInvoke;

namespace BastardFat.ProcessSniffer.Processes.Sniffing
{
    public class DesktopSniffer : BaseSniffer
    {

        public override List<IntPtr> GetAllProcesses() =>
            base.GetAllProcesses()
                .Where(handle => User32.IsWindow(handle) &&
                                 User32.IsWindowVisible(handle)).ToList();
    }
}
