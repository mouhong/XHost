using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost.Commands.Executors
{
    public class ListCommandExecutor : ICommandExecutor
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

        public void Execute(Command commandLine, CommandExecutionContext context)
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
