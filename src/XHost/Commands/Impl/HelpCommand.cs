using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost.Commands.Impl
{
  public  class HelpCommand:ICommand
    {
        public string Name
        {
            get
            {
                return "help";
            }
        }

        public string ShortName
        {
            get
            {
                return "h";
            }
        }

        public string Description
        {
            get
            {
                return "Show help.";
            }
        }

        public string Usage
        {
            get
            {
                return "xhost -" + Name;
            }
        }

        public void Execute(CommandLine commandLine, CommandExecutionContext context)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.WriteLine("==================================================");
            Console.WriteLine(" Welcome to {0} {1}", ApplicationInfo.Title, ApplicationInfo.Version.ToString(2));
            Console.WriteLine(" " + ApplicationInfo.Description);
            Console.WriteLine(" " + ApplicationInfo.CopyrightHolder);
            Console.WriteLine("==================================================");

            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("Available commands are:");

            foreach (var cmd in CommandFactory.All)
            {
                Console.Write("-" + cmd.Name);
                Console.Write("\t");

                if (!String.IsNullOrEmpty(cmd.ShortName))
                {
                    Console.Write("[" + cmd.ShortName + "] ");
                }

                Console.Write(cmd.Description);

                if (!String.IsNullOrEmpty(cmd.Usage))
                {
                    Console.Write(" Usage: " + cmd.Usage);
                }

                Console.WriteLine();
            }
        }
    }
}
