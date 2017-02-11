using BastardFat.ProcessSniffer.Processes.Core;
using BastardFat.ProcessSniffer.Processes.SelectionCriterias;
using BastardFat.ProcessSniffer.Processes.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BastardFat.ProcessSniffer.Processes
{
    public class ProcessManager
    {
        public ProcessManager()
        {
            ProcessListUpdateWrapper.Instance.StartProcess += CheckNewProcess;
            ProcessListUpdateWrapper.Instance.EndProcess += CheckNewProcess;
        }

        private void CheckNewProcess(Models.Process proc)
        {
            foreach (var killer in killers)
            {
                if(killer.Value.CheckCriteria(proc))
                {
                    var processForKilling = proc.FindByPID();

                    Task killTask = new Task(() =>
                    {
                        if (processForKilling.CloseMainWindow())
                        {
                            processForKilling.WaitForExit(3000);
                            if (processForKilling.HasExited) return;
                        }
                        processForKilling.Kill();
                    });

                    killTask.Start();
                }
            }
        }

        public string AddKiller(SelectionCriteria criteria)
        {
            string id = StringHelper.GenerateRandomString();
            killers.Add(id, criteria);
            return id;
        }

        public string AddWarner(SelectionCriteria criteria)
        {
            string id = StringHelper.GenerateRandomString();
            warners.Add(id, criteria);
            return id;
        }

        public void RemoveWarner(string id) => warners.Remove(id);
        public void RemoveKiller(string id) => killers.Remove(id);


        private Dictionary<string, SelectionCriteria> killers = new Dictionary<string, SelectionCriteria>();
        private Dictionary<string, SelectionCriteria> warners = new Dictionary<string, SelectionCriteria>();
    }
}
