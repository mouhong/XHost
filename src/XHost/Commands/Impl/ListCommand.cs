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
            var entries = context.HostFile.AllEntries();

            if (entries.Count == 0)
            {
                context.Output.WriteLine("Hosts file has no entry.");
            }
            else
            {
                foreach (var entry in entries)
                {
                    context.Output.WriteLine(entry.IP + "\t" + entry.Host);
                }
            }
        }
    }
}
