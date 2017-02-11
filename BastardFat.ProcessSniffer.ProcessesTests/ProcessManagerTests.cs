using Microsoft.VisualStudio.TestTools.UnitTesting;
using BastardFat.ProcessSniffer.Processes;
using System;
using BastardFat.ProcessSniffer.Processes.SelectionCriterias;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BastardFat.ProcessSniffer.Processes.Tests
{
    [TestClass]
    public class ProcessManagerTests
    {
        [TestMethod]
        public void ProcessManagerTest()
        {
            int state = 0;
            ProcessManager pm = new ProcessManager();
            string id = pm.AddKiller(new ProcessSelectionCriteria(Enumerations.ProcessFields.Name, Enumerations.SelectionCriteriaMode.Equal, "calc"));
            System.Diagnostics.Process.Start("calc.exe");
            Thread.Sleep(1500);
            if (!System.Diagnostics.Process.GetProcesses().Any(p => p.ProcessName == "calc")) state++;
            pm.RemoveKiller(id);
            System.Diagnostics.Process.Start("calc.exe");
            Thread.Sleep(1500);
            if (System.Diagnostics.Process.GetProcesses().Any(p => p.ProcessName == "calc")) state++;
            System.Diagnostics.Process.GetProcessesByName("calc")?.First().Kill();
            Assert.IsTrue(state == 2);

        }
    }
}