using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost.Commands.Executors
{
    public class RemoveEntryCommandExecutor : ICommandExecutor
    {
        public string Name
        {
            get
            {
                return "remove";
            }
        }

        public string ShortName
        {
            get
            {
                return "rm";
            }
        }

        public void Execute(Command command, CommandExecutionContext context)
        {
            if (command.Parameters.Count == 0)
            {
                context.Output.ErrorLine("Host parameter is required. For example: -" + command.Name + " mouhong.me");
                return;
            }

            var removedHosts = 0;

            var hosts = command.Parameters[0].Split(',').Select(x => x.Trim()).Where(x => !String.IsNullOrEmpty(x));

            foreach (var host in hosts)
            {
                if (context.HostFile.Remove(host)) removedHosts++;
            }

            context.HostFile.Save();

            context.Output.WriteLine(removedHosts + " entries removed");
        }
    }
}
