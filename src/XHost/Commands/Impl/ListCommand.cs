using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost.Commands.Impl
{
    public class ListCommand : ICommand
    {
        public string Name
        {
            get
            {
                return "list";
            }
        }

        public string ShortName
        {
            get
            {
                return "ls";
            }
        }

        public string Description
        {
            get
            {
                return "List all entries.";
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
            var entries = context.Hosts.AllEntries();

            if (entries.Count == 0)
            {
                Console.WriteLine("No entry was found.");
            }
            else
            {
                foreach (var entry in entries)
                {
                    Console.WriteLine(entry.IP + "\t" + entry.Host);
                }
            }
        }
    }
}
