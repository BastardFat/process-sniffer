using Microsoft.VisualStudio.TestTools.UnitTesting;
using BastardFat.ProcessSniffer.Processes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BastardFat.ProcessSniffer.Processes.Core.Tests
{
    [TestClass()]
    public class ProcessListUpdateWrapperTests
    {
        [TestMethod()]
        public void GetActualProcessListTest()
        {
            bool EndOk = false;
            bool StartOk = false;
            ProcessListUpdateWrapper.Instance.EndProcess += (p) => { if (p.Name.StartsWith("calc")) EndOk = true; };
            ProcessListUpdateWrapper.Instance.StartProcess += (p) => { if (p.Name.StartsWith("calc")) StartOk = true; };

            System.Diagnostics.Process.Start("calc.exe");
            System.Threading.Thread.Sleep(1000);
            System.Diagnostics.Process.GetProcessesByName("calc")?.First().Kill();
            System.Threading.Thread.Sleep(1000);

            Assert.IsTrue(EndOk && StartOk);

        }
    }
}