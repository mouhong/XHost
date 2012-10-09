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
                return "Display XHost help.";
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
            var output = context.Output;

            output.WriteLine("Welcome to {0} {1}", ApplicationInfo.Title, ApplicationInfo.Version.ToString(2));
            output.WriteLine(ApplicationInfo.Description);
            output.WriteLine(ApplicationInfo.CopyrightHolder);

            output.WriteLine();
            output.WriteLine("All available commands are:");

            foreach (var cmd in CommandFactory.All)
            {
                output.Write("-" + cmd.Name);
                output.Write("\t");

                if (!String.IsNullOrEmpty(cmd.ShortName))
                {
                    output.Write("[" + cmd.ShortName + "] ");
                }

                output.Write(cmd.Description);

                if (!String.IsNullOrEmpty(cmd.Usage))
                {
                    output.Write(" Usage: " + cmd.Usage);
                }

                output.WriteLine();
            }
        }
    }
}
