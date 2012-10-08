using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XHost.Commands.Executors
{
    public class AddEntryCommandExecutor : ICommandExecutor
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
                return "a";
            }
        }

        public void Execute(Command command, CommandExecutionContext context)
        {
            if (command.Parameters.Count != 2)
            {
                context.Output.ErrorLine("Please enter the IP and the host name as parameters. For example: -" + command.Name + " 127.0.0.1 mouhong.me");
                return;
            }

            context.HostFile.Set(command.Parameters[0], command.Parameters[1]);
            context.HostFile.Save();

            context.Output.WriteLine("New entry added: " + command.Parameters[0] + " " + command.Parameters[1]);
        }
    }
}
