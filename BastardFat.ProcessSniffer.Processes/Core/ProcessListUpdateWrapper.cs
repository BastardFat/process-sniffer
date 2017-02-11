using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BastardFat.ProcessSniffer.Processes.Core
{
    class ProcessListUpdateWrapper
    {
        private ProcessListUpdateWrapper()
        {
            processList = new ProcessList();
            timer = new Timer(1000);
            timer.Elapsed += (s,e) => processList.UpdateProcessList();
            timer.Start();
        }

        public List<Models.Process> GetActualProcessList() => processList.ActualProcessList;

        public event Action<Models.Process> EndProcess
        {
            add { processList.EndProcess += value; }
            remove { processList.EndProcess -= value; }
        }

        public event Action<Models.Process> StartProcess
        {
            add { processList.StartProcess += value; }
            remove { processList.StartProcess -= value; }
        }

        public event Action<Models.Process> TitleChanged
        {
            add { processList.TitleChanged += value; }
            remove { processList.TitleChanged -= value; }
        }

        private ProcessList processList;
        private Timer timer;

        #region SingletonImplementation

        private static ProcessListUpdateWrapper instance = null;
        public static ProcessListUpdateWrapper Instance
        {
            get
            {
                if (instance == null)
                    lock (syncRoot)
                        if (instance == null)
                            instance = new ProcessListUpdateWrapper();
                return instance;
            }
        }

        private static object syncRoot = new object();
        #endregion

    }
}
