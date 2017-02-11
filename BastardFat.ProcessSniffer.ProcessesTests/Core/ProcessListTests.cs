using Microsoft.VisualStudio.TestTools.UnitTesting;
using BastardFat.ProcessSniffer.Processes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BastardFat.ProcessSniffer.Processes.Core.Tests
{
    [TestClass]
    public class ProcessListTests
    {
        [TestMethod]
        public void ProcessListTest()
        {
            ProcessList pl = new ProcessList();
        }

        [TestMethod]
        public void UpdateProcessListTest()
        {
            ProcessList pl = new ProcessList();
            pl.UpdateProcessList();
            pl.ActualProcessList.ForEach(p => Console.WriteLine(p.ToString()));
        }
    }
}