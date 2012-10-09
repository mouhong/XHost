using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost.Commands.Impl
{
    public class AddEntryCommand : ICommand
    {
        public string Name
        {
            get
            {
                return "add";
            }
        }

        public string ShortName
        {
            get
            {
                return null;
            }
        }

        public string Description
        {
            get
            {
                return "Add an entry.";
            }
        }

        public string Usage
        {
            get
            {
                return "xhost -" + Name + " [ip] [host-name] [-override]";
            }
        }

        public void Execute(CommandLine commandLine, CommandExecutionContext context)
        {
            if (commandLine.Parameters.Count != 2)
            {
                ConsoleUtil.ErrorLine("Invalid request. Please check syntax: " + Usage);
                return;
            }

            var host = commandLine.Parameters[1];
            var ip = commandLine.Parameters[0];

            if (context.Hosts.Contains(host))
            {
                if (commandLine.Options.Count > 0 && commandLine.Options[0].Name == "override")
                {
                    context.Hosts.Set(ip, host);
                    context.Hosts.Save();

                    Console.WriteLine("1 entry updated: " + commandLine.Parameters[0] + " " + commandLine.Parameters[1]);
                }
                else
                {
                    ConsoleUtil.ErrorLine(host + " already exists. Use -override to override the existing entry.");
                }
            }
            else
            {
                context.Hosts.Set(commandLine.Parameters[0], commandLine.Parameters[1]);
                context.Hosts.Save();

                Console.WriteLine("1 entry added: " + commandLine.Parameters[0] + " " + commandLine.Parameters[1]);
            }
        }
    }
}
