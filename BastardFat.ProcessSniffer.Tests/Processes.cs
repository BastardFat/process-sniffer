using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BastardFat.ProcessSniffer.Tests
{
    [TestClass]
    public class Processes
    {
        public TestContext TestContext { get; set; }


        [TestMethod]
        public void GetAllProcesses()
        {
            var sniffer = new ProcessSniffer.Processes.Sniffing.BaseSniffer();
            sniffer.GetAllProcesses().ForEach(proc => TestContext.WriteLine($"{proc} | {sniffer.GetTitle(proc)}"));
        }

        [TestMethod]
        public void GetAllDesktopProcesses()
        {
            var sniffer = new ProcessSniffer.Processes.Sniffing.DesktopSniffer();
            sniffer.GetAllProcesses().ForEach(proc => TestContext.WriteLine($"{proc} | {sniffer.GetTitle(proc)}"));
        }


    }
}
