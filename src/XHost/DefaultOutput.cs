using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost
{
    public class DefaultOutput : IOutput
    {
        public static readonly DefaultOutput Instance = new DefaultOutput();

        public void Write(string message)
        {
            Console.Write(message);
        }

        public void Write(string format, params object[] args)
        {
            Console.Write(format, args);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void WarnLine(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void WarnLine(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(format, args);
            Console.ResetColor();
        }

        public void ErrorLine(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void ErrorLine(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(format, args);
            Console.ResetColor();
        }
    }
}
