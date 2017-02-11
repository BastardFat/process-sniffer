using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BastardFat.ProcessSniffer.Processes.Tools
{
    public static class StringHelper
    {
        public static string Escape(string str)
        {

            return str.Replace("\\", "\\\\").
                       Replace("\"", "\\\"").
                       Replace("\b", "\\b").
                       Replace("\f", "\\f").
                       Replace("\n", "\\n").
                       Replace("\r", "\\r").
                       Replace("\t", "\\t");
        }

        public static string GenerateRandomString()
        {
            byte[] bytes = new byte[18];
            rand.NextBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
        private static Random rand = new Random((int) DateTime.Now.Ticks);

    }
}
