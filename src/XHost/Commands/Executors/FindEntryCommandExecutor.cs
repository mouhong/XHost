using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost.Commands.Executors
{
    public class FindEntryCommandExecutor : ICommandExecutor
    {
        public string Name
        {
            get
            {
                return "find";
            }
        }

        public string ShortName
        {
            get
            {
                return "f";
            }
        }

        public void Execute(Command command, CommandExecutionContext context)
        {
            if (command.Parameters.Count == 0)
            {
                context.Output.ErrorLine(command.Name + " command requires arguments.");
                return;
            }

            var entry = context.HostFile.FindEntry(command.Parameters[0]);

            if (entry == null)
            {
                context.Output.WarnLine("No entry was found.");
            }
            else
            {
                context.Output.WriteLine(entry.IP + " " + entry.Host);
            }
        }
    }
}
