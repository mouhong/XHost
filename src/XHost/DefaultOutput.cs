using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost
{
    public class DefaultOutput : IOutput
    {
        public static readonly DefaultOutput Instance = new DefaultOutput();

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void WarnLine(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void ErrorLine(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
