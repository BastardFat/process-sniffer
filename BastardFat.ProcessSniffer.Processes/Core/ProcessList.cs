using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BastardFat.ProcessSniffer.Processes.Core
{
    public class ProcessList
    {
        public ProcessList()
        {
            actualProcessList = new List<Models.Process>();
        }

        private List<Models.Process> actualProcessList;
        public List<Models.Process> ActualProcessList => actualProcessList.ToList();

        public event Action<Models.Process> StartProcess = delegate { };
        public event Action<Models.Process> EndProcess = delegate { };
        public event Action<Models.Process> TitleChanged = delegate { };


        public void UpdateProcessList()
        {
            var newList = GetProcessList().ToList();
            foreach (var newProc in newList)
            {
                if (!actualProcessList.Any((oldProc) => oldProc.Pid == newProc.Pid))
                    StartProcess?.Invoke(newProc);
                if (actualProcessList.Any((oldProc) => oldProc.Pid == newProc.Pid && oldProc.Title != newProc.Title))
                    TitleChanged?.Invoke(newProc);
            }
            foreach (var oldProc in actualProcessList)
            {
                if (!newList.Any((newProc) => oldProc.Pid == newProc.Pid))
                    EndProcess?.Invoke(oldProc);

            }
            actualProcessList = newList;
        }

        private IEnumerable<Models.Process> GetProcessList()
        {
            var processes = System.Diagnostics.Process.GetProcesses();

            foreach (var process in processes)
            {
                if (process.MainWindowHandle != IntPtr.Zero)
                {
                    Models.Process t = null;
                    try
                    {
                        t = new Models.Process(process);
                    }
                    catch { }
                    if (t != null) yield return t;
                }
            }
        }

    }
}
